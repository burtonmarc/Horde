using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "WeaponEntry", menuName = "ScriptableObjects/Entries/Create Weapon Entry", order = 1)]
    public class WeaponCatalogEntry : CatalogEntryId
    {
        public AssetReference WeaponGameplayView;
        
        public AssetReference ProjectileGameplayView;

        public AssetReference WeaponConfig;
    }
}