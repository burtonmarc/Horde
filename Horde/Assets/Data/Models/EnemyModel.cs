using System;
using System.Collections.Generic;
using Catalogs.Scripts.Configs;

namespace Data.Models
{
    [Serializable]
    public class EnemiesTitleData : ITitleData
    {
        public List<EnemyConfig> AllEnemiesTitleData;
    }
    
    [Serializable]
    public class EnemyConfig
    {
        public string EnemyId;
        
        public int BaseHitPoints;

        public float BaseMovementSpeed;
        
        public int BaseHitDamage;
    }
    
    [Serializable]
    public class EnemyModel : ModelWithTitleData<EnemiesTitleData>, IModel
    {
        public int HitPoints;

        public float MovementSpeed;

        public int HitDamage;

        public void Init(string enemyId)
        {
            var enemyConfig = TitleData.AllEnemiesTitleData.Find(enemy => enemy.EnemyId == enemyId);
            
            HitPoints = enemyConfig.BaseHitPoints;

            MovementSpeed = enemyConfig.BaseMovementSpeed;

            HitDamage = enemyConfig.BaseHitDamage;
        }
    }
}