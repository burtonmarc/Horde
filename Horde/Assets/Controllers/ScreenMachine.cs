using System;
using System.Collections.Generic;
using Data.Scripts;
using ScreenMachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class ScreenMachine : IScreenMachine
    {
        
        private Stack<IStateBase> screenStack;

        private StatesCatalog statesCatalog;

        private readonly AssetLoaderFactory assetLoaderFactory = new AssetLoaderFactory();

        private bool isLoading;

        public ScreenMachine(StatesCatalog statesCatalog, AssetLoaderFactory assetLoaderFactory) {
            this.statesCatalog = statesCatalog;
            this.assetLoaderFactory = assetLoaderFactory;
            screenStack = new Stack<IStateBase>();
        }


        public void PopState() {
            PopStateLocally();
        }

        public void PresentState(IStateBase state) {

            while (screenStack.Count != 0) {
                PopStateLocally();
            }

            PushStateInternal(state);
        }

        public void PushState(IStateBase state) {

            if (screenStack.Count != 0) {
                var previousState = screenStack.Peek();
                previousState.OnSendToBack();
                previousState.DisableRaycasts();
            }

            PushStateInternal(state);
        }

        public void OnUpdate() {
            if(screenStack.Count == 0) {
                throw new NotSupportedException("Trying to call OnUpdate on the screenstack but it's empty!");
            }

            if (isLoading) {
                return;
            }

            var currentState = screenStack.Peek();
            currentState.OnUpdate();
        }

        private void PushStateInternal(IStateBase state) {

            isLoading = true;

            screenStack.Push(state);

            var stateEntry = statesCatalog.GetCatalogEntry(state.GetStateId());

            InstantiateViews(stateEntry, state);
        }

        private async void InstantiateViews(StateCatalogEntry stateEntry, IStateBase state) {

            var stateAssetLoader = assetLoaderFactory.CreateLoader(stateEntry.Id);

            foreach(var stateAsset in stateEntry.GetAllStateReferences()) {
                stateAssetLoader.AddReference(stateAsset);
            }

            await stateAssetLoader.LoadAsync();

            var uiViewAsset = stateAssetLoader.GetAsset<UiView>(stateEntry.UiView);
            var worldViewAsset = stateAssetLoader.GetAsset<WorldView>(stateEntry.WorldView);

            var stateAssetsList = new List<ScriptableObject>();

            foreach(var stateAsset in stateEntry.StateAssetReferences) {
                stateAssetsList.Add(stateAssetLoader.GetAsset<ScriptableObject>(stateAsset));
            }

            state.CacheStateAssets(stateAssetsList);

            var worldView = Object.Instantiate(worldViewAsset);
            var uiView = Object.Instantiate(uiViewAsset);

            state.LinkViews(uiView, worldView);

            state.OnCreate();

            isLoading = false;
        }

        private void PopStateLocally() {
            var state = screenStack.Peek();
            state.ReleaseAssets(state.GetStateId());
            state.OnDestroy();
            state.DestroyViews();
            screenStack.Pop();

            if(screenStack.Count > 0) {
                var nextState = screenStack.Peek();
                nextState.OnBringToFront();
                nextState.EnableRaycasts();
            }
        }
    }
}