using Controllers.States;
using Controllers.States.MainMenuState;
using Data;
using ScreenMachine;
using Views.States.GameplayState;

namespace DefaultNamespace
{
    public class GameplayStateController : BaseStateController<GameplayUiView, GameplayWorldView>, IStateBase
    {
        protected override string StateId { get; }
        
        public GameplayStateController(Context context) : base(context)
        {
            StateId = "Gameplay";
        }

        public void OnCreate()
        {
            UiView.Init();
            WorldView.Init();

            UiView.MainMenuClicked += PresentMainMenuState;
        }

        public void OnSendToBack()
        {
            
        }

        public void OnBringToFront()
        {
            
        }

        public void OnDestroy()
        {
            
        }

        private void PresentMainMenuState()
        {
            PresentState(new MainMenuStateController(Context));
        }
    }
}