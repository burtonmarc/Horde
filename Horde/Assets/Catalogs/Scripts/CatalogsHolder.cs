using UnityEngine;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "Catalogs", menuName = "ScriptableObjects/Catalogs/Create Main Catalog's Catalog", order = 1)]
    public class CatalogsHolder : ScriptableObject
    {
        public StatesCatalog StatesCatalog;

        public WeaponsCatalog WeaponsCatalog;
    }
    
    
}