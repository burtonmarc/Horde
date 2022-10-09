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

        private readonly List<GameplayControllerBase> gameplayControllers;

        private readonly PoolController poolController;

        private readonly EnemyWaveController enemyWaveController;

        private PlayerController playerController;

        public GameplayStateController(Context context) : base(context)
        {
            StateId = "Gameplay";
            gameplayControllers = new List<GameplayControllerBase>(64);
            
            enemyWaveController = new EnemyWaveController(context);
            gameplayControllers.Add(enemyWaveController);
            
            poolController = new PoolController(context);
            context.ControllersPool = poolController;
            ControllerFactory.PoolController = poolController;
            gameplayControllers.Add(poolController);
        }

        public void OnCreate()
        {
            UiView.Init();
            WorldView.Init();

            UiView.MainMenuClicked += PresentMainMenuState;
            
            InitPlayer();

            InjectPlayerController();
        }

        private void InjectPlayerController()
        {
            enemyWaveController.PlayerController = playerController;
            poolController.PlayerController = playerController;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            foreach (var gameplayController in gameplayControllers)
            {
                gameplayController.OnUpdate();
            }

            Cheats();
        }
        
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            foreach (var gameplayController in gameplayControllers)
            {
                gameplayController.OnFixedUpdate();
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
            foreach (var gameplayController in gameplayControllers)
            {
                gameplayController.OnDestroy();
            }
        }

        private void InitPlayer()
        {
            var playerView = WorldView.InstantiatePlayer();
            playerController = new PlayerController(Context);
            playerController.Init(playerView);
            gameplayControllers.Add(playerController);
        }

        private void PresentMainMenuState()
        {
            PresentState(new MainMenuStateController(Context));
        }

        private void Cheats()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                for (int i = 0; i < 50; i++)
                {
                    enemyWaveController.CreateEnemyAtRandomPosition();
                }
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                enemyWaveController.RemoveRandomEnemy();
            }
        }

        public Transform GetGameplayLayer(GameplayLayer gameplayLayer)
        {
            return WorldView.GetLayer(gameplayLayer);
        }
    }
}