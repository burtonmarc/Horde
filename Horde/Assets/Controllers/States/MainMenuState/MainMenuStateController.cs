using System;
using System.Threading.Tasks;
using Catalogs.Scripts.Configs;
using Controllers.States.GameplayState;
using Data;
using Game.States.MainMenu;
using ScreenMachine;
using UnityEngine;
using Views.States.GameplayState;
using Views.States.MainMenuState;
using Object = UnityEngine.Object;

namespace Controllers.States.MainMenuState
{
    public class MainMenuStateController : BaseStateController<MainMenuUiView, MainMenuWorldView>, IStateBase, IPreloadable
    {
        protected override string StateId { get; }

        private UserModel userModel;

        private GameObject camera;
        
        public MainMenuStateController(Context context) : base(context)
        {
            StateId = "MainMenu";
            userModel = context.UserModel;
        }
        
        public void OnCreate()
        {
            UiView.ResetUiView();
            WorldView.Init();
            
            UiView.PopulateUiView(userModel.level);

            UiView.LevelUpClicked += LevelUp;
            UiView.StartGameClicked += PresentGameplayState;

            CreateCameraController();
        }

        public void OnBringToFront()
        {
            
        }

        public void OnSendToBack()
        {
            
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            
            Object.Destroy(camera);
        }
        
        private void CreateCameraController()
        {
            var cameraReference = GetStateAsset<CameraConfig>().Camera;
            camera = Object.Instantiate(cameraReference, WorldView.transform);
        }
        
        private void LevelUp()
        {
            userModel.level++;
            UiView.SetUserLevel(userModel.level);
            Context.SaveSystem.SaveModel(userModel);
        }

        private void PresentGameplayState()
        {
            PresentState(new GameplayStateController(Context));
        }

        public Task Preload()
        {
            Preloader = Context.AssetLoaderFactory.CreateLoader(StateId);
            
            Preloader.AddReference(Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry("Shuriken").WeaponConfig);

            var task = Preloader.LoadAsync();

            return task;
        }
    }
}