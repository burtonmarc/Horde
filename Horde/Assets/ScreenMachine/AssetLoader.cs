using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace ScreenMachine
{
    public class AssetLoader
    {
        private List<AssetReference> assetsToLoad = new List<AssetReference>();
        
        private Dictionary<AssetReference, AsyncOperationHandle<Object>> assetsLoaded = new Dictionary<AssetReference, AsyncOperationHandle<Object>>();

        public void AddReference(AssetReference reference)
        {
            if (assetsToLoad.Contains(reference) || assetsLoaded.ContainsKey(reference))
            {
                return;
            }

            assetsToLoad.Add(reference);
        }

        public void AddReferences(IEnumerable<AssetReference> references)
        {
            foreach (var reference in references)
            {
                if (assetsToLoad.Contains(reference) || assetsLoaded.ContainsKey(reference))
                {
                    return;
                }

                assetsToLoad.Add(reference);
            }
        }

        public T GetAsset<T>(AssetReference reference) where  T : Object
        {
            var asset = assetsLoaded[reference].Result;
            
            if (asset is ScriptableObject)
            {
                return asset as T;
            }

            if (asset is GameObject go)
            {
                go.TryGetComponent(typeof(T), out var component);
                if (component == null)
                {
                    throw new NullReferenceException($"Couldn't find type of {typeof(T).FullName} in {reference}");
                }
                return go.GetComponent<T>();
            }
            
            throw new NotSupportedException($"Couldn't find type of {typeof(T).FullName}");
        }

        public Task LoadAsync()
        {
            var taskList = new List<Task>(assetsToLoad.Count);

            LoadAssets(taskList);

            return Task.WhenAll(taskList);
        }

        private void LoadAssets(List<Task> taskList)
        {
            foreach (var assetToLoad in assetsToLoad)
            {
                var handle = Addressables.LoadAssetAsync<Object>(assetToLoad);

                if (assetsLoaded.ContainsKey(assetToLoad))
                {
                    throw new NotSupportedException($"Trying to load asset {assetToLoad} twice! Check asset loader");
                }
                
                taskList.Add(handle.Task);
                assetsLoaded[assetToLoad] = handle;
            }
            
            assetsToLoad.Clear();
        }

        public void ReleaseAssets()
        {
            if (assetsToLoad.Count > 0)
            {
                throw new NotSupportedException(
                    "There are still some assets to load in the loader that weren't loaded yet");
            }

            foreach (var assetLoaded in assetsLoaded)
            {
                var handle = assetLoaded.Value;

                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }
            
            assetsLoaded.Clear();
        }
    }
}















