using System.Collections.Generic;
using Controllers.States.MainMenuState;
using Data;
using ScreenMachine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class GameplayStateController : BaseStateController<GameplayUiView, GameplayWorldView>, IStateBase
    {
        protected override string StateId { get; }

        private List<GameplayControllerBase> GameplayControllers;

        private PoolController PoolController;

        public GameplayStateController(Context context) : base(context)
        {
            StateId = "Gameplay";
            GameplayControllers = new List<GameplayControllerBase>(64);
            PoolController = new PoolController(context);
        }

        public void OnCreate()
        {
            UiView.Init();
            WorldView.Init();

            ControllerFactory.OnControllerCreated += OnControllerCreated;

            UiView.MainMenuClicked += PresentMainMenuState;
            
            var playerView = WorldView.InstantiatePlayer();
            
            var playerController = new PlayerController(Context);
            playerController.Init(playerView);
            GameplayControllers.Add(playerController);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            foreach (var gameplayController in GameplayControllers)
            {
                gameplayController.OnUpdate();
            }
        }

        public void OnSendToBack()
        {
            
        }

        public void OnBringToFront()
        {
            
        }

        public void OnDestroy()
        {
            ControllerFactory.OnControllerCreated -= OnControllerCreated;
            
            foreach (var gameplayController in GameplayControllers)
            {
                gameplayController.OnDestroy();
            }
        }

        private void OnControllerCreated(GameplayControllerBase controller)
        {
            GameplayControllers.Add(controller);
        }

        private void PresentMainMenuState()
        {
            PresentState(new MainMenuStateController(Context));
        }
    }
}