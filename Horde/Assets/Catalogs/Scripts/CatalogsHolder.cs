using UnityEngine;

namespace Data.Scripts
{
    [CreateAssetMenu(fileName = "Catalogs", menuName = "ScriptableObjects/Catalogs/Create Catalogs catalog", order = 1)]
    public class CatalogsHolder : ScriptableObject
    {
        public StatesCatalog StatesCatalog;
    }
    
    
}