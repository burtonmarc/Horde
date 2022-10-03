using System;
using ScreenMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Game.States.MainMenu
{
    public class MainMenuUiView : UiView

    {
        public Button StartGameButton;
        
        public event Action StartGameClicked;

        public override void Init()
        {
            StartGameButton.onClick.AddListener(FireStartGameButton);
        }

        public override void OnUpdate()
        {
            
        }

        private void OnDestroy()
        {
            StartGameButton.onClick.RemoveListener(FireStartGameButton);
        }

        private void FireStartGameButton()
        {
            StartGameClicked?.Invoke();
        }
    }
}