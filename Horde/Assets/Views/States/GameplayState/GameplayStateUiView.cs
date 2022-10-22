using System;
using System.Globalization;
using ScreenMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Views.States.GameplayState
{
    public class GameplayStateUiView : UiView
    {
        [SerializeField] private Button MainMenuButton;

        [SerializeField] private TextMeshProUGUI FrameRate;
        
        public event Action MainMenuClicked;

        private int currentFrame = 0;

        public override void ResetUiView()
        {
            
        }

        public override void OnUpdate()
        {
            if (currentFrame <= 0)
            {
                FrameRate.text = Mathf.FloorToInt(1f / Time.deltaTime).ToString(CultureInfo.InvariantCulture);
                currentFrame = 4;
            }
            currentFrame--;
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