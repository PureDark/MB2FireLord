using MBOptionScreen.Attributes;
using MBOptionScreen.Settings;
using TaleWorlds.InputSystem;

namespace FireLord.Settings
{
    public class FireLordSettings :  AttributeSettings<FireLordSettings>
    {
        public const string InstanceID = "FireLordSettings";
        public override string Id { get; set; } = InstanceID;
        public override string ModName => "{=FireLord_mod_name}火焰领主";
        public override string ModuleFolderName => FireLordSubModule.ModuleName;

        //private DefaultDropdown<FireLordConfig.UnitType> _allowedUnitType = new DefaultDropdown<FireLordConfig.UnitType>(new FireLordConfig.UnitType[]
        //{
        //    FireLordConfig.UnitType.None,
        //    FireLordConfig.UnitType.Player,
        //    FireLordConfig.UnitType.Heroes,
        //    FireLordConfig.UnitType.Companions,
        //    FireLordConfig.UnitType.Allies,
        //    FireLordConfig.UnitType.Enemies,
        //    FireLordConfig.UnitType.All
        //}, (int)FireLordConfig.AllowedUnitType);
        //[SettingPropertyGroup("{=FireLord_menu_fire_arrow_misc}火箭综合设置")]
        //[SettingPropertyDropdown("{=FireLord_menu_allowed_unit_type}允许使用火箭的单位")]
        //public DefaultDropdown<FireLordConfig.UnitType> AllowedUnitType
        //{
        //    get => _allowedUnitType;
        //    set {
        //        _allowedUnitType = value;
        //        FireLordConfig.AllowedUnitType = _allowedUnitType.SelectedValue;
        //    }
        //}

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_misc}火箭综合设置")]
        [SettingProperty("{=FireLord_menu_allowed_unit_type}允许使用火箭的单位", 0, 6, requireRestart: false,
            hintText: "{=FireLord_menu_allowed_unit_type_hint}允许使用火箭的单位类型，0=不允许，1=玩家，2=英雄单位，3=NPC队友，4=己方，5=敌方，6=所有人")]
        public int AllowedUnitType
        {
            get => (int)FireLordConfig.AllowedUnitType;
            set => FireLordConfig.AllowedUnitType = (FireLordConfig.UnitType)value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_misc}火箭综合设置")]
        [SettingProperty("{=FireLord_menu_use_fire_arrow_at_day}白天使用火箭", requireRestart: false,
            hintText: "{=FireLord_menu_use_fire_arrow_at_day_hint}在白天的战斗中使用火焰箭")]
        public bool UseFireArrowsAtDay
        {
            get => FireLordConfig.UseFireArrowsAtDay;
            set => FireLordConfig.UseFireArrowsAtDay = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_misc}火箭综合设置")]
        [SettingProperty("{=FireLord_menu_use_fire_arrow_at_night}夜晚使用火箭", requireRestart: false,
            hintText: "{=FireLord_menu_use_fire_arrow_at_night_hint}在夜晚的战斗中使用火焰箭")]
        public bool UseFireArrowsAtNight
        {
            get => FireLordConfig.UseFireArrowsAtNight;
            set => FireLordConfig.UseFireArrowsAtNight = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_misc}火箭综合设置")]
        [SettingProperty("{=FireLord_menu_use_fire_arrow_at_siege}围城使用火箭", requireRestart: false,
            hintText: "{=FireLord_menu_use_fire_arrow_at_siege_hint}在围城的战斗中使用火焰箭")]
        public bool UseFireArrowsAtSiege
        {
            get => FireLordConfig.UseFireArrowsAtSiege;
            set => FireLordConfig.UseFireArrowsAtSiege = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_misc}火箭综合设置")]
        [SettingProperty("{=FireLord_menu_allow_fire_thrown_weapon}投掷武器点燃", requireRestart: false,
            hintText: "{=FireLord_menu_allow_fire_thrown_weapon_hint}是否让投掷武器也拥有燃烧效果")]
        public bool AllowFireThrownWeapon
        {
            get => FireLordConfig.AllowFireThrownWeapon;
            set => FireLordConfig.AllowFireThrownWeapon = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_misc}火箭综合设置")]
        [SettingProperty("{=FireLord_menu_chances_of_fire_arrow}火箭发生概率", 0, 100, requireRestart: false,
            hintText: "{=FireLord_menu_chances_of_fire_arrow_hint}弓箭手射箭时产生火箭的百分比概率，不影响玩家")]
        public int ChancesOfFireArrow
        {
            get => (int)FireLordConfig.ChancesOfFireArrow;
            set => FireLordConfig.ChancesOfFireArrow = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_misc}火箭综合设置")]
        [SettingProperty("{=FireLord_menu_sticked_arrow_burning_time}火焰残留时间", 0, 20, requireRestart: false,
            hintText: "{=FireLord_menu_sticked_arrow_burning_time_hint}火箭射中后继续燃烧的时间，这段时间内火焰会逐渐熄灭")]
        public int StickedArrowsBurningTime
        {
            get => (int)FireLordConfig.StickedArrowsBurningTime;
            set => FireLordConfig.StickedArrowsBurningTime = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_lights}火箭光照设置")]
        [SettingProperty("{=FireLord_menu_fire_arrow_light_radius}火箭光照范围", 0, 20, requireRestart: false,
            hintText: "{=FireLord_menu_fire_arrow_light_radius_hint}火箭的光源照射的半径")]
        public int FireArrowLightRadius
        {
            get => (int)FireLordConfig.FireArrowLightRadius;
            set => FireLordConfig.FireArrowLightRadius = value;
        }
        
