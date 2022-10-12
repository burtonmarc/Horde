using System;
using Controllers.States.GameplayState;
using Data;
using Game.States.MainMenu;
using ScreenMachine;
using Views.States.MainMenuState;

namespace Controllers.States.MainMenuState
{
    public class MainMenuStateController : BaseStateController<MainMenuUiView, MainMenuWorldView>, IStateBase
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

        public void OnDestroy()
        {
            
        }
        
        private void LevelUp()
        {
            userModel.level++;
            UiView.SetUserLevel(userModel.level);
            Context.SaveSystem.SaveUserData(userModel);
        }

        private void PresentGameplayState()
        {
            PresentState(new GameplayStateController(Context));
        }
    }
}