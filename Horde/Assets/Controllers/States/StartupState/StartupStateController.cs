using Controllers.States.MainMenuState;
using Data;
using Game.States.StartupState;
using ScreenMachine;

namespace Controllers.States.StartupState
{
    public class StartupStateController : BaseStateController<StartupStateUiView, StartupStateWorldView>, IStateBase
    {
        protected sealed override string StateId { get; }

        public StartupStateController(Context context) : base(context)
        {
            StateId = "StartupState";
        }

        public void OnCreate()
        {
            PushState(new MainMenuStateController(Context));
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