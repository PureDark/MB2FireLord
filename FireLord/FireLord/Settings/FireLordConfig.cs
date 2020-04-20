using FireLord.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace FireLord.FireLord.Settings
{
    public static class FireLordConfig
    {
        public static String ConfigPath = BasePath.Name + "Modules/" + FireLordSubModule.ModuleName + "/FireLordConfig.ini";
        public static IniHelper iniHelper = new IniHelper(ConfigPath, FireLordSubModule.ModName);

        private static bool _useFireArrowsAtDay;
        public static bool UseFireArrowsAtDay
        {
            get {
                return _useFireArrowsAtDay;
            }
            set
            {
                _useFireArrowsAtDay = value;
                iniHelper.SetBool("UseFireArrowsAtDay", value);
            }
        }

        private static bool _useFireArrowsAtNight;
        public static bool UseFireArrowsAtNight
        {
            get
            {
                return _useFireArrowsAtNight;
            }
            set
            {
                _useFireArrowsAtNight = value;
                iniHelper.SetBool("UseFireArrowsAtNight", value);
            }
        }

        private static bool _useFireArrowsAtSiege;
        public static bool UseFireArrowsAtSiege
        {
            get
            {
                return _useFireArrowsAtSiege;
            }
            set
            {
                _useFireArrowsAtSiege = value;
                iniHelper.SetBool("UseFireArrowsAtSiege", value);
            }
        }

        private static bool _allowFireThrownWeapon;
        public static bool AllowFireThrownWeapon
        {
            get
            {
                return _allowFireThrownWeapon;
            }
            set
            {
                _allowFireThrownWeapon = value;
                iniHelper.SetBool("AllowFireThrownWeapon", value);
            }
        }

        private static float _stickedArrowsBurningTime;
        public static float StickedArrowsBurningTime
        {
            get
            {
                return _stickedArrowsBurningTime;
            }
            set
            {
                _stickedArrowsBurningTime = value;
                iniHelper.SetInt("StickedArrowsBurningTime", (int)value);
            }
        }

        private static Vec3 _fireArrowLightColor;
        public static Vec3 FireArrowLightColor
        {
            get
            {
                return _fireArrowLightColor;
            }
            set
            {
                _fireArrowLightColor = value;
                iniHelper.SetInt("FireArrowLightColorR", (int)Math.Round(value.x * 255));
                iniHelper.SetInt("FireArrowLightColorG", (int)Math.Round(value.y * 255));
                iniHelper.SetInt("FireArrowLightColorB", (int)Math.Round(value.z * 255));
            }
        }

        private static float _fireArrowLightRadius;
        public static float FireArrowLightRadius
        {
            get
            {
                return _fireArrowLightRadius;
            }
            set
            {
                _fireArrowLightRadius = value;
                iniHelper.SetFloat("FireArrowLightRadius", value);
            }
        }

        private static float _fireArrowLightIntensity;
        public static float FireArrowLightIntensity
        {
            get
            {
                return _fireArrowLightIntensity;
            }
            set
            {
                _fireArrowLightIntensity = value;
                iniHelper.SetFloat("FireArrowLightIntensity", value);
            }
        }

        private static InputKey _fireSwordToggleKey;
        public static InputKey FireSwordToggleKey
        {
            get
            {
                return _fireSwordToggleKey;
            }
            set
            {
                _fireSwordToggleKey = value;
                iniHelper.Set("FireSwordToggleKey", value.ToString());
            }
        }

        private static Vec3 _fireSwordLightColor;
        public static Vec3 FireSwordLightColor
        {
            get
            {
                return _fireSwordLightColor;
            }
            set
            {
                _fireSwordLightColor = value;
                iniHelper.SetInt("FireSwordLightColorR", (int)Math.Round(value.x * 255));
                iniHelper.SetInt("FireSwordLightColorG", (int)Math.Round(value.y * 255));
                iniHelper.SetInt("FireSwordLightColorB", (int)Math.Round(value.z * 255));
            }
        }

        private static float _fireSwordLightRadius;
        public static float FireSwordLightRadius
        {
            get
            {
                return _fireSwordLightRadius;
            }
            set
            {
                _fireSwordLightRadius = value;
                iniHelper.SetFloat("FireSwordLightRadius", value);
            }
        }

        private static float _fireSwordLightIntensity;
        public static float FireSwordLightIntensity
        {
            get
            {
                return _fireSwordLightIntensity;
            }
            set
            {
                _fireSwordLightIntensity = value;
                iniHelper.SetFloat("FireSwordLightIntensity", value);
            }
        }

        private static bool _igniteTargetWithFireArrow;
        public static bool IgniteTargetWithFireArrow
        {
            get
            {
                return _igniteTargetWithFireArrow;
            }
            set
            {
                _igniteTargetWithFireArrow = value;
                iniHelper.SetBool("IgniteTargetWithFireArrow", value);
            }
        }

        private static bool _igniteTargetWithFireSword;
        public static bool IgniteTargetWithFireSword
        {
            get
            {
                return _igniteTargetWithFireSword;
            }
            set
            {
                _igniteTargetWithFireSword = value;
                iniHelper.SetBool("IgniteTargetWithFireSword", value);
            }
        }

        private static float _ignitionBarMax;
        public static float IgnitionBarMax
        {
            get
            {
                return _ignitionBarMax;
            }
            set
            {
                _ignitionBarMax = value;
                iniHelper.SetFloat("IgnitionBarMax", value);
            }
        }

        private static float _ignitionPerFireArrow;
        public static float IgnitionPerFireArrow
        {
            get
            {
                return _ignitionPerFireArrow;
            }
            set
            {
                _ignitionPerFireArrow = value;
                iniHelper.SetFloat("IgnitionPerFireArrow", value);
            }
        }

        private static float _ignitionPerFireSwordHit;
        public static float IgnitionPerFireSwordHit
        {
            get
            {
                return _ignitionPerFireSwordHit;
            }
            set
            {
                _ignitionPerFireSwordHit = value;
                iniHelper.SetFloat("IgnitionPerFireSwordHit", value);
            }
        }

        private static float _ignitionDropPerSecond;
        public static float IgnitionDropPerSecond
        {
            get
            {
                return _ignitionDropPerSecond;
            }
            set
            {
                _ignitionDropPerSecond = value;
                iniHelper.SetFloat("IgnitionDropPerSecond", value);
            }
        }

        private static float _ignitionDurationInSecond;
        public static float IgnitionDurationInSecond
        {
            get
            {
                return _ignitionDurationInSecond;
            }
            set
            {
                _ignitionDurationInSecond = value;
                iniHelper.SetFloat("IgnitionDurationInSecond", value);
            }
        }

        private static Vec3 _ignitionLightColor;
        public static Vec3 IgnitionLightColor
        {
            get
            {
                return _ignitionLightColor;
            }
            set
            {
                _ignitionLightColor = value;
                iniHelper.SetInt("IgnitionLightColorR", (int)Math.Round(value.x * 255));
                iniHelper.SetInt("IgnitionLightColorG", (int)Math.Round(value.y * 255));
                iniHelper.SetInt("IgnitionLightColorB", (int)Math.Round(value.z * 255));
            }
        }

        private static float _ignitionLightRadius;
        public static float IgnitionLightRadius
        {
            get
            {
                return _ignitionLightRadius;
            }
            set
            {
                _ignitionLightRadius = value;
                iniHelper.SetFloat("IgnitionLightRadius", value);
            }
        }

        private static float _ignitionLightIntensity;
        public static float IgnitionLightIntensity
        {
            get
            {
                return _ignitionLightIntensity;
            }
            set
            {
                _ignitionLightIntensity = value;
                iniHelper.SetFloat("IgnitionLightIntensity", value);
            }
        }

        private static bool _ignitionDealDamage;
        public static bool IgnitionDealDamage
        {
            get
            {
                return _ignitionDealDamage;
            }
            set
            {
                _ignitionDealDamage = value;
                iniHelper.SetBool("IgnitionDealDamage", value);
            }
        }

        private static int _ignitionDamagePerSecond;
        public static int IgnitionDamagePerSecond
        {
            get
            {
                return _ignitionDamagePerSecond;
            }
            set
            {
                _ignitionDamagePerSecond = value;
                iniHelper.SetInt("IgnitionDamagePerSecond", value);
            }
        }

        public static void Init()
        {
            if(!File.Exists(ConfigPath))
            {
                UseFireArrowsAtDay = true;
                UseFireArrowsAtNight = true;
                UseFireArrowsAtSiege = true;
                AllowFireThrownWeapon = true;
                StickedArrowsBurningTime = 8f;
                FireArrowLightColor = new Vec3(0.847f, 0.541f, 0f);
                FireArrowLightRadius = 3f;
                FireArrowLightIntensity = 120f;
                FireSwordToggleKey = InputKey.C;
                FireSwordLightColor = new Vec3(0.847f, 0.541f, 0f);
                FireSwordLightRadius = 5f;
                FireSwordLightIntensity = 85f;
                IgniteTargetWithFireArrow = true;
                IgniteTargetWithFireSword = true;
                IgnitionBarMax = 100f;
                IgnitionPerFireArrow = 75f;
                IgnitionPerFireSwordHit = 100f;
                IgnitionDropPerSecond = 10f;
                IgnitionDurationInSecond = 5f;
                IgnitionLightColor = new Vec3(0.847f, 0.541f, 0f);
                IgnitionLightRadius = 7f;
                IgnitionLightIntensity = 125f;
                IgnitionDealDamage = true;
                IgnitionDamagePerSecond = 10;
            }
            else
            {
                _useFireArrowsAtDay = iniHelper.GetBool("UseFireArrowsAtDay", true); ;
                _useFireArrowsAtNight = iniHelper.GetBool("UseFireArrowsAtNight", true);
                _useFireArrowsAtSiege = iniHelper.GetBool("UseFireArrowsAtSiege", true);
                _allowFireThrownWeapon = iniHelper.GetBool("AllowFireThrownWeapon", true);
                _stickedArrowsBurningTime = iniHelper.GetFloat("StickedArrowsBurningTime", 8);
                float R = iniHelper.GetInt("FireArrowLightColorR", 216) / 255f;
                float G = iniHelper.GetInt("FireArrowLightColorG", 138) / 255f;
                float B = iniHelper.GetInt("FireArrowLightColorB", 0) / 255f;
                _fireArrowLightColor = new Vec3(R, G, B);
                _fireArrowLightRadius = iniHelper.GetFloat("FireArrowLightRadius", 3f);
                _fireArrowLightIntensity = iniHelper.GetFloat("FireArrowLightIntensity", 120f);
                _fireSwordToggleKey = (InputKey)Enum.Parse(typeof(InputKey), iniHelper.Get("FireSwordToggleKey", "C"));
                R = iniHelper.GetInt("FireSwordLightColorR", 216) / 255f;
                G = iniHelper.GetInt("FireSwordLightColorG", 138) / 255f;
                B = iniHelper.GetInt("FireSwordLightColorB", 0) / 255f;
                _fireSwordLightColor = new Vec3(R, G, B);
                _fireSwordLightRadius = iniHelper.GetFloat("FireSwordLightRadius", 5f);
                _fireSwordLightIntensity = iniHelper.GetFloat("FireSwordLightIntensity", 85f);
                _igniteTargetWithFireArrow = iniHelper.GetBool("IgniteTargetWithFireArrow", true);
                _igniteTargetWithFireSword = iniHelper.GetBool("IgniteTargetWithFireSword", true);
                _ignitionBarMax = iniHelper.GetFloat("IgnitionBarMax", 100f);
                _ignitionPerFireArrow = iniHelper.GetFloat("IgnitionPerFireArrow", 75f);
                _ignitionPerFireSwordHit = iniHelper.GetFloat("IgnitionPerFireSwordHit", 100f);
                _ignitionDropPerSecond = iniHelper.GetFloat("IgnitionDropPerSecond", 10f);
                _ignitionDurationInSecond = iniHelper.GetFloat("IgnitionDurationInSecond", 5f);
                R = iniHelper.GetInt("IgnitionLightColorR", 216) / 255f;
                G = iniHelper.GetInt("IgnitionLightColorG", 138) / 255f;
                B = iniHelper.GetInt("IgnitionLightColorB", 0) / 255f;
                _ignitionLightColor = new Vec3(R, G, B);
                _ignitionLightRadius = iniHelper.GetFloat("IgnitionLightRadius", 7f);
                _ignitionLightIntensity = iniHelper.GetFloat("IgnitionLightIntensity", 125f);
                _ignitionDealDamage = iniHelper.GetBool("IgnitionDealDamage", true);
                _ignitionDamagePerSecond = iniHelper.GetInt("IgnitionDamagePerSecond", 10);
            }
        }
    }
}
