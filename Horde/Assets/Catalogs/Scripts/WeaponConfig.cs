using UnityEngine;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "Configs", menuName = "ScriptableObjects/Configs/Create Weapon Config", order = 1)]
    public class WeaponConfig : ScriptableObject
    {
        public float BaseDamage;

        public float HitSpeed;

        public bool AutoAim;
    }
}