using UnityEngine;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "Catalogs", menuName = "ScriptableObjects/Catalogs/Create Main Catalog's Catalog", order = 1)]
    public class CatalogsHolder : ScriptableObject
    {
        public StatesCatalog StatesCatalog;

        public PlayerCatalog PlayerCatalog;

        public ItemsCatalog ItemsCatalog;

        public WeaponsCatalog WeaponsCatalog;

        public EnemiesCatalog EnemiesCatalog;

        public LevelsCatalog LevelsCatalog;
    }
}