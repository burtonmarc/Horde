using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Catalogs.Scripts
{
    [CreateAssetMenu(fileName = "Catalogs", menuName = "ScriptableObjects/Entries/Create State Entry", order = 1)]
    public class StateCatalogEntry : CatalogEntryId
    {
        public AssetReference UiView;
        
        public AssetReference WorldView;
        
        public List<AssetReference> StateAssetReferences;
        
        public List<AssetReference> GetAllStateReferences()
        {
            var stateReferences = new List<AssetReference>();
            stateReferences.Add(UiView);
            stateReferences.Add(WorldView);
            
            foreach (var stateAsset in StateAssetReferences)
            {
                stateReferences.Add(stateAsset);
            }
            return stateReferences;
        }
    }
}