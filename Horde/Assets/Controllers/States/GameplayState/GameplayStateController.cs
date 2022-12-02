using System.Collections.Generic;
using System.Threading.Tasks;
using Catalogs.Scripts.Configs;
using Controllers.States.MainMenuState;
using Data;
using Data.Models;
using ScreenMachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class GameplayStateController : BaseStateController<GameplayStateUiView, GameplayStateWorldView>, IStateBase, IPreloadable
    {
        protected override string StateId { get; }

        private readonly List<GameplayControllerBase> generalBehaviourControllers;
        
        private readonly PoolController poolController;

        private readonly WavesController wavesController;

        private JoystickController joystickController;

        private PlayerController playerController;

        private readonly EntitiesContainerController entitiesContainerController;

        private GameplayCameraController gameplayCameraController;
        
        private readonly UserModel userModel;

        public GameplayStateController(Context context) : base(context)
        {
            StateId = "Gameplay";
            
            generalBehaviourControllers = new List<GameplayControllerBase>(8);
            
            entitiesContainerController = new EntitiesContainerController(context);
            
            wavesController = new WavesController(context);
            
            poolController = new PoolController();

            context.PoolController = poolController;
            
            userModel = context.UserModel;

            ControllerViewFactory.PoolController = poolController;
        }

        public void OnCreate()
        {
            UiView.Init();
            WorldView.Init();

            UiView.MainMenuClicked += PresentMainMenuState;

            InitPlayer();
            
            InjectPlayerController();

            CreateJoystickController();

            CreateCameraController();
            
            playerController.AddPlayerWeapon();

            var levelModel = Context.ModelFactory.GetLevelModel();
            
            wavesController.Init(levelModel);
            
            generalBehaviourControllers.Add(wavesController);
            generalBehaviourControllers.Add(poolController);
            generalBehaviourControllers.Add(joystickController);
        }

        private void InitPlayer()
        {
            var playerView = Preloader.GetAsset<PlayerView>(Context.CatalogsHolder.PlayerCatalog.GameplayView);
            var playerModel = Context.ModelFactory.GetPlayerModel();
            playerController = ControllerViewFactory.CreateControllerView<PlayerController>(playerView, playerModel);
            var enemiesLayer = GameplayUtils.GetGameplayLayer(Context, GameplayLayer.Enemies);
            playerController.PlayerView.Activate(enemiesLayer, Vector3.zero);
        }

        private void InjectPlayerController()
        {    
            ControllerViewFactory.PlayerController = playerController;
            
            entitiesContainerController.PlayerController = playerController;
            wavesController.PlayerController = playerController;
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

            entitiesContainerController.OnUpdate();

            Cheats();
        }
        
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            
            foreach (var generalBehaviourController in generalBehaviourControllers)
            {
                generalBehaviourController.OnFixedUpdate();
            }
            
            entitiesContainerController.OnFixedUpdate();
        }

        public override void OnLateUpdate()
        {
            entitiesContainerController.OnLateUpdate();
            gameplayCameraController.OnLateUpdate();
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
            
            entitiesContainerController.OnDestroy();
        }

        private void PresentMainMenuState()
        {
            PresentState(new MainMenuStateController(Context));
        }

        private void Cheats()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                for (var i = 0; i < 10; i++)
                {
                    wavesController.CreateEnemyAtRandomPosition();
                }
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                entitiesContainerController.RemoveRandomEnemy();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                entitiesContainerController.RemoveAllEnemies();
            }
        }

        public Transform GetGameplayLayer(GameplayLayer gameplayLayer)
        {
            return WorldView.GetLayer(gameplayLayer);
        }
        
        private void CreateJoystickController()
        {
            var joystickConfig = GetStateAsset<JoystickConfig>();
            joystickController = ControllerViewFactory.CreateControllerView<JoystickController>(joystickConfig.JoystickView, null, joystickConfig);
            joystickController.JoystickView.Activate(UiView.transform, new Vector3(Screen.width * joystickConfig.xPosition, Screen.height * joystickConfig.yPosition));
        }

        private void CreateCameraController()
        {
            var cameraView = GetStateAsset<CameraConfig>().Camera.GetComponent<GameplayCameraView>();
            gameplayCameraController = ControllerViewFactory.CreateControllerView<GameplayCameraController>(cameraView, null);
            gameplayCameraController.GameplayCameraView.Activate(WorldView.transform, new Vector3(0f, 0f, -1f));
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
                Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry(userModel.EquipmentModel.EquippedWeapon.ItemId).WeaponGameplayView,
                Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry(userModel.EquipmentModel.EquippedWeapon.ItemId).ProjectileGameplayView,
                Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry(userModel.EquipmentModel.EquippedWeapon.ItemId).WeaponConfig,
            });
            
            // Enemies
            var levelModel = Context.ModelFactory.GetLevelModel();
            foreach (var enemyId in levelModel.GetCurrentWaveEnemiesIds())
            {
                Preloader.AddReference(Context.CatalogsHolder.EnemiesCatalog.GetCatalogEntry(enemyId).EnemyGameplayView);
            }

            var task = Preloader.LoadAsync();

            return task;
        }
    }
}