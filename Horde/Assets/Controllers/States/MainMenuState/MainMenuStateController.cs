using System;
using System.Threading.Tasks;
using Controllers.States.GameplayState;
using Data;
using Game.States.MainMenu;
using ScreenMachine;
using Views.States.MainMenuState;

namespace Controllers.States.MainMenuState
{
    public class MainMenuStateController : BaseStateController<MainMenuUiView, MainMenuWorldView>, IStateBase, IPreloadable
    {
        protected override string StateId { get; }

        private UserModel userModel;
        
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