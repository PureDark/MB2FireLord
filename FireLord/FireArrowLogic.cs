using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using FireLord.Settings;
using TaleWorlds.InputSystem;

namespace FireLord
{
    public class FireArrowLogic : MissionLogic
    {
        private bool _initialized = false;
        private bool _fireArrowEnabled = false;

        private IgnitionLogic _ignitionlogic;

        private Dictionary<Mission.Missile, ParticleSystem> _missileParticles = new Dictionary<Mission.Missile, ParticleSystem>();
        private List<ArrowFireData> _hitArrowFires = new List<ArrowFireData>();

        public class ArrowFireData
        {
            public bool delete = false;
            public MissionTimer timer;
            public Mission.Missile missile;
            public ParticleSystem bigFireParticle;
        }

        public FireArrowLogic(IgnitionLogic ignitionlogic)
        {
            _ignitionlogic = ignitionlogic;
        }


        public void Initialize()
        {
            float timeOfDay = Mission.Current.Scene.TimeOfDay;

            if(FireLordConfig.FireArrowAllowedTimeStart < FireLordConfig.FireArrowAllowedTimeEnd)
                _fireArrowEnabled = (timeOfDay >= FireLordConfig.FireArrowAllowedTimeStart && timeOfDay <= FireLordConfig.FireArrowAllowedTimeEnd);
            else
                _fireArrowEnabled = (timeOfDay >= FireLordConfig.FireArrowAllowedTimeStart || timeOfDay <= FireLordConfig.FireArrowAllowedTimeEnd);

            _fireArrowEnabled &= FireLordConfig.UseFireArrowsOnlyInSiege?
                Mission.Current.MissionTeamAIType == Mission.MissionTeamAITypeEnum.Siege
                :true;
        }

        public override void OnMissionTick(float dt)
        {
            if (!_initialized && Mission.Current != null && Agent.Main != null)
            {
                _initialized = true;
                Initialize();
            }

            if (Input.IsKeyPressed(FireLordConfig.FireArrowToggleKey))
            {
                _fireArrowEnabled = !_fireArrowEnabled;
                TextObject text = GameTexts.FindText("ui_fire_arrow_" + (_fireArrowEnabled?"enabled":"disabled"), null);
                InformationManager.DisplayMessage(new InformationMessage(text.ToString()));
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
                                entity.GetLight().Intensity = Math.Max(FireLordConfig.FireArrowLightIntensity - 35, 0);
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

            allowed &= shooterAgent == Agent.Main || MBRandom.RandomFloatRanged(100) < FireLordConfig.ChancesOfFireArrow;

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

            var existMissiles = new Dictionary<Mission.Missile, ParticleSystem>();
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

                //下面是点燃敌人的逻辑
                if(FireLordConfig.IgniteTargetWithFireArrow && victim != null && victim.IsHuman)
                {
                    if (FireLordConfig.IgnitionFriendlyFire || attacker.IsEnemyOf(victim))
                    {
                        bool isBlocked = attachedBoneIndex < 0;
                        float firebarAdd = (isBlocked) ? FireLordConfig.IgnitionPerFireArrow / 2f : FireLordConfig.IgnitionPerFireArrow;
                        _ignitionlogic.IncreaseAgentFireBar(attacker, victim, firebarAdd);
                    }
                }
            }
            _missileParticles = existMissiles;
        }
    }
}
