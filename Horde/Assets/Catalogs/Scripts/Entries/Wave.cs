using System;

namespace Catalogs.Scripts.Entries
{
    [Serializable]
    public class Wave
    {
        public string EnemyId;
        
        public int EnemiesInWave;

        public float TimeUntilNextWave;

        public float TimeBetweenEnemySpawn;
    }
}