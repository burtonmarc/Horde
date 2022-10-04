using Data;
using DefaultNamespace;
using Game.States.MainMenu;
using ScreenMachine;

namespace Controllers.States.MainMenuState
{
    public class MainMenuStateController : BaseStateController<MainMenuUiView, MainMenuWorldView>, IStateBase
    {
        protected override string StateId { get; }
        
        public MainMenuStateController(Context context) : base(context)
        {
            StateId = "MainMenu";
        }
        
        public void OnCreate()
        {
            UiView.Init();
            WorldView.Init();
            
            UiView.StartGameClicked += PresentGameplayState;
        }

        public void OnBringToFront()
        {
            
        }

        public void OnSendToBack()
        {
            
        }

        public void OnDestroy()
        {
            
        }

        private void PresentGameplayState()
        {
            PresentState(new GameplayStateController(Context));
        }
    }
}