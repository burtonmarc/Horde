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
            UiView.Init();
            WorldView.Init();
            
            PresentStateAsync();
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

        private async void PresentStateAsync()
        {
            await Task.Yield();
            PresentState(new MainMenuStateController(Context));
        }
    }
}