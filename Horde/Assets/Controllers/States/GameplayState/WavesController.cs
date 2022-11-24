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

        private float currentWaveTime;

        private float nextWaveTimeObjective;

        private float currentSpawnTime;

        private float spawnTime;

        public WavesController(Context context) : base(context) { }

        public override void Init(IModel model, object args = null)
        {
            base.Init(model, args);

            if (model is LevelModel lm)
            {
                levelModel = lm;
            }

            levelFinished = false;
            currentWaveTime = 0;
            nextWaveTimeObjective = levelModel.CurrentWave.WaveTimeInSeconds;
            currentSpawnTime = 0;
            spawnTime = 1f / levelModel.CurrentWave.EnemiesSpawnedPerSecond + levelModel.TimeForFirstSpawn;
        }

        public override void OnUpdate()
        {
            if (levelFinished) return;
            
            currentWaveTime += Time.deltaTime;
            CheckForNextWave();
            
            currentSpawnTime += Time.deltaTime;
            CheckToSpawnNextEnemy();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
        
        private void CheckForNextWave()
        {
            if (currentWaveTime >= nextWaveTimeObjective)
            {
                Debug.Log("Switch to next wave");
                SwitchToNextWave();
            }
        }

        private void SwitchToNextWave()
        {
            if (levelModel.WasLastWave())
            {
                Debug.Log("Was last wave");
                levelFinished = true;
                return;
            }
            
            
            levelModel.UpdateWaveIndex();
            nextWaveTimeObjective += levelModel.CurrentWave.WaveTimeInSeconds;
            currentSpawnTime = 0;
            spawnTime = 1f / levelModel.CurrentWave.EnemiesSpawnedPerSecond;
        }

        private void CheckToSpawnNextEnemy()
        {
            if (currentSpawnTime >= spawnTime)
            {
                currentSpawnTime = 0;
                spawnTime = 1f / levelModel.CurrentWave.EnemiesSpawnedPerSecond;
                CreateEnemyAtRandomPosition();
            }
        }

        public void CreateEnemyAtRandomPosition()
        {
            Debug.Log("Spawn Enemy");

            var enemyId = levelModel.GetEnemyIdToSpawn();
            
            var enemyEntry = Context.CatalogsHolder.EnemiesCatalog.GetCatalogEntry(enemyId);
            
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