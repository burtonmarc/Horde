using Catalogs.Scripts.Entries;
using UnityEngine;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "ItemsCatalog", menuName = "ScriptableObjects/Catalogs/Create Items Catalog", order = 1)]
    public class ItemsCatalog : CatalogEntryListBase<ItemCatalogEntry>
    {
        
    }
}