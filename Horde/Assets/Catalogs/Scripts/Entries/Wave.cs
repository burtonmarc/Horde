using System;
using System.Collections.Generic;
using UnityEngine;

namespace Catalogs.Scripts.Entries
{
    [Serializable]
    public class Wave
    {
        [Serializable]
        public struct SpawnableEnemiesWithProbability
        {
            public string EnemyId;

            [Range(0, 100)]
            public int SpawnProbability;
        }
        
        public List<SpawnableEnemiesWithProbability> SpawnableEnemies;
        
        public int EnemiesSpawnedPerSecond;

        public float WaveTimeInSeconds;
    }
}