using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "Weapon Entry", menuName = "ScriptableObjects/Entries/Create Weapon Entry", order = 1)]
    public class WeaponCatalogEntry : CatalogEntryId
    {
        public float Damage = 1;

        public bool AutoAim = true;

        public float Range = 1;

        public float TravelSpeed = 1;

        public AssetReference CharacterWeapon;

        public AssetReference Projectile;
    }
}