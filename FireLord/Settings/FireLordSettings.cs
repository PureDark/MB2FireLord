using ModLib;
using ModLib.Attributes;
using System.Xml.Serialization;
using TaleWorlds.InputSystem;

namespace FireLord.Settings
{
    public class FireLordSettings : SettingsBase
    {
        public override string ModName => "火焰领主";
        public override string ModuleFolderName => FireLordSubModule.ModuleName;

        public const string InstanceID = "FireLordSettings";
        [XmlElement]
        public override string ID { get; set; } = InstanceID;

        public static FireLordSettings Instance
        {
            get => (FireLordSettings)SettingsDatabase.GetSettings(InstanceID);
        }

        [SettingPropertyGroup("火箭综合设置")]
        [SettingProperty("允许火箭的单位类型", 0, 6, "允许使用火箭的单位类型，0=不允许，1=玩家，2=英雄单位，3=NPC队友，4=己方，5=敌方，6=所有人")]
        [XmlElement]
        public int AllowedUnitType
        {
            get => (int)FireLordConfig.AllowedUnitType;
            set => FireLordConfig.AllowedUnitType = (FireLordConfig.UnitType)value;
        }

        [SettingPropertyGroup("火箭综合设置")]
        [SettingProperty("白天使用火箭", "在白天的战斗中使用火焰箭")]
        [XmlElement]
        public bool UseFireArrowsAtDay
        {
            get => FireLordConfig.UseFireArrowsAtDay;
            set => FireLordConfig.UseFireArrowsAtDay = value;
        }

        [SettingPropertyGroup("火箭综合设置")]
        [SettingProperty("夜晚使用火箭", "在夜晚的战斗中使用火焰箭")]
        [XmlElement]
        public bool UseFireArrowsAtNight
        {
            get => FireLordConfig.UseFireArrowsAtNight;
            set => FireLordConfig.UseFireArrowsAtNight = value;
        }

        [SettingPropertyGroup("火箭综合设置")]
        [SettingProperty("围城使用火箭", "在围城的战斗中使用火焰箭")]
        [XmlElement]
        public bool UseFireArrowsAtSiege
        {
            get => FireLordConfig.UseFireArrowsAtSiege;
            set => FireLordConfig.UseFireArrowsAtSiege = value;
        }

        [SettingPropertyGroup("火箭综合设置")]
        [SettingProperty("投掷武器点燃", "是否让投掷武器也拥有燃烧效果")]
        [XmlElement]
        public bool AllowFireThrownWeapon
        {
            get => FireLordConfig.AllowFireThrownWeapon;
            set => FireLordConfig.AllowFireThrownWeapon = value;
        }

        [SettingPropertyGroup("火箭综合设置")]
        [SettingProperty("火箭发生概率", 0, 100, "弓箭手射箭时产生火箭的百分比概率，不影响玩家")]
        [XmlElement]
        public int ChancesOfFireArrow
        {
            get => (int)FireLordConfig.ChancesOfFireArrow;
            set => FireLordConfig.ChancesOfFireArrow = value;
        }

        [SettingPropertyGroup("火箭综合设置")]
        [SettingProperty("火箭火焰残留时间", 0, 20, "火箭射中后继续燃烧的时间，这段时间内火焰会逐渐熄灭")]
        [XmlElement]
        public int StickedArrowsBurningTime
        {
            get => (int)FireLordConfig.StickedArrowsBurningTime;
            set => FireLordConfig.StickedArrowsBurningTime = value;
        }

        [SettingPropertyGroup("火箭光源设置")]
        [SettingProperty("火箭光照范围", 0, 20, "火箭的光源照射的半径")]
        [XmlElement]
        public int FireArrowLightRadius
        {
            get => (int)FireLordConfig.FireArrowLightRadius;
            set => FireLordConfig.FireArrowLightRadius = value;
        }

        [SettingPropertyGroup("火箭光源设置")]
        [SettingProperty("火箭光照强度", 0, 200, "火箭的光源强度，决定亮度")]
        [XmlElement]
        public int FireArrowLightIntensity
        {
            get => (int)FireLordConfig.FireArrowLightIntensity;
            set => FireLordConfig.FireArrowLightIntensity = value;
        }


        [SettingPropertyGroup("火焰剑设置")]
        [SettingProperty("火焰剑切换键", 0, 255, "开启/关闭 火焰剑的热键，菜单暂时不支持下拉框，最好不要修改，可以在FireLordConfig.ini中设置（默认：C）")]
        [XmlElement]
        public int FireSwordToggleKey
        {
            get => (int)FireLordConfig.FireSwordToggleKey;
            set => FireLordConfig.FireSwordToggleKey = (InputKey)value;
        }

        [SettingPropertyGroup("火焰剑设置")]
        [SettingProperty("点燃玩家身体（无伤害））", "是否在开启火焰剑的同时点燃玩家的身体，只有视觉效果，不造成伤害")]
        [XmlElement]
        public bool IgnitePlayerBody
        {
            get => FireLordConfig.IgnitePlayerBody;
            set => FireLordConfig.IgnitePlayerBody = value;
        }

