using ScreenMachine;

namespace Game.States.StartupState
{
    public class StartupStateController : BaseStateController<StartupStateUiView, StartupStateWorldView>, IStateBase
    {
        protected sealed override string StateId { get; }
        
        public StartupStateController()
        {
            StateId = "StartupState";
        }

        public void OnCreate()
        {
            
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