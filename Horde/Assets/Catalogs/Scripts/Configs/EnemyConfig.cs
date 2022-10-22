using UnityEngine;

namespace Catalogs.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/Configs/Create Enemy Config", order = 1)]
    public class EnemyConfig : ScriptableObject
    {
        public int HitPoints;

        public float MovementSpeed;
        
        public int HitDamage;
    }
}