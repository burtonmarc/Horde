using System;
using ScreenMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Views.States.GameplayState
{
    public class GameplayStateUiView : UiView
    {
        [SerializeField] private Button MainMenuButton;
        
        public event Action MainMenuClicked;

        public override void Init()
        {
            
        }

        public override void OnUpdate()
        {
            
        }
        
        private void OnEnable()
        {
            MainMenuButton.onClick.AddListener(FireMainMenuButton);
        }

        private void OnDisable()
        {
            MainMenuButton.onClick.RemoveListener(FireMainMenuButton);
        }

        private void OnDestroy()
        {
            
        }

        private void FireMainMenuButton()
        {
            MainMenuClicked?.Invoke();
        }
    }
}