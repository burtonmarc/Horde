using System;
using Catalogs.Scripts;

namespace Data
{
    [Serializable]
    public class EnemyModel : IModel
    {
        public int HitPoints;

        public float MovementSpeed;

        public int HitDamage;

        public EnemyModel(EnemyConfig enemyConfig)
        {
            HitPoints = enemyConfig.HitPoints;

            MovementSpeed = enemyConfig.MovementSpeed;

            HitDamage = enemyConfig.HitDamage;
        }
    }
}