using FireLord.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace FireLord
{
    public class IgnitionLogic : MissionLogic
    {
        public Dictionary<Agent, AgentFireData> AgentFireDatas = new Dictionary<Agent, AgentFireData>();

        private static int[] _ignitionBoneIndexes = { 0, 1, 2, 3, 5, 6, 7, 9, 12, 13, 15, 17, 22, 24 };

        public delegate void OnAgentDropItemDelegate(Agent agent, bool dropLock);

        public event OnAgentDropItemDelegate OnAgentDropItem;

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

        public void IncreaseAgentFireBar(Agent attacker, Agent victim, float firebarAdd)
        {
            if (AgentFireDatas.ContainsKey(victim))
            {
                AgentFireData fireData = AgentFireDatas[victim];
                if (!fireData.isBurning)
                {
                    fireData.firebar += firebarAdd;
                    fireData.attacker = attacker;
                }
            }
            else
            {
                AgentFireData fireData = new AgentFireData();
                fireData.firebar += firebarAdd;
                fireData.attacker = attacker;
                AgentFireDatas.Add(victim, fireData);
            }
        }
        
        public bool IsInBattle()
        {
            return (Mission.Mode == MissionMode.Battle || Mission.Mode == MissionMode.Duel
                || Mission.Mode == MissionMode.Stealth || Mission.Mode == MissionMode.Tournament);
        }

        public override void OnMissionTick(float dt)
        {
            if (!IsInBattle())
                return;
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
                            if (fireData.attacker == Agent.Main)
                            {
                                TextObject text = GameTexts.FindText("ui_delivered_burning_damage", null);
                                //text.SetTextVariable("DAMAGE", blow.InflictedDamage);
                                string damageText = Regex.Replace(text.ToString(), @"\d+", blow.InflictedDamage + "");
                                InformationManager.DisplayMessage(new InformationMessage(damageText));
                            }
                            else if (agent == Agent.Main)
                            {
                                TextObject text = GameTexts.FindText("ui_received_burning_damage", null);
                                //text.SetTextVariable("DAMAGE", blow.InflictedDamage);
                                string damageText = Regex.Replace(text.ToString(), @"\d+", blow.InflictedDamage + "");
                                InformationManager.DisplayMessage(new InformationMessage(damageText, Color.ConvertStringToColor("#D65252FF")));
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
                                    MBAgentVisuals agentVisuals = agent.AgentVisuals;
                                    if (agentVisuals != null)
                                    {
                                        Skeleton skeleton = agentVisuals.GetSkeleton();
                                        if (skeleton != null)
                                            skeleton.RemoveComponent(fireData.fireLight);
                                    }
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
                            MBAgentVisuals agentVisuals = agent.AgentVisuals;
                            if (agentVisuals == null)
                                return;
                            Skeleton skeleton = agentVisuals.GetSkeleton();
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
                            if(OnAgentDropItem!=null)
                                OnAgentDropItem(agent, true);
                            agent.DropItem(index);
                            SpawnedItemEntity spawnedItemEntity = wieldedWeaponEntity.GetFirstScriptOfType<SpawnedItemEntity>();
                            if (spawnedItemEntity != null)
                                agent.OnItemPickup(spawnedItemEntity, EquipmentIndex.None, out bool removeItem);
                            fireData.fireEntity = wieldedWeaponEntity;
                            if (OnAgentDropItem != null)
                                OnAgentDropItem(agent, false);

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
                    if (entity != null)
                        entity.RemoveAllParticleSystems();
                    MBAgentVisuals agentVisuals = agent.AgentVisuals;
                    if (agentVisuals != null)
                    {
                        Skeleton skeleton = agentVisuals.GetSkeleton();
                        if (skeleton != null && fireData.fireLight != null)
                            skeleton.RemoveComponent(fireData.fireLight);
                    }
                    AgentFireDatas.Remove(agent);
                }
            }
        }

        private Blow CreateBlow(Agent attacker, Agent victim)
        {
            Blow blow = new Blow(attacker.Index);
            blow.DamageType = DamageTypes.Blunt;
            blow.BlowFlag = BlowFlags.ShrugOff;
            blow.BlowFlag |= BlowFlags.NoSound;
            blow.BoneIndex = victim.Monster.HeadLookDirectionBoneIndex;
            blow.Position = victim.Position;
            blow.Position.z = blow.Position.z + victim.GetEyeGlobalHeight();
            blow.BaseMagnitude = 0;
            blow.WeaponRecord.FillWith(null, -1, -1);
            blow.InflictedDamage = FireLordConfig.IgnitionDamagePerSecond;
            blow.SwingDirection = victim.LookDirection;
            blow.SwingDirection.Normalize();
            blow.Direction = blow.SwingDirection;
            blow.DamageCalculated = true;
            return blow;
        }
    }
}
