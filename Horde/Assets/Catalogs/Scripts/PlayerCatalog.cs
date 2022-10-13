using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "PlayerCatalog", menuName = "ScriptableObjects/Catalogs/Create Player Catalog", order = 1)]
    public class PlayerCatalog : ScriptableObject
    {
        public AssetReference MainMenuView;

        public AssetReference GameplayView;
        
        public int Health;

        public float MovementSpeed;
    }
}