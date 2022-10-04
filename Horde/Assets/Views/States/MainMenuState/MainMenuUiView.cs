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
            
        }

        public override void OnUpdate()
        {
            
        }
        
        private void OnEnable()
        {
            StartGameButton.onClick.AddListener(FireStartGameButton);
        }

        private void OnDisable()
        {
            StartGameButton.onClick.RemoveListener(FireStartGameButton);
        }

        private void OnDestroy()
        {
            
        }

        private void FireStartGameButton()
        {
            StartGameClicked?.Invoke();
        }
    }
}