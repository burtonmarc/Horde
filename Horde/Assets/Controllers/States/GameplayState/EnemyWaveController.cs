using System.Collections.Generic;
using Catalogs.Scripts;
using Controllers.States.GameplayState.GameplayExtensions;
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
        
        public override void OnFixedUpdate()
        {
            foreach (var waveEntity in waveEntities)
            {
                waveEntity.OnFixedUpdate();
            }
        }

        public override void OnDestroy()
        {
            for (var index = waveEntities.Count - 1; index >= 0; index--)
            {
                var waveEntity = waveEntities[index];
                waveEntity.OnDestroy();
            }
            base.OnDestroy();
        }

        public void CreateEnemyAtRandomPosition()
        {
            var enemyEntry = Context.CatalogsHolder.EnemiesCatalog.GetCatalogEntry("EnemyTest");
            var enemyView = Context.Preloader.GetAsset<EnemyView>(enemyEntry.EnemyGameplayView);
            var enemyConfig = Context.Preloader.GetAsset<EnemyConfig>(enemyEntry.EnemyConfig);
            var enemyModel = new EnemyModel(enemyConfig);
            var enemyController = ControllerFactory.CreateController<EnemyController>(enemyView, enemyModel);
            waveEntities.AddController(enemyController);

            var aspectRatio = (float) Screen.height / Screen.width;
            var orthographicSize = Camera.main.orthographicSize;
            var randomX = Random.Range(-orthographicSize / aspectRatio, orthographicSize / aspectRatio);
            var randomY = Random.Range(-orthographicSize, orthographicSize);
            var randomPosition = new Vector3(randomX, randomY, 0);

            var enemyLayer = Context.GetGameplayLayer(GameplayLayer.Enemies);
            enemyController.EnemyView.Activate(enemyLayer, randomPosition);
        }

        public void RemoveRandomEnemy()
        {
            var index = Random.Range(0, waveEntities.Count - 1);
            waveEntities[index].OnDestroy();
        }

        public void RemoveAllEnemies()
        {
            for (var index = waveEntities.Count - 1; index >= 0; index--)
            {
                var waveEntity = waveEntities[index];
                waveEntity.OnDestroy();
            }
        }
    }
}