using System;
using ScreenMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Game.States.MainMenu
{
    public class MainMenuUiView : UiView

    {
        public Button StartGameButton;
        
        public Action StartGameButtonClicked;

        public override void Init()
        {
            StartGameButton.onClick.AddListener(OnStartGameButtonClicked);
        }

        public override void OnUpdate()
        {
            
        }

        private void OnDestroy()
        {
            StartGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
        }

        private void OnStartGameButtonClicked()
        {
            StartGameButtonClicked?.Invoke();
        }
    }
}