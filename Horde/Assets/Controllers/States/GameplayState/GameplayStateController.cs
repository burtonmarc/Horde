using System.Collections.Generic;
using System.Threading.Tasks;
using Catalogs.Scripts;
using Controllers.States.GameplayState.PlayerWeapons;
using Controllers.States.MainMenuState;
using Data;
using ScreenMachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class GameplayStateController : BaseStateController<GameplayStateUiView, GameplayStateWorldView>, IStateBase, IPreloadable
    {
        protected override string StateId { get; }

        private readonly PoolController poolController;

        private readonly EnemyWaveController enemyWaveController;

        private PlayerController playerController;

        private List<GameplayControllerBase> generalBehaviourControllers;

        private EntitiesCollisionController entitiesCollisionController;

        public GameplayStateController(Context context) : base(context)
        {
            StateId = "Gameplay";
            
            generalBehaviourControllers = new List<GameplayControllerBase>(8);
            
            entitiesCollisionController = new EntitiesCollisionController(context);
            
            enemyWaveController = new EnemyWaveController(context);
            
            poolController = new PoolController();

            context.PoolController = poolController;

            ControllerFactory.PoolController = poolController;
        }

        public void OnCreate()
        {
            UiView.ResetUiView();
            WorldView.Init();
            
            generalBehaviourControllers.Add(poolController);

            UiView.MainMenuClicked += PresentMainMenuState;

            InitPlayer();

            InjectPlayerController();
            
            playerController.AddPlayerWeapon();
        }
        
        private void InitPlayer()
        {
            var playerView = Preloader.GetAsset<PlayerView>(Context.CatalogsHolder.PlayerCatalog.GameplayView);
            var playerModel = Context.SaveSystem.LoadModel<PlayerModel>();
            playerController = ControllerFactory.CreateController<PlayerController>(playerView, playerModel);
            var enemiesLayer = Context.GetGameplayLayer(GameplayLayer.Enemies);
            playerController.PlayerView.Activate(enemiesLayer, Vector3.zero);
        }

        private void InjectPlayerController()
        {    
            ControllerFactory.PlayerController = playerController;
            
            entitiesCollisionController.PlayerController = playerController;
            enemyWaveController.PlayerController = playerController;
            poolController.PlayerController = playerController;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            foreach (var generalBehaviourController in generalBehaviourControllers)
            {
                generalBehaviourController.OnUpdate();
            }

            playerController.OnUpdate();

            entitiesCollisionController.OnUpdate();

            Cheats();
        }
        
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            
            foreach (var generalBehaviourController in generalBehaviourControllers)
            {
                generalBehaviourController.OnFixedUpdate();
            }
            
            entitiesCollisionController.OnFixedUpdate();
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();
            
            foreach (var generalBehaviourController in generalBehaviourControllers)
            {
                generalBehaviourController.OnLateUpdate();
            }
            
            entitiesCollisionController.OnLateUpdate();
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
            
            foreach (var generalBehaviourController in generalBehaviourControllers)
            {
                generalBehaviourController.OnDestroy();
            }
            
            entitiesCollisionController.OnDestroy();
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
                entitiesCollisionController.RemoveRandomEnemy();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                entitiesCollisionController.RemoveAllEnemies();
            }
        }

        public Transform GetGameplayLayer(GameplayLayer gameplayLayer)
        {
            return WorldView.GetLayer(gameplayLayer);
        }

        public Task Preload()
        {
            Preloader = Context.AssetLoaderFactory.CreateLoader(StateId);
            Context.Preloader = Preloader;

            Preloader.AddReferences(new List<AssetReference>
            {
                // Player
                Context.CatalogsHolder.PlayerCatalog.GameplayView,
                
                // Weapons
                Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry("Shuriken").WeaponGameplayView,
                Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry("Shuriken").ProjectileGameplayView,
                Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry("Shuriken").WeaponConfig,
                
                // Enemies
                Context.CatalogsHolder.EnemiesCatalog.GetCatalogEntry("EnemyTest").EnemyGameplayView,
                Context.CatalogsHolder.EnemiesCatalog.GetCatalogEntry("EnemyTest").EnemyConfig,
            });

            var task = Preloader.LoadAsync();

            return task;
        }
    }
}