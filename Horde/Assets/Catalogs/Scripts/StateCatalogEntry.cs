using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Data.Scripts
{
    
    
    [CreateAssetMenu(fileName = "Catalogs", menuName = "ScriptableObjects/Catalogs/Create State Catalog Entry", order = 1)]
    public class StateCatalogEntry : CatalogEntryId
    {
        public AssetReference UiView;
        
        public AssetReference WorldView;
        
        public List<AssetReference> StateAssetReferences;

        public List<AssetReference> GetAllStateReferences()
        {
            var stateReferences = new List<AssetReference> {UiView, WorldView};
            foreach (var stateAsset in StateAssetReferences)
            {
                stateReferences.Add(stateAsset);
            }
            return stateReferences;
        }
    }
}