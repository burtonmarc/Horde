using System;
using System.Collections.Generic;
using Catalogs.Scripts;
using Catalogs.Scripts.Entries;
using ScreenMachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class ScreenMachine : IScreenMachine
    {
        public IStateBase CurrentState => screenStack.Peek();

        private Stack<IStateBase> screenStack;

        private StatesCatalog statesCatalog;

        private readonly AssetLoaderFactory assetLoaderFactory = new AssetLoaderFactory();

        private bool isLoading;
        
        private readonly Queue<IStateBase> statesToCleanUp = new Queue<IStateBase>();

        public ScreenMachine(StatesCatalog statesCatalog, AssetLoaderFactory assetLoaderFactory)
        {
            this.statesCatalog = statesCatalog;
            this.assetLoaderFactory = assetLoaderFactory;
            screenStack = new Stack<IStateBase>();
        }


        public void PopState()
        {
            PopStateInternal();
            BringToFrontCurrentState();
        }

        public void PresentState(IStateBase state)
        {
            while (screenStack.Count != 0)
            {
                PopStateInternal();
            }

            PushStateInternal(state);
        }

        public void PushState(IStateBase state)
        {

            if (screenStack.Count != 0)
            {
                var previousState = screenStack.Peek();
                previousState.OnSendToBack();
                previousState.DisableRaycasts();
            }

            PushStateInternal(state);
        }

        public void OnUpdate()
        {
            if (screenStack.Count == 0)
            {
                throw new NotSupportedException("Trying to call OnUpdate on the screenstack but it's empty!");
            }

            if (isLoading)
            {
                return;
            }

            var currentState = screenStack.Peek();
            currentState.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            if (screenStack.Count == 0)
            {
                throw new NotSupportedException("Trying to call OnFixedUpdate on the screenstack but it's empty!");
            }

            if (isLoading)
            {
                return;
            }

            var currentState = screenStack.Peek();
            currentState.OnFixedUpdate();
        }

        public void OnLateUpdate()
        {
            if (screenStack.Count == 0)
            {
                throw new NotSupportedException("Trying to call OnLateUpdate on the screenstack but it's empty!");
            }

            if (isLoading)
            {
                return;
            }

            var currentState = screenStack.Peek();
            currentState.OnLateUpdate();
        }

        private void PushStateInternal(IStateBase state)
        {

            isLoading = true;

            screenStack.Push(state);

            var stateEntry = statesCatalog.GetCatalogEntry(state.GetStateId());

            InstantiateViews(stateEntry, state);
        }

        private async void InstantiateViews(StateCatalogEntry stateEntry, IStateBase state)
        {

            var stateAssetLoader = assetLoaderFactory.CreateLoader(stateEntry.Id);

            foreach (var stateAsset in stateEntry.GetAllStateReferences())
            {
                stateAssetLoader.AddReference(stateAsset);
            }

            await stateAssetLoader.LoadAsync();

            var uiViewAsset = stateAssetLoader.GetAsset<UiView>(stateEntry.UiView);
            var worldViewAsset = stateAssetLoader.GetAsset<WorldView>(stateEntry.WorldView);

            var stateAssetsList = new List<ScriptableObject>();

            foreach (var stateAsset in stateEntry.StateAssetReferences)
            {
                stateAssetsList.Add(stateAssetLoader.GetAsset<ScriptableObject>(stateAsset));
            }

            state.CacheStateAssets(stateAssetsList);

            if (state is IPreloadable preloadable)
            {
                await preloadable.Preload();
            }

            var worldView = Object.Instantiate(worldViewAsset);
            var uiView = Object.Instantiate(uiViewAsset);

            state.LinkViews(uiView, worldView);

            state.OnCreate();
            
            CleanStatesViews();
            
            isLoading = false;
        }

        private void PopStateInternal()
        {
            var state = screenStack.Peek();
            state.ReleaseAssets(state.GetStateId());
            state.OnDestroy();
            statesToCleanUp.Enqueue(state);
            screenStack.Pop();
        }

        private void BringToFrontCurrentState()
        {
            if (screenStack.Count <= 0) return;

            var nextState = screenStack.Peek();
            nextState.OnBringToFront();
            nextState.EnableRaycasts();
        }
        
        private void CleanStatesViews() {
            while(statesToCleanUp.Count > 0) {
                var state = statesToCleanUp.Dequeue();
                state.DestroyViews();
            }
        }
    }
}