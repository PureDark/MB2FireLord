using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using FireLord.Settings;
using System.Collections.Generic;

namespace FireLord
{
    class FireSwordLogic : MissionLogic
    {
        private static bool _playerFireSwordEnabled = false;

        private IgnitionLogic _ignitionlogic;

        private Dictionary<Agent, AgentFireSwordData> _agentFireSwordData = new Dictionary<Agent, AgentFireSwordData>();

        public class AgentFireSwordData
        {
            public bool enabled = false;
            public Agent agent;
            public GameEntity entity;
            public Light light;
            public bool dropLock;
            public MissionTimer timer;
            public bool lastWieldedWeaponEmpty = false;

            public void OnAgentWieldedItemChange()
            {
                if (dropLock || agent == null)
                    return;
                MissionWeapon wieldedWeapon = agent.WieldedWeapon;
                MissionWeapon wieldedOffHandWeapon = agent.WieldedOffhandWeapon;
                // 判断是否是抽出新武器
                if(lastWieldedWeaponEmpty && !wieldedWeapon.Weapons.IsEmpty())
                {
                    if (!agent.IsMainAgent || _playerFireSwordEnabled)
                    {
                        timer = new MissionTimer(0.1f);
                    }
                }
                else
                {
                    SetFireSwordEnable(false);
                }

                lastWieldedWeaponEmpty = wieldedWeapon.Weapons.IsEmpty();
            }

            public void OnAgentHealthChanged(Agent agent, float oldHealth, float newHealth)
            {
                if (newHealth <= 0)
                {
                    SetFireSwordEnable(false);
                }
            }

