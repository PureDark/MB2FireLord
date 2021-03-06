﻿using MBOptionScreen.Attributes;
using MBOptionScreen.Attributes.v2;
using MBOptionScreen.Data;
using MBOptionScreen.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace FireLord.Settings
{
    public class FireLordSettings :  AttributeSettings<FireLordSettings>
    {
        public const string InstanceID = "FireLordSettings";
        public override string Id { get; set; } = InstanceID;
        public override string ModName => "Fire Lord";
        public override string ModuleFolderName => FireLordSubModule.ModuleName;
        //public override string Format => "firelord";

        //[Version("e1.0.0", 200)]
        //[Version("e1.0.1", 200)]
        //[Version("e1.0.2", 200)]
        //[Version("e1.0.3", 200)]
        //[Version("e1.0.4", 200)]
        //[Version("e1.0.5", 200)]
        //[Version("e1.0.6", 200)]
        //[Version("e1.0.7", 200)]
        //[Version("e1.0.8", 200)]
        //[Version("e1.0.9", 200)]
        //[Version("e1.0.10", 200)]
        //[Version("e1.0.11", 200)]
        //[Version("e1.1.0", 200)]
        //[Version("e1.2.0", 200)]
        //[Version("e1.2.1", 200)]
        //[Version("e1.3.0", 200)]

        //public class FireLordFormat : ISettingsFormat
        //{
        //    public IEnumerable<string> Extensions => new string[] { "firelord" };

        //    public SettingsBase Load(SettingsBase settings, string path)
        //    {
        //        if (settings is FireLordSettings fireLordSettings)
        //            fireLordSettings.Load();
        //        return settings;
        //    }

        //    public bool Save(SettingsBase settings, string path)
        //    {
        //        if (settings is FireLordSettings fireLordSettings)
        //            fireLordSettings.Save();
        //        return true;
        //    }
        //}


        private DefaultDropdown<InputKey> _fireArrowToggleKey = GetDropdownOptions<InputKey>((int)InputKey.V);

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_gerneral}General", Order = 1)]
        [SettingPropertyDropdown("{=FireLord_menu_fire_arrow_toggle_key}Toggle Key", Order = 1, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_arrow_toggle_key_hint}Key to toggle fire arrows.")]
        public DefaultDropdown<InputKey> FireArrowToggleKey
        {
            get => _fireArrowToggleKey;
            set
            {
                _fireArrowToggleKey = value;
                FireLordConfig.FireArrowToggleKey = value.SelectedValue;
            }
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_gerneral}General", Order = 1)]
        [SettingPropertyInteger("{=FireLord_menu_fire_arrow_allowed_time_start}Fire Arrow Allowed Time Start", 0, 24, Order = 2, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_arrow_allowed_time_start_hint}Starting hour of fire arrow allowed time interval. E.g. Start=6 and End=18 for enabling at day.")]
        public int FireArrowAllowedTimeStart
        {
            get => FireLordConfig.FireArrowAllowedTimeStart;
            set => FireLordConfig.FireArrowAllowedTimeStart = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_gerneral}General", Order = 1)]
        [SettingPropertyInteger("{=FireLord_menu_fire_arrow_allowed_time_end}Fire Arrow Allowed Time End", 0, 24, Order = 3, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_arrow_allowed_time_end_hint}Ending hour of fire arrow allowed time interval. E.g. Start=20 and End=6 for enabling at night.")]
        public int FireArrowAllowedTimeEnd
        {
            get => FireLordConfig.FireArrowAllowedTimeEnd;
            set => FireLordConfig.FireArrowAllowedTimeEnd = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_gerneral}General", Order = 1)]
        [SettingPropertyBool("{=FireLord_menu_use_fire_arrow_only_in_siege}Enable Fire Arrow Only In Siege", Order = 4, RequireRestart = false,
            HintText = "{=FireLord_menu_use_fire_arrow_only_in_siege_hint}Only allow units to use fire arrows in a siege battle.")]
        public bool UseFireArrowsOnlyInSiege
        {
            get => FireLordConfig.UseFireArrowsOnlyInSiege;
            set => FireLordConfig.UseFireArrowsOnlyInSiege = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_gerneral}General", Order = 1)]
        [SettingPropertyBool("{=FireLord_menu_allow_fire_thrown_weapon}Thrown Weapon On Fire", Order = 5, RequireRestart = false,
            HintText = "{=FireLord_menu_allow_fire_thrown_weapon_hint}Whether to light thrown weapons on fire as well.")]
        public bool AllowFireThrownWeapon
        {
            get => FireLordConfig.AllowFireThrownWeapon;
            set => FireLordConfig.AllowFireThrownWeapon = value;
        }

        private DefaultDropdown<FireLordConfig.UnitType> _fireArrowAllowedUnitType = GetDropdownOptions<FireLordConfig.UnitType>((int)FireLordConfig.FireArrowAllowedUnitType);

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_gerneral}General", Order = 1)]
        [SettingPropertyDropdown("{=FireLord_menu_fire_arrow_allowed_unit_type}Fire Arrow Allowed Units", Order = 6, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_arrow_allowed_unit_type_hint}What type of units are allowed to use fire arrows.")]
        public DefaultDropdown<FireLordConfig.UnitType> FireArrowAllowedUnitType
        {
            get => _fireArrowAllowedUnitType;
            set
            {
                _fireArrowAllowedUnitType = value;
                FireLordConfig.FireArrowAllowedUnitType = value.SelectedValue;
            }
        }

        private DefaultDropdown<FireLordConfig.WhitelistType> _fireArrowWhitelistType = GetDropdownOptions<FireLordConfig.WhitelistType>((int)FireLordConfig.FireArrowWhitelistType);

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_gerneral}General", Order = 1)]
        [SettingPropertyDropdown("{=FireLord_menu_fire_arrow_whitelist_type}Fire Arrow Whitelist Type", Order = 7, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_arrow_whitelist_type_hint}Whitelist type, Disabled=No whitelist. This is extra to allowed units.")]
        public DefaultDropdown<FireLordConfig.WhitelistType> FireArrowWhitelistType
        {
            get => _fireArrowWhitelistType;
            set
            {
                _fireArrowWhitelistType = value;
                FireLordConfig.FireArrowWhitelistType = value.SelectedValue;
            }
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_gerneral}General", Order = 1)]
        [SettingPropertyText("{=FireLord_menu_fire_arrow_troops_whitelist}Fire Arrow Troops Whitelist", Order = 8, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_arrow_troops_whitelist_hint}A list of troops who is allowed to use fire arrows. Add 'main_hero' as player.")]
        public string FireArrowTroopsWhitelist
        {
            get => string.Join(",", FireLordConfig.FireArrowTroopsWhitelist);
            set => FireLordConfig.FireArrowTroopsWhitelist = new List<string>(value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_gerneral}General", Order = 1)]
        [SettingPropertyText("{=FireLord_menu_fire_arrow_items_whitelist}Fire Arrow Items Whitelist", Order = 8, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_arrow_troops_whitelist_hint}A list of items which is recognized as fire arrows. Supports both bows and arrows.")]
        public string FireArrowItemsWhitelist
        {
            get => string.Join(",", FireLordConfig.FireArrowItemsWhitelist);
            set => FireLordConfig.FireArrowItemsWhitelist = new List<string>(value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_numerical}Numerical Setup", Order = 2)]
        [SettingPropertyFloatingInteger("{=FireLord_menu_chances_of_fire_arrow}Probability Of Fire Arrow", 0, 1, "0%", Order = 1, RequireRestart = false,
            HintText = "{=FireLord_menu_chances_of_fire_arrow_hint}Percent probability of shooting a fire arrow, does not affect the player.")]
        public float ChancesOfFireArrow
        {
            get => FireLordConfig.ChancesOfFireArrow/100f;
            set => FireLordConfig.ChancesOfFireArrow = value*100;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_numerical}Numerical Setup", Order = 2)]
        [SettingPropertyInteger("{=FireLord_menu_sticked_arrow_burning_time}Sticked Arrow Burning Time", 0, 20, "0s", Order = 2, RequireRestart = false,
            HintText = "{=FireLord_menu_sticked_arrow_burning_time_hint}For how long should sticked arrow's  fire remains before it goes out.")]
        public int StickedArrowsBurningTime
        {
            get => (int)FireLordConfig.StickedArrowsBurningTime;
            set => FireLordConfig.StickedArrowsBurningTime = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_lights}Lights", Order = 3)]
        [SettingPropertyInteger("{=FireLord_menu_fire_arrow_light_radius}Light Radius", 0, 20, Order = 1, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_arrow_light_radius_hint}The radius of the point light attched to the arrow.")]
        public int FireArrowLightRadius
        {
            get => (int)FireLordConfig.FireArrowLightRadius;
            set => FireLordConfig.FireArrowLightRadius = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_settings}Fire Arrow Settings/{=FireLord_menu_fire_arrow_lights}Lights", Order = 3)]
        [SettingPropertyInteger("{=FireLord_menu_fire_arrow_light_intensity}Light Intensity", 0, 200, Order = 2, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_arrow_light_intensity_hint}The lighting intensity of the point light attched to the arrow.")]
        public int FireArrowLightIntensity
        {
            get => (int)FireLordConfig.FireArrowLightIntensity;
            set => FireLordConfig.FireArrowLightIntensity = value;
        }


        private DefaultDropdown<InputKey> _fireSwordToggleKey = GetDropdownOptions<InputKey>((int)InputKey.C);

        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}Fire Sword Settings/{=FireLord_menu_fire_sword_gerneral}General", Order = 4)]
        [SettingPropertyDropdown("{=FireLord_menu_fire_sword_toggle_key}Toggle Key", Order = 1, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_sword_toggle_key_hint}Key to toggle your own fire sword.")]
        public DefaultDropdown<InputKey> FireSwordToggleKey
        {
            get => _fireSwordToggleKey;
            set
            {
                _fireSwordToggleKey = value;
                FireLordConfig.FireSwordToggleKey = value.SelectedValue;
            }
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}Fire Sword Settings/{=FireLord_menu_fire_sword_gerneral}General", Order = 4)]
        [SettingPropertyBool("{=FireLord_menu_player_fire_sword_default_on}Player Fire Sword Default On", Order = 2, RequireRestart = false,
            HintText = "{=FireLord_menu_player_fire_sword_default_on_hint}Ignite Player's weapon by default when starting a battle.")]
        public bool PlayerFireSwordDefaultOn
        {
            get => FireLordConfig.PlayerFireSwordDefaultOn;
            set => FireLordConfig.PlayerFireSwordDefaultOn = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}Fire Sword Settings/{=FireLord_menu_fire_sword_gerneral}General", Order = 4)]
        [SettingPropertyBool("{=FireLord_menu_ignite_player_body}Ignite Player Body (No Damage)", Order = 3, RequireRestart = false,
            HintText = "{=FireLord_menu_ignite_player_body_hint}Whether to light player's body when fire sword activated, only visuals and no damages.")]
        public bool IgnitePlayerBody
        {
            get => FireLordConfig.IgnitePlayerBody;
            set => FireLordConfig.IgnitePlayerBody = value;
        }
        

        private DefaultDropdown<FireLordConfig.UnitType> _fireSwordAllowedUnitType = GetDropdownOptions<FireLordConfig.UnitType>((int)FireLordConfig.FireSwordAllowedUnitType);

        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}Fire Sword Settings/{=FireLord_menu_fire_sword_gerneral}General", Order = 4)]
        [SettingPropertyDropdown("{=FireLord_menu_fire_sword_allowed_unit_type}Fire Sword Allowed Units", Order = 4, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_sword_allowed_unit_type_hint}What type of units are allowed to use fire weapons.")]
        public DefaultDropdown<FireLordConfig.UnitType> FireSwordAllowedUnitType
        {
            get => _fireSwordAllowedUnitType;
            set
            {
                _fireSwordAllowedUnitType = value;
                FireLordConfig.FireSwordAllowedUnitType = value.SelectedValue;
            }
        }

        private DefaultDropdown<FireLordConfig.WhitelistType> _fireSwordWhitelistType = GetDropdownOptions<FireLordConfig.WhitelistType>((int)FireLordConfig.FireArrowWhitelistType);

        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}Fire Sword Settings/{=FireLord_menu_fire_sword_gerneral}General", Order = 4)]
        [SettingPropertyDropdown("{=FireLord_menu_fire_sword_whitelist_type}Fire Arrow Whitelist Type", Order = 5, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_sword_whitelist_type_hint}Whitelist type, Disabled=No whitelist. This is extra to allowed units.")]
        public DefaultDropdown<FireLordConfig.WhitelistType> FireSwordWhitelistType
        {
            get => _fireSwordWhitelistType;
            set
            {
                _fireSwordWhitelistType = value;
                FireLordConfig.FireSwordWhitelistType = value.SelectedValue;
            }
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}Fire Sword Settings/{=FireLord_menu_fire_sword_gerneral}General", Order = 4)]
        [SettingPropertyText("{=FireLord_menu_fire_sword_troops_whitelist}Fire Sword Troops Whitelist", Order = 6, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_sword_troops_whitelist_hint}A list of troops who is allowed to use fire weapon.")]
        public string FireSwordTroopsWhitelist
        {
            get => string.Join(",", FireLordConfig.FireSwordTroopsWhitelist);
            set => FireLordConfig.FireSwordTroopsWhitelist = new List<string>(value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}Fire Sword Settings/{=FireLord_menu_fire_sword_gerneral}General", Order = 4)]
        [SettingPropertyText("{=FireLord_menu_fire_sword_items_whitelist}Fire Sword Items Whitelist", Order = 7, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_sword_troops_whitelist_hint}A list of items which is recognized as fire weapons. Supports both all types of melee weapons.")]
        public string FireSwordItemsWhitelist
        {
            get => string.Join(",", FireLordConfig.FireSwordItemsWhitelist);
            set => FireLordConfig.FireSwordItemsWhitelist = new List<string>(value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        }


        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}Fire Sword Settings/{=FireLord_menu_fire_sword_lights}Lights", Order = 5)]
        [SettingPropertyInteger("{=FireLord_menu_fire_sword_light_radius}Light Radius", 0, 20, Order = 1, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_sword_light_radius_hint}The radius of the point light attched to the sword.")]
        public int FireSwordLightRadius
        {
            get => (int)FireLordConfig.FireSwordLightRadius;
            set => FireLordConfig.FireSwordLightRadius = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}Fire Sword Settings/{=FireLord_menu_fire_sword_lights}Lights", Order = 5)]
        [SettingPropertyInteger("{=FireLord_menu_fire_sword_light_intensity}Light Intensity", 0, 200, Order = 2, RequireRestart = false,
            HintText = "{=FireLord_menu_fire_sword_light_intensity_hint}The lighting intensity of the point light attched to the sword.")]
        public int FireSwordLightIntensity
        {
            get => (int)FireLordConfig.FireSwordLightIntensity;
            set => FireLordConfig.FireSwordLightIntensity = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_gerneral}General", Order = 6)]
        [SettingPropertyBool("{=FireLord_menu_ignite_target_with_fire_arrow}Allow Ignition With Fire Arrow", Order = 1, RequireRestart = false,
            HintText = "{=FireLord_menu_ignite_target_with_fire_arrow_hint}Allow fire arrows to ignite enemies.")]
        public bool IgniteTargetWithFireArrow
        {
            get => FireLordConfig.IgniteTargetWithFireArrow;
            set => FireLordConfig.IgniteTargetWithFireArrow = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_gerneral}General", Order = 6)]
        [SettingPropertyBool("{=FireLord_menu_ignite_target_with_fire_sword}Allow Ignition With Fire Sword", Order = 2, RequireRestart = false,
        HintText = "{=FireLord_menu_ignite_target_with_fire_sword_hint}Allow fire sword to ignite enemies on hit.")]
        public bool IgniteTargetWithFireSword
        {
            get => FireLordConfig.IgniteTargetWithFireSword;
            set => FireLordConfig.IgniteTargetWithFireSword = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_bar}Ignition Bar", Order = 7)]
        [SettingPropertyInteger("{=FireLord_menu_ignition_bar_max}Ignition Bar Max", 0, 200, Order = 1, RequireRestart = false,
            HintText = "{=FireLord_menu_ignition_bar_max_hint}One will burn when his ignition bar is filled.")]
        public int IgnitionBarMax
        {
            get => (int)FireLordConfig.IgnitionBarMax;
            set => FireLordConfig.IgnitionBarMax = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_bar}Ignition Bar", Order = 7)]
        [SettingPropertyInteger("{=FireLord_menu_ignition_per_fire_arrow}Ignition Per Fire Arrow", 0, 200, Order = 2, RequireRestart = false,
            HintText = "{=FireLord_menu_ignition_per_fire_arrow_hint}How much will one's ignition bar raise when hit by a fire arrow. Reduced by half when blocked.")]
        public int IgnitionPerFireArrow
        {
            get => (int)FireLordConfig.IgnitionPerFireArrow;
            set => FireLordConfig.IgnitionPerFireArrow = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_bar}Ignition Bar", Order = 7)]
        [SettingPropertyInteger("{=FireLord_menu_ignition_per_fire_sword_hit}Ignition Per Fire Sword Hit", 0, 200, Order = 3, RequireRestart = false,
            HintText = "{=FireLord_menu_ignition_per_fire_sword_hit_hint}How much will one's ignition bar raise when hit by fire sword. Reduced by half when blocked.")]
        public int IgnitionPerFireSwordHit
        {
            get => (int)FireLordConfig.IgnitionPerFireSwordHit;
            set => FireLordConfig.IgnitionPerFireSwordHit = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_bar}Ignition Bar", Order = 7)]
        [SettingPropertyInteger("{=FireLord_menu_ignition_drop_per_second}Ignition Bar Drop Per Second", 0, 200, Order = 4, RequireRestart = false,
            HintText = "{=FireLord_menu_ignition_drop_per_second_hint}How much will the ignition bar drop per second.")]
        public int IgnitionDropPerSecond
        {
            get => (int)FireLordConfig.IgnitionDropPerSecond;
            set => FireLordConfig.IgnitionDropPerSecond = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_damages}Damages", Order = 8)]
        [SettingPropertyBool("{=FireLord_menu_ignition_deal_damage}Enable Ignition Damage", Order = 1, RequireRestart = false,
            HintText = "{=FireLord_menu_ignition_deal_damage_hint}Whether to deal burning damages with ignition.")]
        public bool IgnitionDealDamage
        {
            get => FireLordConfig.IgnitionDealDamage;
            set => FireLordConfig.IgnitionDealDamage = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_damages}Damages", Order = 8)]
        [SettingPropertyBool("{=FireLord_menu_ignition_Friendly_Fire}Enable Friendly Fire", Order = 2, RequireRestart = false,
            HintText = "{=FireLord_menu_ignition_Friendly_Fire_hint}Whether to enable ignition by friendly fire.")]
        public bool IgnitionFriendlyFire
        {
            get => FireLordConfig.IgnitionFriendlyFire;
            set => FireLordConfig.IgnitionFriendlyFire = value;
        }


        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_damages}Damages", Order = 8)]
        [SettingPropertyInteger("{=FireLord_menu_ignition_duration}Ignition Duration", 0, 30, "0s", Order = 3, RequireRestart = false,
            HintText = "{=FireLord_menu_ignition_duration_hint}For how many seconds will the target burns.")]
        public int IgnitionDurationInSecond
        {
            get => (int)FireLordConfig.IgnitionDurationInSecond;
            set => FireLordConfig.IgnitionDurationInSecond = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_damages}Damages", Order = 8)]
        [SettingPropertyInteger("{=FireLord_menu_ignition_damage_per_second}Ignition Damage Per Second", 0, 200, Order = 4, RequireRestart = false,
            HintText = "{=FireLord_menu_ignition_damage_per_second_hint}The burning damage taken per second.")]
        public int IgnitionDamagePerSecond
        {
            get => (int)FireLordConfig.IgnitionDamagePerSecond;
            set => FireLordConfig.IgnitionDamagePerSecond = value;
        }


        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_lights}Lights", Order = 9)]
        [SettingPropertyInteger("{=FireLord_menu_ignition_light_radius}Light Radius", 0, 20, Order = 1, RequireRestart = false,
            HintText = "{=FireLord_menu_ignition_light_radius_hint}The radius of the the point light attched to the burning target.")]
        public int IgnitionLightRadius
        {
            get => (int)FireLordConfig.IgnitionLightRadius;
            set => FireLordConfig.IgnitionLightRadius = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}Ignition Settings/{=FireLord_menu_ignition_lights}Lights", Order = 9)]
        [SettingPropertyInteger("{=FireLord_menu_ignition_light_intensity}Light Intensity", 0, 200, Order = 2, RequireRestart = false,
            HintText = "{=FireLord_menu_ignition_light_intensity_hint}The lighting intensity of the the point light attched to the burning target.")]
        public int IgnitionLightIntensity
        {
            get => (int)FireLordConfig.IgnitionLightIntensity;
            set => FireLordConfig.IgnitionLightIntensity = value;
        }

        public void Save()
        {
            FireLordConfig.FireArrowToggleKey = FireArrowToggleKey.SelectedValue;
            FireLordConfig.FireArrowAllowedTimeStart = FireArrowAllowedTimeStart;
            FireLordConfig.FireArrowAllowedTimeEnd = FireArrowAllowedTimeEnd;
            FireLordConfig.UseFireArrowsOnlyInSiege = UseFireArrowsOnlyInSiege;
            FireLordConfig.AllowFireThrownWeapon = AllowFireThrownWeapon;

            FireLordConfig.FireArrowAllowedUnitType = FireArrowAllowedUnitType.SelectedValue;
            FireLordConfig.FireArrowWhitelistType = FireArrowWhitelistType.SelectedValue;
            FireLordConfig.FireArrowTroopsWhitelist = new List<string>(FireArrowTroopsWhitelist.Split(','));
            FireLordConfig.FireArrowItemsWhitelist = new List<string>(FireArrowItemsWhitelist.Split(','));

            FireLordConfig.ChancesOfFireArrow = ChancesOfFireArrow * 100f;
            FireLordConfig.StickedArrowsBurningTime = StickedArrowsBurningTime;
            FireLordConfig.FireArrowLightRadius = FireArrowLightRadius;
            FireLordConfig.FireArrowLightIntensity = FireArrowLightIntensity;

            FireLordConfig.FireSwordToggleKey = FireSwordToggleKey.SelectedValue;
            FireLordConfig.PlayerFireSwordDefaultOn = PlayerFireSwordDefaultOn;
            FireLordConfig.IgnitePlayerBody = IgnitePlayerBody;

            FireLordConfig.FireSwordAllowedUnitType = FireSwordAllowedUnitType.SelectedValue;
            FireLordConfig.FireSwordWhitelistType = FireSwordWhitelistType.SelectedValue;
            FireLordConfig.FireSwordTroopsWhitelist = new List<string>(FireSwordTroopsWhitelist.Split(','));
            FireLordConfig.FireSwordItemsWhitelist = new List<string>(FireSwordItemsWhitelist.Split(','));

            FireLordConfig.FireSwordLightRadius = FireSwordLightRadius;
            FireLordConfig.FireSwordLightIntensity = FireSwordLightIntensity;
            FireLordConfig.IgniteTargetWithFireArrow = IgniteTargetWithFireArrow;
            FireLordConfig.IgniteTargetWithFireSword = IgniteTargetWithFireSword;
            FireLordConfig.IgnitionBarMax = IgnitionBarMax;
            FireLordConfig.IgnitionPerFireArrow = IgnitionPerFireArrow;
            FireLordConfig.IgnitionPerFireSwordHit = IgnitionPerFireSwordHit;
            FireLordConfig.IgnitionDropPerSecond = IgnitionDropPerSecond;
            FireLordConfig.IgnitionDurationInSecond = IgnitionDurationInSecond;
            FireLordConfig.IgnitionLightRadius = IgnitionLightRadius;
            FireLordConfig.IgnitionLightIntensity = IgnitionLightIntensity;
            FireLordConfig.IgnitionDealDamage = IgnitionDealDamage;
            FireLordConfig.IgnitionFriendlyFire = IgnitionFriendlyFire;
            FireLordConfig.IgnitionDamagePerSecond = IgnitionDamagePerSecond;
        }

        public void Load()
        {
            for (int i = 0; i < FireArrowToggleKey.Count; i++)
            {
                if (FireArrowToggleKey[i] == FireLordConfig.FireArrowToggleKey)
                {
                    FireArrowToggleKey.SelectedIndex = i;
                }
            }
            FireArrowAllowedTimeStart = FireLordConfig.FireArrowAllowedTimeStart;
            FireArrowAllowedTimeEnd = FireLordConfig.FireArrowAllowedTimeEnd;
            UseFireArrowsOnlyInSiege = FireLordConfig.UseFireArrowsOnlyInSiege;
            AllowFireThrownWeapon = FireLordConfig.AllowFireThrownWeapon;
            FireArrowAllowedUnitType.SelectedIndex = (int)FireLordConfig.FireArrowAllowedUnitType;
            FireArrowWhitelistType.SelectedIndex = (int)FireLordConfig.FireArrowWhitelistType;
            FireArrowTroopsWhitelist = string.Join(",", FireLordConfig.FireArrowTroopsWhitelist);
            FireArrowItemsWhitelist = string.Join(",", FireLordConfig.FireArrowItemsWhitelist);

            ChancesOfFireArrow = FireLordConfig.ChancesOfFireArrow / 100f;
            StickedArrowsBurningTime = (int)FireLordConfig.StickedArrowsBurningTime;
            FireArrowLightRadius = (int)FireLordConfig.FireArrowLightRadius;
            FireArrowLightIntensity = (int)FireLordConfig.FireArrowLightIntensity;

            for (int i = 0; i < FireSwordToggleKey.Count; i++)
            {
                if (FireSwordToggleKey[i] == FireLordConfig.FireSwordToggleKey)
                {
                    FireSwordToggleKey.SelectedIndex = i;
                }
            }

            PlayerFireSwordDefaultOn = FireLordConfig.PlayerFireSwordDefaultOn;
            IgnitePlayerBody = FireLordConfig.IgnitePlayerBody;

            FireSwordAllowedUnitType.SelectedIndex = (int)FireLordConfig.FireSwordAllowedUnitType;
            FireSwordWhitelistType.SelectedIndex = (int)FireLordConfig.FireSwordWhitelistType;
            FireSwordTroopsWhitelist = string.Join(",", FireLordConfig.FireSwordTroopsWhitelist);
            FireSwordItemsWhitelist = string.Join(",", FireLordConfig.FireSwordItemsWhitelist);

            FireSwordLightRadius = (int)FireLordConfig.FireSwordLightRadius;
            FireSwordLightIntensity = (int)FireLordConfig.FireSwordLightIntensity;
            IgniteTargetWithFireArrow = FireLordConfig.IgniteTargetWithFireArrow;
            IgniteTargetWithFireSword = FireLordConfig.IgniteTargetWithFireSword;
            IgnitionBarMax = (int)FireLordConfig.IgnitionBarMax;
            IgnitionPerFireArrow = (int)FireLordConfig.IgnitionPerFireArrow;
            IgnitionPerFireSwordHit = (int)FireLordConfig.IgnitionPerFireSwordHit;
            IgnitionDropPerSecond = (int)FireLordConfig.IgnitionDropPerSecond;
            IgnitionDurationInSecond = (int)FireLordConfig.IgnitionDurationInSecond;
            IgnitionLightRadius = (int)FireLordConfig.IgnitionLightRadius;
            IgnitionLightIntensity = (int)FireLordConfig.IgnitionLightIntensity;
            IgnitionDealDamage = FireLordConfig.IgnitionDealDamage;
            IgnitionFriendlyFire = FireLordConfig.IgnitionFriendlyFire;
            IgnitionDamagePerSecond = FireLordConfig.IgnitionDamagePerSecond;
        }

        public FireLordSettings() : base()
        {
            FireLordConfig.Init();
            FireLordSubModule.LoadSettingsTimer = new Timer(MBCommon.GetTime(MBCommon.TimeType.Application), 3, false);
        }

        private static DefaultDropdown<T> GetDropdownOptions<T>(int selectedIndex)
        {
            List<T> enumList = new List<T>();
            foreach (T t in Enum.GetValues(typeof(T)))
            {
                enumList.Add(t);
            }
            return new DefaultDropdown<T>(enumList, selectedIndex);
        }
    }
}
