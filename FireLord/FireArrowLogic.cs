using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using FireLord.Settings;

namespace FireLord
{
    public class FireArrowLogic : MissionLogic
    {
        private static int[] _ignitionBoneIndexes = { 0, 1, 2, 3, 5, 6, 7, 9, 12, 13, 15, 17, 22, 24 };

        private bool _initialized = false;
        private bool _fireArrowEnabled = false;
        private Dictionary<Mission.Missile, ParticleSystem> _missileParticles = new Dictionary<Mission.Missile, ParticleSystem>();
        private List<ArrowFireData> _hitArrowFires = new List<ArrowFireData>();

        public static Dictionary<Agent, AgentFireData> AgentFireDatas = new Dictionary<Agent, AgentFireData>();

        public class AgentFireData
        {
            public bool isBurning = false;
            public float firebar = 0;
            public MissionTimer burningTimer;
            public GameEntity fireEntity;
            public Light fireLight;
            public Agent attacker;
            public MissionTimer damageTimer;
            public ParticleSystem[] particles;
        }

        public class ArrowFireData
        {
            public bool delete = false;
            public MissionTimer timer;
            public Mission.Missile missile;
            public ParticleSystem bigFireParticle;
        }

        public void Initialize()
        {
            float timeOfDay = Mission.Current.Scene.TimeOfDay;
            AgentFireDatas = new Dictionary<Agent, AgentFireData>();
            _fireArrowEnabled = (FireLordConfig.UseFireArrowsAtDay && (timeOfDay > 2 && timeOfDay < 22))
                || (FireLordConfig.UseFireArrowsAtNight && (timeOfDay <= 2 || timeOfDay >= 22))
                || (FireLordConfig.UseFireArrowsAtSiege && Mission.Current.MissionTeamAIType == Mission.MissionTeamAITypeEnum.Siege);
        }

        public override void OnRemoveBehaviour()
        {
            AgentFireDatas = new Dictionary<Agent, AgentFireData>();
        }

