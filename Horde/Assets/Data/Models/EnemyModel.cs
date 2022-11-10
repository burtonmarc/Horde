using System;
using Catalogs.Scripts.Configs;

namespace Data.Models
{
    [Serializable]
    public class EnemyModel : IModel
    {
        public int HitPoints;

        public float MovementSpeed;

        public int HitDamage;

        public void InjectDependencies(EnemyConfig enemyConfig)
        {
            HitPoints = enemyConfig.HitPoints;

            MovementSpeed = enemyConfig.MovementSpeed;

            HitDamage = enemyConfig.HitDamage;
        }
    }
}