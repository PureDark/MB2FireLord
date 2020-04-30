using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using FireLord.Settings;

namespace FireLord
{
    public class FireLordSubModule : MBSubModuleBase
    {
        public static string ModName => "Fire Lord";
        public static string ModuleName => "FireLord";
        public static string Version => "1.1.0";

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            FireLordSettings.Instance.Save();
            IgnitionLogic ignitionLogic = new IgnitionLogic();
            mission.AddMissionBehaviour(ignitionLogic);
            mission.AddMissionBehaviour(new FireArrowLogic(ignitionLogic));
            mission.AddMissionBehaviour(new FireSwordLogic(ignitionLogic));
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            game.GameTextManager.LoadGameTexts($"{BasePath.Name}/Modules/{ModuleName}/ModuleData/module_strings.xml");
        }
    }
}
