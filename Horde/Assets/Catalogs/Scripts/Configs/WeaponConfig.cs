using UnityEngine;

namespace Catalogs.Scripts.Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/Configs/Create Weapon Config", order = 1)]
    public class WeaponConfig : ScriptableObject
    {
        public Sprite WeaponIcon;
        
        public float BaseDamage;

        public float HitSpeed;

        public float ProjectileSpeed;

        public bool AutoAim;
    }
}