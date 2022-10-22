using Catalogs.Scripts.Entries;
using UnityEngine;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "States Catalog", menuName = "ScriptableObjects/Catalogs/Create States Catalog", order = 1)]   
    public class StatesCatalog : CatalogEntryListBase<StateCatalogEntry>
    {
        
    }
}