        public override void OnMissionTick(float dt)
        {
            if (!_initialized && Mission.Current != null && Agent.Main != null)
            {
                _initialized = true;
                Initialize();
            }

            //每支箭落地燃烧，4秒火势变弱，8秒熄灭（默认）
            if (_hitArrowFires.Count > 0)
            {
                List<ArrowFireData> _deleteitems = new List<ArrowFireData>();
                foreach (ArrowFireData item in _hitArrowFires)
                {
                    if (item.timer.Check(true))
                    {
                        if (!item.delete)
                        {
                            item.delete = true;
                            GameEntity entity = item.bigFireParticle.GetEntity();
                            if (entity != null)
                            {
                                entity.GetLight().Intensity = FireLordConfig.FireArrowLightIntensity - 35;
                                entity.RemoveComponent(item.bigFireParticle);
                            }
                        }
                        else
                        {
                            GameEntity entity = item.missile.Entity;
                            if (entity != null)
                            {
                                entity.RemoveAllParticleSystems();
                                Light light = entity.GetLight();
                                if (light != null)
                                    entity.RemoveComponent(light);
                            }
                            _deleteitems.Add(item);
                        }
                    }
                }
                foreach(ArrowFireData item in _deleteitems)
                {
                    _hitArrowFires.Remove(item);
                }
            }

            //计算每个agent的着火条，满了就点燃
            if (AgentFireDatas.Count > 0)
            {
                List<Agent> _deleteAgent = new List<Agent>();
                foreach (KeyValuePair<Agent, AgentFireData> item in AgentFireDatas)
                {
                    Agent agent = item.Key;
                    AgentFireData fireData = item.Value;
                    if (fireData.isBurning)
                    {
                        if (FireLordConfig.IgnitionDealDamage && fireData.damageTimer.Check(true) && agent.IsActive())
                        {
                            Blow blow = CreateBlow(fireData.attacker, agent);
                            agent.RegisterBlow(blow);
                            if(fireData.attacker == Agent.Main)
                            {
                                TextObject text = GameTexts.FindText("ui_delivered_burning_damage", null);
                                text.SetTextVariable("DAMAGE", blow.InflictedDamage);
                                InformationManager.DisplayMessage(new InformationMessage(text.ToString()));
                            }
                            else if (agent == Agent.Main)
                            {
                                TextObject text = GameTexts.FindText("ui_received_burning_damage", null);
                                text.SetTextVariable("DAMAGE", blow.InflictedDamage);
                                InformationManager.DisplayMessage(new InformationMessage(text.ToString(), Color.ConvertStringToColor("#D65252FF")));
                            }
                        }
                        if (fireData.burningTimer.Check())
                        {
                            if (fireData.fireEntity != null)
                            {
                                foreach (ParticleSystem particle in fireData.particles)
                                {
                                    fireData.fireEntity.RemoveComponent(particle);
                                }
                                if (fireData.fireLight != null)
                                {
                                    fireData.fireLight.Intensity = 0;
                                    Skeleton skeleton = agent.AgentVisuals.GetSkeleton();
                                    if (skeleton != null)
                                        skeleton.RemoveComponent(fireData.fireLight);
                                }
                                fireData.fireEntity = null;
                                fireData.fireLight = null;
                            }
                            fireData.firebar = 0;
                            fireData.isBurning = false;
                        }
                    }
                    else
                    {
                        if (fireData.firebar >= FireLordConfig.IgnitionBarMax)
                        {
                            fireData.isBurning = true;
                            fireData.burningTimer = new MissionTimer(FireLordConfig.IgnitionDurationInSecond);
                            fireData.damageTimer = new MissionTimer(1f);
                            EquipmentIndex index = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
                            if (index == EquipmentIndex.None)
                                return;
                            GameEntity wieldedWeaponEntity = agent.GetWeaponEntityFromEquipmentSlot(index);
                            Skeleton skeleton = agent.AgentVisuals.GetSkeleton();
                            if (skeleton == null)
                                return;
                            fireData.particles = new ParticleSystem[_ignitionBoneIndexes.Length];
                            for (byte i = 0; i < _ignitionBoneIndexes.Length; i++)
                            {
                                MatrixFrame localFrame = new MatrixFrame(Mat3.Identity, new Vec3(0, 0, 0));
                                ParticleSystem particle = ParticleSystem.CreateParticleSystemAttachedToEntity("psys_campfire",
                                    wieldedWeaponEntity, ref localFrame);
                                skeleton.AddComponentToBone(_ignitionBoneIndexes[i], particle);
                                fireData.particles[i] = particle;
                            }

                            //只有通过扔掉再重新捡起这把武器，才能让粒子效果出现
                            FireSwordLogic.DropLock = true;
                            agent.DropItem(index);
                            SpawnedItemEntity spawnedItemEntity = wieldedWeaponEntity.GetFirstScriptOfType<SpawnedItemEntity>();
                            agent.OnItemPickup(spawnedItemEntity, EquipmentIndex.None, out bool removeItem);
                            fireData.fireEntity = wieldedWeaponEntity;
                            FireSwordLogic.DropLock = false;

                            Light light = Light.CreatePointLight(FireLordConfig.IgnitionLightRadius);
                            light.Intensity = FireLordConfig.IgnitionLightIntensity;
                            light.LightColor = FireLordConfig.IgnitionLightColor;
                            skeleton.AddComponentToBone(0, light);
                            fireData.fireLight = light;
                        }
                        else
                        {
                            fireData.firebar -= dt * FireLordConfig.IgnitionDropPerSecond;
                            fireData.firebar = Math.Max(fireData.firebar, 0);

                            if (!agent.IsActive())
                                _deleteAgent.Add(agent);
                        }
                    }
                }
                foreach (Agent agent in _deleteAgent)
                {
                    AgentFireData fireData = AgentFireDatas[agent];
                    GameEntity entity = fireData.fireEntity;
                    if(entity!=null)
                        entity.RemoveAllParticleSystems();
                    Skeleton skeleton = agent.AgentVisuals.GetSkeleton();
                    if (skeleton != null && fireData.fireLight != null)
                        skeleton.RemoveComponent(fireData.fireLight);
                    AgentFireDatas.Remove(agent);
                }
            }
        }

