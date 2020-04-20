using FireLord.FireLord.Settings;
using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using static FireLord.FireArrowLogic;

namespace FireLord
{
    class FireSwordLogic : MissionLogic
    {
        private bool _initialized = false;
        private bool _fireSwordEnabled = false;

        private GameEntity _fireSwordEntity;
        private Light _fireSwordLight;

        public static bool DropLock = false;

        public void Initialize()
        {
            Agent.Main.OnMainAgentWieldedItemChange += OnMainAgentWieldedItemChange;
            Agent.Main.OnAgentHealthChanged += OnAgentHealthChanged;
        }


        public override void OnMissionTick(float dt)
        {
            if (!_initialized && Mission.Current != null && Agent.Main != null)
            {
                _initialized = true;
                Initialize();
            }

            if (Input.IsKeyPressed(FireLordConfig.FireSwordToggleKey))
            {
                SetFireSwordEnable(!_fireSwordEnabled);
            }
        }

        private void SetFireSwordEnable(bool enable)
        {
            if (Agent.Main==null)
                return;
            if (enable)
            {
                EquipmentIndex index = Agent.Main.GetWieldedItemIndex(Agent.HandIndex.MainHand);
                if (index == EquipmentIndex.None)
                    return;
                GameEntity wieldedWeaponEntity = Agent.Main.GetWeaponEntityFromEquipmentSlot(index);

                MissionWeapon wieldedWeapon = Agent.Main.WieldedWeapon;
                int length = wieldedWeapon.GetWeaponStatsData()[0].WeaponLength;
                int num = (int)Math.Round(length / 10f);
                Skeleton skeleton = Agent.Main.AgentVisuals.GetSkeleton();

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
                        int fireLenth = (num>19)? 9 :(num > 15)? 6 : (num > 12)? 5 :(num > 10) ? 4 : 3; 
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

                //只有通过扔掉再重新捡起这把武器，才能让粒子效果出现
                DropLock = true;
                Agent.Main.DropItem(index);
                SpawnedItemEntity spawnedItemEntity = wieldedWeaponEntity.GetFirstScriptOfType<SpawnedItemEntity>();
                Agent.Main.OnItemPickup(spawnedItemEntity, EquipmentIndex.None, out bool removeItem);
                DropLock = false;

                _fireSwordLight = light;
                _fireSwordEntity = wieldedWeaponEntity;
                _fireSwordEnabled = true;
            }
            else
            {
                _fireSwordEnabled = false;
                if (_fireSwordEntity != null)
                {
                    Skeleton skeleton = Agent.Main.AgentVisuals.GetSkeleton();
                    if (_fireSwordLight != null && skeleton != null)
                        skeleton.RemoveComponent(_fireSwordLight);
                    _fireSwordEntity.RemoveAllParticleSystems();
                }
            }
        }

        public void OnMainAgentWieldedItemChange()
        {
            if (!_fireSwordEnabled || DropLock)
                return;
            MissionWeapon wieldedWeapon = Agent.Main.WieldedWeapon;

            if (wieldedWeapon.Weapons.IsEmpty())
            {
                SetFireSwordEnable(false);
            }
        }

        public void OnAgentHealthChanged(Agent agent, float oldHealth, float newHealth)
        {
            if(newHealth <= 0)
            {
                SetFireSwordEnable(false);
            }
        }

        public override void OnScoreHit(Agent affectedAgent, Agent affectorAgent, int affectorWeaponKind, bool isBlocked, float damage, float movementSpeedDamageModifier, float hitDistance, AgentAttackType attackType, float shotDifficulty, int weaponCurrentUsageIndex)
        {
            if (FireLordConfig.IgniteTargetWithFireSword && 
                _fireSwordEnabled && affectorAgent == Agent.Main && affectedAgent != null && affectedAgent.IsHuman)
            {
                if (FireArrowLogic.AgentFireDatas.ContainsKey(affectedAgent))
                {
                    AgentFireData fireData = FireArrowLogic.AgentFireDatas[affectedAgent];
                    if (!fireData.isBurning)
                    {
                        fireData.firebar += (isBlocked) ? FireLordConfig.IgnitionPerFireSwordHit/2 : FireLordConfig.IgnitionPerFireSwordHit;
                        fireData.attacker = affectorAgent;
                    }
                }
                else
                {
                    AgentFireData fireData = new AgentFireData();
                    fireData.firebar += (isBlocked) ? FireLordConfig.IgnitionPerFireSwordHit / 2 : FireLordConfig.IgnitionPerFireSwordHit;
                    fireData.attacker = affectorAgent;
                    FireArrowLogic.AgentFireDatas.Add(affectedAgent, fireData);
                }
            }
        }

    }
}
