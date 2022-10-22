using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Catalogs.Scripts.Entries
{
    [CreateAssetMenu(fileName = "EnemyEntry", menuName = "ScriptableObjects/Entries/Create Enemy Entry", order = 1)]
    public class EnemyEntry : CatalogEntryId
    {
        public AssetReference EnemyGameplayView;

        public AssetReference EnemyConfig;
    }
}