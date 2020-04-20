using ModLib;
using ModLib.Attributes;
using System.Xml.Serialization;
using TaleWorlds.InputSystem;

namespace FireLord.Settings
{
    public class FireLordSettingsEnglish : SettingsBase
    {
        public override string ModName => FireLordSubModule.ModName;
        public override string ModuleFolderName => FireLordSubModule.ModuleName;

        public const string InstanceID = "FireLordSettings";
        [XmlElement]
        public override string ID { get; set; } = InstanceID;

        public static FireLordSettings Instance
        {
            get => (FireLordSettings)SettingsDatabase.GetSettings(InstanceID);
        }

        [XmlElement]
        [SettingPropertyGroup("Fire Arrow Misc")]
        [SettingProperty("Enable Fire Arrow At Day", "All units will use fire arrows during day time.")]
        public bool UseFireArrowsAtDay
        {
            get => FireLordConfig.UseFireArrowsAtDay;
            set => FireLordConfig.UseFireArrowsAtDay = value;
        }

        [SettingPropertyGroup("Fire Arrow Misc")]
        [SettingProperty("Enable Fire Arrow At Night", "All units will use fire arrows during night time.")]
        [XmlElement]
        public bool UseFireArrowsAtNight
        {
            get => FireLordConfig.UseFireArrowsAtNight;
            set => FireLordConfig.UseFireArrowsAtNight = value;
        }

        [SettingPropertyGroup("Fire Arrow Misc")]
        [SettingProperty("Enable Fire Arrow At Siege", "All units will use fire arrows in a siege battle.")]
        [XmlElement]
        public bool UseFireArrowsAtSiege
        {
            get => FireLordConfig.UseFireArrowsAtSiege;
            set => FireLordConfig.UseFireArrowsAtSiege = value;
        }

        [SettingPropertyGroup("Fire Arrow Misc")]
        [SettingProperty("Thrown Weapon On Fire", "Whether to lit thrown weapons on fire as well.")]
        [XmlElement]
        public bool AllowFireThrownWeapon
        {
            get => FireLordConfig.AllowFireThrownWeapon;
            set => FireLordConfig.AllowFireThrownWeapon = value;
        }

        [SettingPropertyGroup("Fire Arrow Misc")]
        [SettingProperty("Sticked Arrow Burning Time", 0, 20, "For how long should sticked arrow's  fire remains before it goes out.")]
        [XmlElement]
        public int StickedArrowsBurningTime
        {
            get => (int)FireLordConfig.StickedArrowsBurningTime;
            set => FireLordConfig.StickedArrowsBurningTime = value;
        }

        [SettingPropertyGroup("Fire Arrow Lights")]
        [SettingProperty("Light Radius", 0, 20, "The radius of the point light attched to the arrow.")]
        [XmlElement]
        public int FireArrowLightRadius
        {
            get => (int)FireLordConfig.FireArrowLightRadius;
            set => FireLordConfig.FireArrowLightRadius = value;
        }

        [SettingPropertyGroup("Fire Arrow Lights")]
        [SettingProperty("Light Intensity", 0, 200, "The lighting intensity of the point light attched to the arrow.")]
        [XmlElement]
        public int FireArrowLightIntensity
        {
            get => (int)FireLordConfig.FireArrowLightIntensity;
            set => FireLordConfig.FireArrowLightIntensity = value;
        }


        [SettingPropertyGroup("Fire Sword Settings")]
        [SettingProperty("Toggle Key", 0, 255, "Key to toggle fire sword. DO NOT EDIT IT HERE. Try the FireLordConfig.ini.(Default C)")]
        [XmlElement]
        public int FireSwordToggleKey
        {
            get => (int)FireLordConfig.FireSwordToggleKey;
            set => FireLordConfig.FireSwordToggleKey = (InputKey)value;
        }


        [SettingPropertyGroup("Fire Sword Settings")]
        [SettingProperty("Light Radius", 0, 20, "The radius of the point light attched to the sword.")]
        [XmlElement]
        public int FireSwordLightRadius
        {
            get => (int)FireLordConfig.FireSwordLightRadius;
            set => FireLordConfig.FireSwordLightRadius = value;
        }