        public override void OnAgentShootMissile(Agent shooterAgent, EquipmentIndex weaponIndex, Vec3 position, Vec3 velocity, Mat3 orientation, bool hasRigidBody, int forcedMissileIndex)
        {
            if (!_fireArrowEnabled)
                return;

            bool allowed = false;
            MissionWeapon weapon = shooterAgent.Equipment[weaponIndex];
            switch (weapon.CurrentUsageItem.WeaponClass)
            {
                case WeaponClass.Arrow:
                case WeaponClass.Bolt:
                case WeaponClass.Bow:
                case WeaponClass.Crossbow:
                    allowed = true;
                    break;
                case WeaponClass.ThrowingAxe:
                case WeaponClass.ThrowingKnife:
                case WeaponClass.Javelin:
                    allowed = FireLordConfig.AllowFireThrownWeapon;
                    break;
            }

            allowed &= (FireLordConfig.AllowedUnitType == FireLordConfig.UnitType.All) 
                || (FireLordConfig.AllowedUnitType == FireLordConfig.UnitType.Player && shooterAgent == Agent.Main)
                || (FireLordConfig.AllowedUnitType == FireLordConfig.UnitType.Heroes && shooterAgent.IsHero)
                || (FireLordConfig.AllowedUnitType == FireLordConfig.UnitType.Companions && shooterAgent.IsHero && shooterAgent.Team.IsPlayerTeam)
                || (FireLordConfig.AllowedUnitType == FireLordConfig.UnitType.Allies && shooterAgent.Team.IsPlayerAlly)
                || (FireLordConfig.AllowedUnitType == FireLordConfig.UnitType.Enemies && !shooterAgent.Team.IsPlayerAlly);

            allowed &= shooterAgent== Agent.Main || MBRandom.RandomFloatRanged(100) < FireLordConfig.ChancesOfFireArrow;

            if (allowed)
            {
                foreach (Mission.Missile missile in Mission.Current.Missiles)
                {
                    if (missile.ShooterAgent == shooterAgent && !_missileParticles.ContainsKey(missile))
                    {
                        MatrixFrame localFrame = new MatrixFrame(Mat3.Identity, new Vec3(0, 0, 0));
                        ParticleSystem particle = ParticleSystem.CreateParticleSystemAttachedToEntity(
                            "psys_game_burning_agent", missile.Entity, ref localFrame);
                        Light light = Light.CreatePointLight(FireLordConfig.FireArrowLightRadius);
                        light.Intensity = FireLordConfig.FireArrowLightIntensity;
                        light.LightColor = FireLordConfig.FireArrowLightColor;
                        missile.Entity.AddLight(light);
                        _missileParticles.Add(missile, particle);
                        break;
                    }
                }
            }
        }

