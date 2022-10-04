using System.Collections.Generic;
using Controllers.States.MainMenuState;
using Data;
using ScreenMachine;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class GameplayStateController : BaseStateController<GameplayUiView, GameplayWorldView>, IStateBase
    {
        protected override string StateId { get; }

        private List<GameplayControllerBase> GameplayControllers;

        public GameplayStateController(Context context) : base(context)
        {
            StateId = "Gameplay";
            GameplayControllers = new List<GameplayControllerBase>(64);
        }

        public void OnCreate()
        {
            UiView.Init();
            WorldView.Init();

            UiView.MainMenuClicked += PresentMainMenuState;
            
            var playerView = WorldView.InstantiatePlayer();
            var playerController = new PlayerController(Context);
            playerController.Init(playerView);
            GameplayControllers.Add(playerController);
            
            var enemyView = WorldView.InstantiateEnemy();
            var enemyController = new EnemyController(Context, playerController);
            enemyController.Init(enemyView);
            GameplayControllers.Add(enemyController);
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
            
        }

        private void PresentMainMenuState()
        {
            PresentState(new MainMenuStateController(Context));
        }
    }
}