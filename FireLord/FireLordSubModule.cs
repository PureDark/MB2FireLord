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

        protected override void OnSubModuleLoad()
        {
            FireLordConfig.Init();
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            mission.AddMissionBehaviour(new FireArrowLogic());
            mission.AddMissionBehaviour(new FireSwordLogic());
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            game.GameTextManager.LoadGameTexts(BasePath.Name + $"Modules/{ModuleName}/ModuleData/module_strings.xml");
        }
    }
}
