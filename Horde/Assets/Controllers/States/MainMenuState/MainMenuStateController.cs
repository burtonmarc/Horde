using ScreenMachine;

namespace Game.States.MainMenu
{
    public class MainMenuStateController : BaseStateController<MainMenuUiView, MainMenuWorldView>, IStateBase
    {
        protected sealed override string StateId { get; }
        
        public MainMenuStateController()
        {
            StateId = "StartupState";
        }
        
        public void OnCreate()
        {
            //UiView.StartGameButtonClicked +=  
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
    }
}