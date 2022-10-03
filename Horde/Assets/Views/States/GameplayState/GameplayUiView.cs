using System;
using ScreenMachine;
using UnityEngine.UI;

namespace Game.States.Gameplay
{
    public class GameplayUiView : UiView
    {
        public Button MainMenuButton;
        
        public Action MainMenuButtonClicked;

        public override void Init()
        {
            MainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        public override void OnUpdate()
        {
            
        }

        private void OnDestroy()
        {
            MainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
            MainMenuButtonClicked?.Invoke();
        }
    }
}