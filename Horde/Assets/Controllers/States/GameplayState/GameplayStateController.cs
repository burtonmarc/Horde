using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers.States.GameplayState.GameplayExtensions;
using Controllers.States.GameplayState.PlayerWeapons;
using Controllers.States.MainMenuState;
using Data;
using ScreenMachine;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class GameplayStateController : BaseStateController<GameplayStateUiView, GameplayStateWorldView>, IStateBase, IPreloadable
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
            gameplayControllers.AddController(enemyWaveController);
            
            poolController = new PoolController(context);
            context.ControllersPool = poolController;
            ControllerFactory.PoolController = poolController;
            gameplayControllers.AddController(poolController);
        }

        public void OnCreate()
        {
            UiView.ResetUiView();
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

        public override void OnDestroy()
        {
            base.OnDestroy();
            
            for (var index = gameplayControllers.Count - 1; index >= 0; index--)
            {
                var gameplayController = gameplayControllers[index];
                gameplayController.OnDestroy();
            }
        }

        private void InitPlayer()
        {
            var playerView = Preloader.GetAsset<PlayerView>(Context.CatalogsHolder.PlayerCatalog.GameplayView);
            var playerModel = Context.SaveSystem.LoadModel<PlayerModel>();
            playerController = ControllerFactory.CreateController<PlayerController>(playerView, playerModel);
            gameplayControllers.AddController(playerController);
            var enemiesLayer = Context.GetGameplayLayer(GameplayLayer.Enemies);
            playerController.PlayerView.Activate(enemiesLayer, Vector3.zero);

            var shurikenEntry = Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry("Shuriken");
            var shurikenView = Preloader.GetAsset<ShurikenView>(shurikenEntry.WeaponGameplayView);
            var shurikenModel = new ShurikenModel(shurikenEntry.WeaponConfig);
            var shurikenController = ControllerFactory.CreateController<ShurikenController>(shurikenView, shurikenModel);
            gameplayControllers.AddController(shurikenController);
            shurikenController.ShurikenView.Activate(playerController.PlayerView.WeaponAnchor, Vector3.zero);
        }

        private void PresentMainMenuState()
        {
            PresentState(new MainMenuStateController(Context));
        }

        private void Cheats()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                for (int i = 0; i < 10; i++)
                {
                    enemyWaveController.CreateEnemyAtRandomPosition();
                }
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                enemyWaveController.RemoveRandomEnemy();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                enemyWaveController.RemoveAllEnemies();
            }
        }

        public Transform GetGameplayLayer(GameplayLayer gameplayLayer)
        {
            return WorldView.GetLayer(gameplayLayer);
        }

        public Task Preload()
        {
            return null;
        }
    }
}