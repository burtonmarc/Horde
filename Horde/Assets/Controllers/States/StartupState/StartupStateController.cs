using System.Collections;
using System.Threading.Tasks;
using Controllers.States.MainMenuState;
using Data;
using Game.States.StartupState;
using ScreenMachine;
using UnityEngine;

namespace Controllers.States.StartupState
{
    public class StartupStateController : BaseStateController<StartupStateUiView, StartupStateWorldView>, IStateBase
    {
        protected override string StateId { get; }

        public StartupStateController(Context context) : base(context)
        {
            StateId = "Startup";
        }

        public void OnCreate()
        {
            UiView.ResetUiView();
            WorldView.Init();

            LoadSharedAddressablesAsync();
        }

        private async void LoadSharedAddressablesAsync()
        {
            await Task.Yield();
            PresentState(new MainMenuStateController(Context));

        }

        private void LoadWeapons()
        {
            
        }

        public void OnBringToFront()
        {
            
        }

        public void OnSendToBack()
        {
            
        }
    }
}