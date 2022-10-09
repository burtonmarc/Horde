using System.Collections.Generic;
using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class EnemyWaveController : GameplayControllerBase
    {
        private List<GameplayControllerBase> waveEntities;
        
        // TODO: Get wave data from scriptable in context
        
        public EnemyWaveController(Context context) : base(context)
        {
            waveEntities = new List<GameplayControllerBase>();
        }

        public override void OnUpdate()
        {
            foreach (var waveEntity in waveEntities)
            {
                waveEntity.OnUpdate();
            }
        }

        public override void OnDestroy()
        {
            foreach (var waveEntity in waveEntities)
            {
                waveEntity.OnDestroy();
            }
        }

        public void CreateEnemyAtRandomPosition()
        {
            var enemyController = ControllerFactory.CreateController<EnemyController>(Context, PlayerController);
            waveEntities.Add(enemyController);

            var aspectRatio = (float) Screen.height / Screen.width;
            var orthographicSize = Camera.main.orthographicSize;
            var randomX = Random.Range(-orthographicSize / aspectRatio, orthographicSize / aspectRatio);
            var randomY = Random.Range(-orthographicSize, orthographicSize);
            var randomPosition = new Vector3(randomX, randomY, 0);

            var gameplayState = Context.ScreenMachine.CurrentState as GameplayStateController;
            var enemyLayer = gameplayState.GetGameplayLayer(GameplayLayer.Enemies);
            enemyController.EnemyView.Activate(enemyLayer, randomPosition);
        }

        public void RemoveRandomEnemy()
        {
            var index = Random.Range(0, waveEntities.Count - 1);
            waveEntities[index].OnDestroy();
            waveEntities.RemoveAt(index);
        }
    }
}