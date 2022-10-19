using System.Collections.Generic;
using Catalogs.Scripts;
using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class EnemyWaveController : GameplayControllerBase
    {
        // TODO: Get wave data from scriptable in context
        
        public EnemyWaveController(Context context) : base(context)
        {
        }

        public override void OnUpdate()
        {
            
        }
        
        public override void OnFixedUpdate()
        {
            
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void CreateEnemyAtRandomPosition()
        {
            var enemyEntry = Context.CatalogsHolder.EnemiesCatalog.GetCatalogEntry("EnemyTest");
            var enemyView = Context.Preloader.GetAsset<EnemyView>(enemyEntry.EnemyGameplayView);
            var enemyConfig = Context.Preloader.GetAsset<EnemyConfig>(enemyEntry.EnemyConfig);
            var enemyModel = new EnemyModel(enemyConfig);
            var enemyController = ControllerFactory.CreateController<EnemyController>(enemyView, enemyModel);
            enemyController.Activate();
            var entityArgs = new EntityArgs {EntityType = EntityType.Enemy, Entity = enemyController};
            OnGameplayEvent(GameplayEvent.AddEntity, entityArgs);
            
            var orthographicSize = Camera.main.orthographicSize;
            var randomX = Random.Range(-orthographicSize / GameplayExtensions.aspectRatio, orthographicSize / GameplayExtensions.aspectRatio);
            var randomY = Random.Range(-orthographicSize, orthographicSize);
            var randomPosition = new Vector3(randomX, randomY, 0);

            var enemyLayer = Context.GetGameplayLayer(GameplayLayer.Enemies);
            enemyController.EnemyView.Activate(enemyLayer, randomPosition);
        }
    }
}