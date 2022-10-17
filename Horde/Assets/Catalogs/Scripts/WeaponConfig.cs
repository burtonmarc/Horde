using UnityEngine;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/Configs/Create Weapon Config", order = 1)]
    public class WeaponConfig : ScriptableObject
    {
        public Sprite WeaponIcon;
        
        public float BaseDamage;

        public float HitSpeed;

        public bool AutoAim;
    }
}