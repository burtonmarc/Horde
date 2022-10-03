using Data;
using DefaultNamespace;
using Game.States.MainMenu;
using ScreenMachine;

namespace Controllers.States.MainMenuState
{
    public class MainMenuStateController : BaseStateController<MainMenuUiView, MainMenuWorldView>, IStateBase
    {
        protected sealed override string StateId { get; }
        
        public MainMenuStateController(Context context) : base(context)
        {
            StateId = "StartupState";
        }
        
        public void OnCreate()
        {
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