            public void SetFireSwordEnable(bool enable)
            {
                if (agent == null)
                    return;
                if (enable)
                {
                    SetFireSwordEnable(false);
                    EquipmentIndex index = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
                    if (index == EquipmentIndex.None)
                        return;
                    GameEntity wieldedWeaponEntity = agent.GetWeaponEntityFromEquipmentSlot(index);

                    MissionWeapon wieldedWeapon = agent.WieldedWeapon;
                    if (wieldedWeapon.Weapons.IsEmpty())
                        return;
                    
                    bool allowed = (FireLordConfig.FireSwordAllowedUnitType == FireLordConfig.UnitType.All)
                        || (FireLordConfig.FireSwordAllowedUnitType == FireLordConfig.UnitType.Player && agent == Agent.Main)
                        || (FireLordConfig.FireSwordAllowedUnitType == FireLordConfig.UnitType.Heroes && agent.IsHero)
                        || (FireLordConfig.FireSwordAllowedUnitType == FireLordConfig.UnitType.Companions && agent.IsHero && agent.Team.IsPlayerTeam)
                        || (FireLordConfig.FireSwordAllowedUnitType == FireLordConfig.UnitType.Allies && agent.Team.IsPlayerAlly)
                        || (FireLordConfig.FireSwordAllowedUnitType == FireLordConfig.UnitType.Enemies && !agent.Team.IsPlayerAlly);

                    if (!allowed)
                    {
                        switch (FireLordConfig.FireSwordWhitelistType)
                        {
                            case FireLordConfig.WhitelistType.Troops:
                                allowed = FireLordConfig.FireSwordTroopsWhitelist.Contains(agent.Character.StringId);
                                break;
                            case FireLordConfig.WhitelistType.Items:
                                allowed = FireLordConfig.FireSwordItemsWhitelist.Contains(wieldedWeapon.PrimaryItem.ToString());
                                break;
                        }
                    }

                    if (!allowed)
                        return;

                    int length = wieldedWeapon.GetWeaponStatsData()[0].WeaponLength;
                    int num = (int)Math.Round(length / 10f);
                    MBAgentVisuals agentVisuals = agent.AgentVisuals;
                    if (agentVisuals == null)
                        return;
                    Skeleton skeleton = agentVisuals.GetSkeleton();
                    Light light = Light.CreatePointLight(FireLordConfig.FireSwordLightRadius);
                    light.Intensity = FireLordConfig.FireSwordLightIntensity;
                    light.LightColor = FireLordConfig.FireSwordLightColor;

                    switch (wieldedWeapon.CurrentUsageItem.WeaponClass)
                    {
                        case WeaponClass.OneHandedSword:
                        case WeaponClass.TwoHandedSword:
                        case WeaponClass.Mace:
                        case WeaponClass.TwoHandedMace:
                            for (int i = 1; i < num; i++)
                            {
                                MatrixFrame localFrame = new MatrixFrame(Mat3.Identity, new Vec3(0, 0, 0)).Elevate(i * 0.1f);
                                ParticleSystem particle = ParticleSystem.CreateParticleSystemAttachedToEntity("psys_game_burning_agent",
                                    wieldedWeaponEntity, ref localFrame);
                                skeleton.AddComponentToBone(Game.Current.HumanMonster.MainHandItemBoneIndex, particle);
                            }
                            light.Frame = light.Frame.Elevate((num - 1) * 0.1f);
                            break;
                        case WeaponClass.OneHandedAxe:
                        case WeaponClass.TwoHandedAxe:
                        case WeaponClass.OneHandedPolearm:
                        case WeaponClass.TwoHandedPolearm:
                        case WeaponClass.LowGripPolearm:
                            int fireLenth = (num > 19) ? 9 : (num > 15) ? 6 : (num > 12) ? 5 : (num > 10) ? 4 : 3;
                            for (int i = num - 1; i > 0 && i > num - fireLenth; i--)
                            {
                                MatrixFrame localFrame = new MatrixFrame(Mat3.Identity, new Vec3(0, 0, 0)).Elevate(i * 0.1f);
                                ParticleSystem particle = ParticleSystem.CreateParticleSystemAttachedToEntity("psys_game_burning_agent",
                                    wieldedWeaponEntity, ref localFrame);
                                skeleton.AddComponentToBone(Game.Current.HumanMonster.MainHandItemBoneIndex, particle);
                            }
                            light.Frame = light.Frame.Elevate((num - 1) * 0.1f);
                            break;
                        default:
                            //InformationManager.DisplayMessage(new InformationMessage("当前武器无法点燃"));
                            return;
                    }
                    skeleton.AddComponentToBone(Game.Current.HumanMonster.MainHandItemBoneIndex, light);

                    if (agent.IsMainAgent && FireLordConfig.IgnitePlayerBody)
                    {
                        int count = skeleton.GetBoneCount();
                        for (byte i = 0; i < count; i++)
                        {
                            MatrixFrame localFrame = new MatrixFrame(Mat3.Identity, new Vec3(0, 0, 0));
                            ParticleSystem particle = ParticleSystem.CreateParticleSystemAttachedToEntity("psys_game_burning_agent",
                                wieldedWeaponEntity, ref localFrame);
                            skeleton.AddComponentToBone(i, particle);
                        }
                    }

                    //只有通过扔掉再重新捡起这把武器，才能让粒子效果出现
                    dropLock = true;
                    agent.DropItem(index);
                    SpawnedItemEntity spawnedItemEntity = wieldedWeaponEntity.GetFirstScriptOfType<SpawnedItemEntity>();
                    agent.OnItemPickup(spawnedItemEntity, EquipmentIndex.None, out bool removeItem);
                    dropLock = false;

                    this.light = light;
                    entity = wieldedWeaponEntity;
                    enabled = true;
                }
                else
                {
                    //关闭火焰剑，移除相关粒子
                    enabled = false;
                    if (entity != null && agent != null)
                    {
                        MBAgentVisuals agentVisuals = agent.AgentVisuals;
                        if(agentVisuals != null)
                        {
                            Skeleton skeleton = agentVisuals.GetSkeleton();
                            if (light != null && skeleton != null)
                                skeleton.RemoveComponent(light);
                        }
                        entity.RemoveAllParticleSystems();
                    }
                }
            }

        }
        
        public FireSwordLogic(IgnitionLogic ignitionlogic)
        {
            _ignitionlogic = ignitionlogic;
            _ignitionlogic.OnAgentDropItem += SetDropLockForAgent;
            _playerFireSwordEnabled = FireLordConfig.PlayerFireSwordDefaultOn;
        }

