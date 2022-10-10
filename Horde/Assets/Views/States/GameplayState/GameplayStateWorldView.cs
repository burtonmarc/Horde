using System;
using ScreenMachine;
using UnityEngine;

namespace Views.States.GameplayState
{
    public enum GameplayLayer
    {
        Background,
        Enemies,
        Effects
    }
    public class GameplayStateWorldView : WorldView
    {
        [SerializeField] private PlayerView PlayerView;
        [SerializeField] private EnemyView EnemyView;

        [SerializeField] private Transform BackgroundLayer;
        [SerializeField] private Transform EnemiesLayer;
        [SerializeField] private Transform EffectsLayer;
        
        public PlayerView InstantiatePlayer()
        {
            return Instantiate(PlayerView, EnemiesLayer);
        }
        
        public Transform GetLayer(GameplayLayer gameplayLayer)
        {
            switch (gameplayLayer)
            {
                case GameplayLayer.Background:
                    return BackgroundLayer;
                case GameplayLayer.Enemies:
                    return EnemiesLayer;
                case GameplayLayer.Effects:
                    return EffectsLayer;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameplayLayer), gameplayLayer, null);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var screenAspectRatio = Screen.height / Screen.width;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Camera.main.transform.position, new Vector3(Camera.main.orthographicSize * screenAspectRatio,Camera.main.orthographicSize * 2f, 0.4f));
        }
#endif
    }
}