        [SettingPropertyGroup("Fire Sword Settings")]
        [SettingProperty("Light Intensity", 0, 200, "The lighting intensity of the point light attched to the sword.")]
        [XmlElement]
        public int FireSwordLightIntensity
        {
            get => (int)FireLordConfig.FireSwordLightIntensity;
            set => FireLordConfig.FireSwordLightIntensity = value;
        }


        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Allow Ignition With Fire Arrow", "Allow fire arrows to ignite enemies.")]
        [XmlElement]
        public bool IgniteTargetWithFireArrow
        {
            get => FireLordConfig.IgniteTargetWithFireArrow;
            set => FireLordConfig.IgniteTargetWithFireArrow = value;
        }

        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Allow Ignition With Fire Sword", "Allow fire sword to ignite enemies on hit.")]
        [XmlElement]
        public bool IgniteTargetWithFireSword
        {
            get => FireLordConfig.IgniteTargetWithFireSword;
            set => FireLordConfig.IgniteTargetWithFireSword = value;
        }

        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Ignition Bar Max", 0, 200, "One will burn when his ignition bar is filled.")]
        [XmlElement]
        public int IgnitionBarMax
        {
            get => (int)FireLordConfig.IgnitionBarMax;
            set => FireLordConfig.IgnitionBarMax = value;
        }


        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Ignition Per Fire Arrow", 0, 200, "How much will one's ignition bar raise when hit by a fire arrow.")]
        [XmlElement]
        public int IgnitionPerFireArrow
        {
            get => (int)FireLordConfig.IgnitionPerFireArrow;
            set => FireLordConfig.IgnitionPerFireArrow = value;
        }

        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Ignition Per Fire Sword Hit", 0, 200, "How much will one's ignition bar raise when hit by fire sword. Reduced by half when blocked.")]
        [XmlElement]
        public int IgnitionPerFireSwordHit
        {
            get => (int)FireLordConfig.IgnitionPerFireSwordHit;
            set => FireLordConfig.IgnitionPerFireSwordHit = value;
        }


        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Ignition Bar Drop Per Second", 0, 200, "How much will the ignition bar drop per second.")]
        [XmlElement]
        public int IgnitionDropPerSecond
        {
            get => (int)FireLordConfig.IgnitionDropPerSecond;
            set => FireLordConfig.IgnitionDropPerSecond = value;
        }

        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Ignition Duration", 0, 30, "For how many seconds will the target burns.")]
        [XmlElement]
        public int IgnitionDurationInSecond
        {
            get => (int)FireLordConfig.IgnitionDurationInSecond;
            set => FireLordConfig.IgnitionDurationInSecond = value;
        }
        
        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Light Radius", 0, 20, "The radius of the the point light attched to the burning target.")]
        [XmlElement]
        public int IgnitionLightRadius
        {
            get => (int)FireLordConfig.IgnitionLightRadius;
            set => FireLordConfig.IgnitionLightRadius = value;
        }

        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Light Intensity", 0, 200, "The lighting intensity of the the point light attched to the burning target.")]
        [XmlElement]
        public int IgnitionLightIntensity
        {
            get => (int)FireLordConfig.IgnitionLightIntensity;
            set => FireLordConfig.IgnitionLightIntensity = value;
        }

        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Enable Ignition Damage", "Whether to deal burning damages with ignition.")]
        [XmlElement]
        public bool IgnitionDealDamage
        {
            get => FireLordConfig.IgnitionDealDamage;
            set => FireLordConfig.IgnitionDealDamage = value;
        }

        [SettingPropertyGroup("Ignition Settings")]
        [SettingProperty("Ignition Damage Per Second", 0, 200, "The burning damage taken per second.")]
        [XmlElement]
        public int IgnitionDamagePerSecond
        {
            get => (int)FireLordConfig.IgnitionDamagePerSecond;
            set => FireLordConfig.IgnitionDamagePerSecond = value;
        }

        public void Init()
        {
            UseFireArrowsAtDay = FireLordConfig.UseFireArrowsAtDay;
            UseFireArrowsAtNight = FireLordConfig.UseFireArrowsAtNight;
            UseFireArrowsAtSiege = FireLordConfig.UseFireArrowsAtDay;
            AllowFireThrownWeapon = FireLordConfig.AllowFireThrownWeapon;
            StickedArrowsBurningTime = (int)FireLordConfig.StickedArrowsBurningTime;
            FireArrowLightRadius = (int)FireLordConfig.FireArrowLightRadius;
            FireArrowLightIntensity = (int)FireLordConfig.FireArrowLightIntensity;
            FireSwordToggleKey = (int)FireLordConfig.FireSwordToggleKey;
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
            IgnitionDamagePerSecond = (int)FireLordConfig.IgnitionDamagePerSecond;
        }
    }
}