        public void SetDropLockForAgent(Agent agent, bool dropLock)
        {
            _agentFireSwordData.TryGetValue(agent, out AgentFireSwordData fireSwordData);
            if (fireSwordData != null)
            {
                fireSwordData.dropLock = dropLock;
            }
        }

        
        public override void OnAgentCreated(Agent agent)
        {
            if (agent.IsHuman && !_agentFireSwordData.ContainsKey(agent))
            {
                AgentFireSwordData agentFireSwordData = new AgentFireSwordData();
                agentFireSwordData.agent = agent;
                agent.OnAgentWieldedItemChange += agentFireSwordData.OnAgentWieldedItemChange;
                agent.OnAgentHealthChanged += agentFireSwordData.OnAgentHealthChanged;
                agentFireSwordData.lastWieldedWeaponEmpty = agent.WieldedWeapon.Weapons.IsEmpty();
                if (!agent.IsMainAgent || _playerFireSwordEnabled)
                {
                    agentFireSwordData.timer = new MissionTimer(0.5f);
                }
                
                _agentFireSwordData.Add(agent, agentFireSwordData);
            }
        }

        public override void OnAgentDeleted(Agent agent)
        {
            _agentFireSwordData.Remove(agent);
        }


        public override void OnMissionTick(float dt)
        {
            foreach(KeyValuePair<Agent, AgentFireSwordData> item in _agentFireSwordData)
            {
                AgentFireSwordData fireSwordData = item.Value;
                if(item.Key.IsMainAgent && !_playerFireSwordEnabled)
                {
                    fireSwordData.timer = null;
                }
                else if (fireSwordData.timer != null && fireSwordData.timer.Check())
                {
                    fireSwordData.SetFireSwordEnable(true);
                    fireSwordData.timer = null;
                }
            }
            if (Input.IsKeyPressed(FireLordConfig.FireSwordToggleKey) && Agent.Main != null)
            {
                _playerFireSwordEnabled = !_playerFireSwordEnabled;
                if (_agentFireSwordData.ContainsKey(Agent.Main))
                {
                    AgentFireSwordData fireSwordData = _agentFireSwordData[Agent.Main];
                    fireSwordData.SetFireSwordEnable(_playerFireSwordEnabled);
                    fireSwordData.timer = null;
                }
            }
            //if (Input.IsKeyPressed(InputKey.K) && Agent.Main != null)
            //{
            //    MissionWeapon wieldedWeapon = Agent.Main.WieldedWeapon;
            //    MissionWeapon wieldedOffHandWeapon = Agent.Main.WieldedOffhandWeapon;
            //    if (!wieldedWeapon.IsEmpty)
            //    {
            //        InformationManager.DisplayMessage(new InformationMessage("wieldedWeapon : " + wieldedWeapon.PrimaryItem.ToString()));
            //        WeaponData ammoData =  wieldedWeapon.GetAmmoWeaponData();
            //        if(ammoData.IsValid())
            //            InformationManager.DisplayMessage(new InformationMessage("ammoData : " + ammoData.GetItemObject().ToString()));
            //    }
            //}
        }

        public override void OnScoreHit(Agent victim, Agent attacker, int affectorWeaponKind, bool isBlocked, float damage, float movementSpeedDamageModifier, float hitDistance, AgentAttackType attackType, float shotDifficulty, int weaponCurrentUsageIndex)
        {
            _agentFireSwordData.TryGetValue(attacker, out AgentFireSwordData fireSwordData);
            if (fireSwordData == null)
                return;
            if (FireLordConfig.IgniteTargetWithFireSword &&
                fireSwordData.enabled && victim != null && victim.IsHuman)
            {
                float firebarAdd = (isBlocked) ? FireLordConfig.IgnitionPerFireSwordHit / 2f : FireLordConfig.IgnitionPerFireSwordHit;
                _ignitionlogic.IncreaseAgentFireBar(attacker, victim, firebarAdd);
            }
        }

    }
}
