using Catalogs.Scripts.Configs;
using Catalogs.Scripts.Entries;
using Data;
using Data.Models;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class WavesController : GameplayControllerBase
    {
        private LevelModel levelModel;
        
        private bool levelFinished;

        private Wave currentWave;

        private float currentWaveTime;

        private float timeObjectiveForNextWave;

        private float spawnTimeBetweenEnemies;
        
        public WavesController(Context context) : base(context)
        {
            
        }

        public override void Init(SaveableBaseModel model, object args = null)
        {
            base.Init(model, args);

            if (model is LevelModel lm)
            {
                levelModel = lm;
                //currentWave = levelModel.Waves[levelModel.CurrentWave];
            }

            StartNewWave();
        }

        private void StartNewWave()
        {
            //currentWave = 
            //timeObjectiveForNextWave = currentWaveTime + 
        }

        public override void OnUpdate()
        {
            if (levelFinished) return;
            
            //currentTimeUntilNextWave += Time.deltaTime;

            CheckForNextWave();

            CheckToSpawnNextEnemy();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
        
        private void CheckForNextWave()
        {
            //if (currentTimeUntilNextWave >= currentWave.TimeUntilNextWave)
            //{
            //    currentTimeUntilNextWave = 0;
            //    currentAmountOfEnemiesSpawnedOfCurrentWave = 0;
            //    if (levelModel.CurrentWaveCount + 1 < levelModel.Waves.Count)
            //    {
            //        levelModel.CurrentWaveCount++;
            //        currentWave = levelModel.Waves[levelModel.CurrentWaveCount];
            //    }
            //    else
            //    {
            //        wavesFinished = true;
            //    }
            //}
        }

        private void CheckToSpawnNextEnemy()
        {
            //currentTimeBetweenEnemySpawns += Time.deltaTime;
//
            //if (currentTimeBetweenEnemySpawns >= currentWave.TimeBetweenEnemySpawn && currentAmountOfEnemiesSpawnedOfCurrentWave < currentWave.EnemiesInWave - 1)
            //{
            //    currentTimeBetweenEnemySpawns = 0;
            //    currentAmountOfEnemiesSpawnedOfCurrentWave++;
            //    CreateEnemyAtRandomPosition();
            //}
        }

        public void CreateEnemyAtRandomPosition()
        {
            var enemyEntry = Context.CatalogsHolder.EnemiesCatalog.GetCatalogEntry("EnemyTest");
            
            var enemyView = Context.Preloader.GetAsset<EnemyView>(enemyEntry.EnemyGameplayView);
            
            var enemyConfig = Context.Preloader.GetAsset<EnemyConfig>(enemyEntry.EnemyConfig);
            
            var enemyModel = Context.ModelFactory.GetEnemyModel();
            enemyModel.InjectDependencies(enemyConfig);
            
            var enemyController = ControllerViewFactory.CreateControllerView<EnemyController>(enemyView, enemyModel);
            enemyController.Activate();
            
            var entityArgs = new EntityArgs {EntityType = EntityType.Enemy, Entity = enemyController};
            OnGameplayEvent(GameplayEvent.AddEntity, entityArgs);
            
            var orthographicSize = Camera.main.orthographicSize;
            var randomX = Random.Range(-orthographicSize / GameplayUtils.aspectRatio + PlayerController.PlayerPosition.x, orthographicSize / GameplayUtils.aspectRatio + PlayerController.PlayerPosition.x);
            var randomY = Random.Range(-orthographicSize + PlayerController.PlayerPosition.y, orthographicSize + PlayerController.PlayerPosition.y);
            var randomPosition = new Vector3(randomX, randomY, 0);

            var enemyLayer = GameplayUtils.GetGameplayLayer(Context, GameplayLayer.Enemies);
            enemyController.EnemyView.Activate(enemyLayer, randomPosition);
        }
    }
}