        public override void OnMissileCollisionReaction(Mission.MissileCollisionReaction collisionReaction, Agent attacker, Agent victim, sbyte attachedBoneIndex)
        {
            if (!_fireArrowEnabled)
                return;

            Dictionary<Mission.Missile, ParticleSystem> existMissiles = new Dictionary<Mission.Missile, ParticleSystem>();
            foreach (Mission.Missile missile in Mission.Current.Missiles)
            {
                if (_missileParticles.ContainsKey(missile))
                {
                    existMissiles.Add(missile, _missileParticles[missile]);
                    _missileParticles.Remove(missile);
                }
            }
            foreach (KeyValuePair<Mission.Missile, ParticleSystem> item in _missileParticles)
            {
                Mission.Missile missile = item.Key;
                Light light = missile.Entity.GetLight();

                //击中敌人箭矢必须熄灭，因为插在Agent身上的箭的粒子效果是无法显示的
                if (victim != null)
                {
                    missile.Entity.RemoveAllParticleSystems();
                    if (light != null)
                        missile.Entity.RemoveComponent(light);
                }
                else
                {
                    ParticleSystem particle = item.Value;
                    MatrixFrame localFrame = particle.GetLocalFrame().Elevate(0.6f);
                    particle.SetLocalFrame(ref localFrame);
                    if (light != null)
                    {
                        light.Frame = light.Frame.Elevate(0.15f);
                        light.Intensity = FireLordConfig.FireArrowLightIntensity;
                    }

                    ArrowFireData fireData = new ArrowFireData();

                    localFrame = new MatrixFrame(Mat3.Identity, new Vec3(0, 0, 0)).Elevate(0.6f);
                    fireData.bigFireParticle = ParticleSystem.CreateParticleSystemAttachedToEntity("psys_campfire", missile.Entity, ref localFrame);
                    fireData.timer = new MissionTimer(FireLordConfig.StickedArrowsBurningTime/2);
                    fireData.missile = missile;
                    _hitArrowFires.Add(fireData);
                }

                if(FireLordConfig.IgniteTargetWithFireArrow && victim != null && victim.IsHuman)
                {
                    if (AgentFireDatas.ContainsKey(victim))
                    {
                        AgentFireData fireData = AgentFireDatas[victim];
                        if (!fireData.isBurning)
                        {
                            fireData.firebar += FireLordConfig.IgnitionPerFireArrow;
                            fireData.attacker = attacker;
                        }
                    }
                    else
                    {
                        AgentFireData fireData = new AgentFireData();
                        fireData.firebar += FireLordConfig.IgnitionPerFireArrow;
                        fireData.attacker = attacker;
                        AgentFireDatas.Add(victim, fireData);
                    }
                }
            }
            _missileParticles = existMissiles;
        }

        private Blow CreateBlow(Agent attacker, Agent victim)
        {
            Blow blow = new Blow(attacker.Index);
            blow.VictimBodyPart = BoneBodyPartType.None;
            blow.AttackType = AgentAttackType.Standard;
            blow.WeaponRecord = default(BlowWeaponRecord);
            blow.WeaponRecord.WeaponFlags = (blow.WeaponRecord.WeaponFlags | (WeaponFlags.AffectsArea | WeaponFlags.Burning));
            blow.MissileRecord.IsValid = true;
            blow.MissileRecord.CurrentPosition = blow.Position;
            blow.MissileRecord.MissileItemKind = 67109974;
            blow.MissileRecord.StartingPosition = blow.Position;
            blow.MissileRecord.WeaponFlags = blow.WeaponRecord.WeaponFlags;
            blow.StrikeType = StrikeType.Invalid;
            blow.DamageType = DamageTypes.Blunt;
            blow.NoIgnore = false;
            blow.AttackerStunPeriod = 0;
            blow.DefenderStunPeriod = 0;
            blow.BlowFlag = BlowFlags.ShrugOff;
            blow.BlowFlag |= BlowFlags.NoSound;
            blow.Position = victim.GetEyeGlobalPosition();
            blow.BoneIndex = 0;
            blow.Direction = victim.LookDirection;
            blow.SwingDirection = victim.LookDirection;
            blow.BaseMagnitude = 0;
            blow.MovementSpeedDamageModifier = 1;
            blow.InflictedDamage = FireLordConfig.IgnitionDamagePerSecond;
            blow.SelfInflictedDamage = 0;
            blow.AbsorbedByArmor = 0;
            blow.DamageCalculated = true;
            return blow;
        }
    }
}
