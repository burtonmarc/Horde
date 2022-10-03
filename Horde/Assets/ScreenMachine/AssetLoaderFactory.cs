using System.Collections.Generic;

namespace ScreenMachine
{
    public class AssetLoaderFactory
    {
        private Dictionary<string, List<AssetLoader>> stateAssetLoaders = new Dictionary<string, List<AssetLoader>>();

        public AssetLoader CreateLoader(string stateId)
        {
            var loader = new AssetLoader();

            if (!stateAssetLoaders.ContainsKey(stateId))
            {
                stateAssetLoaders[stateId] = new List<AssetLoader>();
            }
            
            stateAssetLoaders[stateId].Add(loader);

            return loader;
        }

        public void ReleaseStateLoadedAssets(string stateId)
        {
            if (!stateAssetLoaders.ContainsKey(stateId))
            {
                return;
            }

            foreach (var assetLoader in stateAssetLoaders[stateId])
            {
                assetLoader.ReleaseAssets();
            }

            stateAssetLoaders.Remove(stateId);
        }
        
    }
}