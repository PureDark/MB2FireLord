using FireLord.FireLord.Settings;
using ModLib;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace FireLord
{
    public class FireLordSubModule : MBSubModuleBase
    {
        public static string ModName => "Fire Lord";
        public static string ModuleName => "FireLord";
        public static string Version => "1.0.1";

        private SettingsBase _fireLordSettings;

        protected override void OnSubModuleLoad()
        {
            FireLordConfig.Init();
            FileDatabase.Initialise(ModuleName);
            BannerlordConfig.Initialize();
            if (BannerlordConfig.Language == "简体中文")
            {
                _fireLordSettings = FileDatabase.Get<FireLordSettings>(FireLordSettings.InstanceID);
                if (_fireLordSettings == null) _fireLordSettings = new FireLordSettings();
                ((FireLordSettings)_fireLordSettings).Init();
                SettingsDatabase.RegisterSettings(_fireLordSettings);
            }
            else
            {
                _fireLordSettings = FileDatabase.Get<FireLordSettingsEnglish>(FireLordSettings.InstanceID);
                if (_fireLordSettings == null) _fireLordSettings = new FireLordSettingsEnglish();
                ((FireLordSettingsEnglish)_fireLordSettings).Init();
                SettingsDatabase.RegisterSettings(_fireLordSettings);
            }
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            mission.AddMissionBehaviour((MissionBehaviour)new FireArrowLogic());
            mission.AddMissionBehaviour((MissionBehaviour)new FireSwordLogic());
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            game.GameTextManager.LoadGameTexts(BasePath.Name + $"Modules/{ModuleName}/ModuleData/module_strings.xml");
        }
    }
}
