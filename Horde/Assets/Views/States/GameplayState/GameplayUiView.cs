using System;
using ScreenMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Views.States.GameplayState
{
    public class GameplayUiView : UiView
    {
        [SerializeField] private Button MainMenuButton;
        
        public event Action MainMenuClicked;

        public override void Init()
        {
            MainMenuButton.onClick.AddListener(FireMainMenuButton);
        }

        public override void OnUpdate()
        {
            
        }

        private void OnDestroy()
        {
            MainMenuButton.onClick.RemoveListener(FireMainMenuButton);
        }

        private void FireMainMenuButton()
        {
            MainMenuClicked?.Invoke();
        }
    }
}