        [SettingPropertyGroup("火焰剑设置")]
        [SettingProperty("火焰剑光照范围", 0, 20, "火焰剑的光源照射的半径")]
        [XmlElement]
        public int FireSwordLightRadius
        {
            get => (int)FireLordConfig.FireSwordLightRadius;
            set => FireLordConfig.FireSwordLightRadius = value;
        }

        [SettingPropertyGroup("火焰剑设置")]
        [SettingProperty("火焰剑光照强度", 0, 200, "火焰剑的光源强度，决定亮度")]
        [XmlElement]
        public int FireSwordLightIntensity
        {
            get => (int)FireLordConfig.FireSwordLightIntensity;
            set => FireLordConfig.FireSwordLightIntensity = value;
        }
        

        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("允许火箭点燃敌人", "是否允许使用火箭点燃敌人（击中友军也能点燃）")]
        [XmlElement]
        public bool IgniteTargetWithFireArrow
        {
            get => FireLordConfig.IgniteTargetWithFireArrow;
            set => FireLordConfig.IgniteTargetWithFireArrow = value;
        }

        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("允许火焰剑点燃敌人", "是否允许使用火焰剑点燃敌人")]
        [XmlElement]
        public bool IgniteTargetWithFireSword
        {
            get => FireLordConfig.IgniteTargetWithFireSword;
            set => FireLordConfig.IgniteTargetWithFireSword = value;
        }

        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("点燃槽上限", 0, 200, "当点燃槽满了的时候，点燃这名敌人")]
        [XmlElement]
        public int IgnitionBarMax
        {
            get => (int)FireLordConfig.IgnitionBarMax;
            set => FireLordConfig.IgnitionBarMax = value;
        }


        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("火箭每箭点燃值", 0, 200, "每只火箭击中敌人时，增加的点燃槽数值")]
        [XmlElement]
        public int IgnitionPerFireArrow
        {
            get => (int)FireLordConfig.IgnitionPerFireArrow;
            set => FireLordConfig.IgnitionPerFireArrow = value;
        }

        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("火焰剑每击点燃值", 0, 200, "火焰剑每次击中敌人时，增加的点燃槽数值（被格挡时减半）")]
        [XmlElement]
        public int IgnitionPerFireSwordHit
        {
            get => (int)FireLordConfig.IgnitionPerFireSwordHit;
            set => FireLordConfig.IgnitionPerFireSwordHit = value;
        }


        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("每秒点燃值降低", 0, 200, "点燃槽随时间自动减少，每秒降低的数值")]
        [XmlElement]
        public int IgnitionDropPerSecond
        {
            get => (int)FireLordConfig.IgnitionDropPerSecond;
            set => FireLordConfig.IgnitionDropPerSecond = value;
        }

        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("燃烧持续时间", 0, 30, "点燃敌人后，该敌人持续燃烧的时间")]
        [XmlElement]
        public int IgnitionDurationInSecond
        {
            get => (int)FireLordConfig.IgnitionDurationInSecond;
            set => FireLordConfig.IgnitionDurationInSecond = value;
        }

        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("燃烧光照范围", 0, 20, "敌人被点燃后，身上的光源的光照半径")]
        [XmlElement]
        public int IgnitionLightRadius
        {
            get => (int)FireLordConfig.IgnitionLightRadius;
            set => FireLordConfig.IgnitionLightRadius = value;
        }

        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("燃烧光照强度", 0, 200, "敌人被点燃后，身上的光源的光照强度，决定亮度")]
        [XmlElement]
        public int IgnitionLightIntensity
        {
            get => (int)FireLordConfig.IgnitionLightIntensity;
            set => FireLordConfig.IgnitionLightIntensity = value;
        }

        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("启用燃烧伤害", "当敌人被点燃时，是否对其造成燃烧伤害")]
        [XmlElement]
        public bool IgnitionDealDamage
        {
            get => FireLordConfig.IgnitionDealDamage;
            set => FireLordConfig.IgnitionDealDamage = value;
        }

        [SettingPropertyGroup("点燃设置")]
        [SettingProperty("每秒燃烧伤害", 0, 200, "当敌人被点燃时，每秒受到的燃烧伤害")]
        [XmlElement]
        public int IgnitionDamagePerSecond
        {
            get => (int)FireLordConfig.IgnitionDamagePerSecond;
            set => FireLordConfig.IgnitionDamagePerSecond = value;
        }

        public void Init()
        {
            AllowedUnitType = (int)FireLordConfig.AllowedUnitType;
            UseFireArrowsAtDay = FireLordConfig.UseFireArrowsAtDay;
            UseFireArrowsAtNight = FireLordConfig.UseFireArrowsAtNight;
            UseFireArrowsAtSiege = FireLordConfig.UseFireArrowsAtSiege;
            AllowFireThrownWeapon = FireLordConfig.AllowFireThrownWeapon;
            ChancesOfFireArrow = (int)FireLordConfig.ChancesOfFireArrow;
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
        }
    }
}