        [SettingPropertyGroup("{=FireLord_menu_fire_arrow_lights}火箭光照设置")]
        [SettingProperty("{=FireLord_menu_fire_arrow_light_intensity}火箭光照强度", 0, 200, requireRestart: false,
            hintText: "{=FireLord_menu_fire_arrow_light_intensity_hint}火箭的光源强度，决定亮度")]
        public int FireArrowLightIntensity
        {
            get => (int)FireLordConfig.FireArrowLightIntensity;
            set => FireLordConfig.FireArrowLightIntensity = value;
        }
        
        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}火焰剑设置")]
        [SettingProperty("{=FireLord_menu_fire_sword_toggle_key}火焰剑切换键", 0, 255, requireRestart: false,
            hintText: "{=FireLord_menu_fire_sword_toggle_key_hint}开启/关闭 火焰剑的热键，菜单暂时不支持下拉框，最好不要修改，可以在FireLordConfig.ini中设置（默认：C）")]
        public int FireSwordToggleKey
        {
            get => (int)FireLordConfig.FireSwordToggleKey;
            set => FireLordConfig.FireSwordToggleKey = (InputKey)value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}火焰剑设置")]
        [SettingProperty("{=FireLord_menu_ignite_player_body}点燃玩家身体（无伤害）", requireRestart: false,
            hintText: "{=FireLord_menu_ignite_player_body_hint}是否在开启火焰剑的同时点燃玩家的身体，只有视觉效果，不造成伤害")]
        public bool IgnitePlayerBody
        {
            get => FireLordConfig.IgnitePlayerBody;
            set => FireLordConfig.IgnitePlayerBody = value;
        }
        
        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}火焰剑设置")]
        [SettingProperty("{=FireLord_menu_fire_sword_light_radius}火焰剑光照范围", 0, 20, requireRestart: false,
            hintText: "{=FireLord_menu_fire_sword_light_radius_hint}火焰剑的光源照射的半径")]
        public int FireSwordLightRadius
        {
            get => (int)FireLordConfig.FireSwordLightRadius;
            set => FireLordConfig.FireSwordLightRadius = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_fire_sword_settings}火焰剑设置")]
        [SettingProperty("{=FireLord_menu_fire_sword_light_intensity}火焰剑光照强度", 0, 200, requireRestart: false,
            hintText: "{=FireLord_menu_fire_sword_light_intensity_hint}火焰剑的光源强度，决定亮度")]
        public int FireSwordLightIntensity
        {
            get => (int)FireLordConfig.FireSwordLightIntensity;
            set => FireLordConfig.FireSwordLightIntensity = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignite_target_with_fire_arrow}允许火箭点燃敌人", requireRestart: false,
            hintText: "{=FireLord_menu_ignite_target_with_fire_arrow_hint}是否允许使用火箭点燃敌人")]
        public bool IgniteTargetWithFireArrow
        {
            get => FireLordConfig.IgniteTargetWithFireArrow;
            set => FireLordConfig.IgniteTargetWithFireArrow = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignite_target_with_fire_sword}允许火焰剑点燃敌人", requireRestart: false,
        hintText: "{=FireLord_menu_ignite_target_with_fire_sword_hint}是否允许使用火焰剑点燃敌人")]
        public bool IgniteTargetWithFireSword
        {
            get => FireLordConfig.IgniteTargetWithFireSword;
            set => FireLordConfig.IgniteTargetWithFireSword = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignition_bar_max}点燃槽上限", 0, 200, requireRestart: false,
            hintText: "{=FireLord_menu_ignition_bar_max_hint}当点燃槽满了的时候，点燃这名敌人")]
        public int IgnitionBarMax
        {
            get => (int)FireLordConfig.IgnitionBarMax;
            set => FireLordConfig.IgnitionBarMax = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignition_per_fire_arrow}火箭每箭点燃值", 0, 200, requireRestart: false,
            hintText: "{=FireLord_menu_ignition_per_fire_arrow_hint}每只火箭击中敌人时，增加的点燃槽数值")]
        public int IgnitionPerFireArrow
        {
            get => (int)FireLordConfig.IgnitionPerFireArrow;
            set => FireLordConfig.IgnitionPerFireArrow = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignition_per_fire_sword_hit}火焰剑每击点燃值", 0, 200, requireRestart: false,
            hintText: "{=FireLord_menu_ignition_per_fire_sword_hit_hint}火焰剑每次击中敌人时，增加的点燃槽数值（被格挡时减半）")]
        public int IgnitionPerFireSwordHit
        {
            get => (int)FireLordConfig.IgnitionPerFireSwordHit;
            set => FireLordConfig.IgnitionPerFireSwordHit = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignition_drop_per_second}每秒点燃值降低", 0, 200, requireRestart: false,
            hintText: "{=FireLord_menu_ignition_drop_per_second_hint}点燃槽随时间自动减少，每秒降低的数值")]
        public int IgnitionDropPerSecond
        {
            get => (int)FireLordConfig.IgnitionDropPerSecond;
            set => FireLordConfig.IgnitionDropPerSecond = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignition_duration}燃烧持续时间", 0, 30, requireRestart: false,
            hintText: "{=FireLord_menu_ignition_duration_hint}被点燃的人的持续燃烧的时间")]
        public int IgnitionDurationInSecond
        {
            get => (int)FireLordConfig.IgnitionDurationInSecond;
            set => FireLordConfig.IgnitionDurationInSecond = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignition_light_radius}燃烧光照范围", 0, 20, requireRestart: false,
            hintText: "{=FireLord_menu_ignition_light_radius_hint}被点燃的人身上的光源的光照半径")]
        public int IgnitionLightRadius
        {
            get => (int)FireLordConfig.IgnitionLightRadius;
            set => FireLordConfig.IgnitionLightRadius = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignition_light_intensity}燃烧光照强度", 0, 200, requireRestart: false,
            hintText: "{=FireLord_menu_ignition_light_intensity_hint}被点燃的人身上的光源的光照强度，决定亮度")]
        public int IgnitionLightIntensity
        {
            get => (int)FireLordConfig.IgnitionLightIntensity;
            set => FireLordConfig.IgnitionLightIntensity = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignition_deal_damage}启用燃烧伤害", requireRestart: false,
            hintText: "{=FireLord_menu_ignition_deal_damage_hint}是否对点燃的人造成燃烧伤害")]
        public bool IgnitionDealDamage
        {
            get => FireLordConfig.IgnitionDealDamage;
            set => FireLordConfig.IgnitionDealDamage = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignition_Friendly_Fire}启用友军伤害", requireRestart: false,
            hintText: "{=FireLord_menu_ignition_Friendly_Fire_hint}是否允许被队友点燃")]
        public bool IgnitionFriendlyFire
        {
            get => FireLordConfig.IgnitionFriendlyFire;
            set => FireLordConfig.IgnitionFriendlyFire = value;
        }

        [SettingPropertyGroup("{=FireLord_menu_ignition_settings}点燃设置")]
        [SettingProperty("{=FireLord_menu_ignition_damage_per_second}每秒燃烧伤害", 0, 200, requireRestart: false,
            hintText: "{=FireLord_menu_ignition_damage_per_second_hint}当敌人被点燃时，每秒受到的燃烧伤害")]
        public int IgnitionDamagePerSecond
        {
            get => (int)FireLordConfig.IgnitionDamagePerSecond;
            set => FireLordConfig.IgnitionDamagePerSecond = value;
        }

        public FireLordSettings():base()
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
            IgnitionDealDamage = FireLordConfig.IgnitionDealDamage;
            IgnitionFriendlyFire = FireLordConfig.IgnitionFriendlyFire;
            IgnitionDamagePerSecond = FireLordConfig.IgnitionDamagePerSecond;
        }
    }
}
