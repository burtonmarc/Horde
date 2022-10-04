using ScreenMachine;
using UnityEngine;

namespace Views.States.GameplayState
{
    public class GameplayWorldView : WorldView
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
        
        public EnemyView InstantiateEnemy()
        {
            return Instantiate(EnemyView, EnemiesLayer);
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