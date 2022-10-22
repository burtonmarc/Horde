using Catalogs.Scripts.Entries;
using UnityEngine;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "LevelsCatalog", menuName = "ScriptableObjects/Catalogs/Create Levels Catalog", order = 1)]
    public class LevelsCatalog : CatalogEntryListBase<LevelEntry>
    {
        
    }
}