using System;
using ScreenMachine;
using TMPro;
using UnityEngine.UI;
using Views.States.MainMenuState.Views;

namespace Views.States.MainMenuState
{
    public class MainMenuUiView : UiView

    {
        public TextMeshProUGUI UserLevel;
        
        public Button LevelUpButton;
        
        public Button StartGameButton;

        public EquippedItemView EquippedItemPrefab;

        public event Action LevelUpClicked;
        public event Action StartGameClicked;

        public override void ResetUiView()
        {
            
        }

        public void PopulateUiView(int userLevel)
        {
            SetUserLevel(userLevel);
        }

        public override void OnUpdate()
        {
            
        }
        
        private void OnEnable()
        {
            LevelUpButton.onClick.AddListener(FireLevelUpButton);
            StartGameButton.onClick.AddListener(FireStartGameButton);
        }

        private void OnDisable()
        {
            LevelUpButton.onClick.RemoveListener(FireLevelUpButton);
            StartGameButton.onClick.RemoveListener(FireStartGameButton);
        }

        private void OnDestroy()
        {
            
        }

        public void SetUserLevel(int userLevel)
        {
            UserLevel.text = userLevel.ToString();
        }

        private void FireLevelUpButton()
        {
            LevelUpClicked?.Invoke();
        }

        private void FireStartGameButton()
        {
            StartGameClicked?.Invoke();
        }
    }
}