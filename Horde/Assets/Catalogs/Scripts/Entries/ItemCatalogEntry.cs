using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Catalogs.Scripts.Entries
{
    [CreateAssetMenu(fileName = "ItemEntry", menuName = "ScriptableObjects/Entries/Create Item Entry", order = 1)]
    public class ItemCatalogEntry : CatalogEntryId
    {
        public AssetReference ItemIcon;

        public ItemType ItemType;

        public int ItemBaseAttack;

        public int ItemBaseHealthPoints;
    }
}