using System;
using System.Collections.Generic;
using System.Linq;
using Catalogs.Scripts.Entries;
using Random = UnityEngine.Random;

namespace Data.Models
{
    [Serializable]
    public class LevelTitleData : ITitleData
    {
        // TODO: Separate the level data (current level you are playing) and the Levels data (maybe MainChapterData?)
        public float TimeForFirstSpawn;
        public List<Wave> Waves;
    }
    
    [Serializable]
    public class LevelUserData : IUserData
    {
        public int CurrentWaveIndex;
    }
    
    public class LevelModel : ModelWithUserDataAndTitleData<LevelUserData, LevelTitleData>, IModel
    {
        // References
        
        // Unsaved Data
        private LevelTitleData levelConfig;
        public Wave CurrentWave => levelConfig.Waves[UserData.CurrentWaveIndex];
        //public List<Wave> Waves => levelConfig.Waves;
        public float TimeForFirstSpawn => levelConfig.TimeForFirstSpawn;
        
        // Saved Data
        public int CurrentWaveIndex
        {
            get => UserData.CurrentWaveIndex;
            private set
            {
                UserData.CurrentWaveIndex = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }

        public LevelModel()
        {
            
        }

        public void UpdateWaveIndex()
        {
            CurrentWaveIndex += 1;
        }

        public bool WasLastWave()
        {
            return CurrentWaveIndex == levelConfig.Waves.Count - 1;
        }

        public string GetEnemyIdToSpawn()
        {
            var totalWeight = CurrentWave.SpawnableEnemies.Sum(enemy => enemy.SpawnProbability);

            var randomWeight = Random.Range(0, totalWeight);

            var selectedEnemy = CurrentWave.SpawnableEnemies.First(enemy => (randomWeight -= enemy.SpawnProbability) < 0);

            return selectedEnemy.EnemyId;
        }
    }
}