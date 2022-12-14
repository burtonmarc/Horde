using System;
using System.Collections.Generic;
using Data;
using ScreenMachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Controllers.States
{
    public abstract class BaseStateController<TUiView, TWorldView> 
        where TUiView : UiView 
        where TWorldView : WorldView {

        protected abstract string StateId { get; }

        protected TUiView UiView;

        protected TWorldView WorldView;

        protected Context Context;

        private List<ScriptableObject> StateAssets = new List<ScriptableObject>();

        private IScreenMachine ScreenMachine => Context.ScreenMachine;

        protected AssetLoader Preloader;

        protected BaseStateController(Context context) {
            Context = context;
            Context.Preloader = Preloader;
        }

        public string GetStateId()
        {
            return StateId;
        }

        protected void PopState() {
            ScreenMachine.PopState();
        }

        protected void PushState(IStateBase state) {
            ScreenMachine.PushState(state);
        }

        protected void PresentState(IStateBase state) {
            ScreenMachine.PresentState(state);
        }

        public virtual void OnUpdate() {
            WorldView.OnUpdate();
            UiView.OnUpdate();
        }
        
        public virtual void OnFixedUpdate() {
            WorldView.OnFixedUpdate();
            UiView.OnFixedUpdate();
        }

        public virtual void OnLateUpdate() { }

        public void DisableRaycasts() {
            UiView.DisableRaycast();
            //WorldView.DisableRaycast();
        }

        public void EnableRaycasts() {
            UiView.EnableRaycast();
            //WorldView.EnableRaycast();
        }

        public virtual void OnDestroy()
        {
            
        }

        public void CacheStateAssets(List<ScriptableObject> stateAssets) {
            StateAssets = stateAssets;
        }
        
        public T GetStateAsset<T>() where T : ScriptableObject {
            foreach(var stateAsset in StateAssets) {
                if(stateAsset is T asset) {
                    return asset;
                }
            }

            throw new NotSupportedException("Couldn't find any state asset of type " + typeof(T).FullName);
        }

        public void LinkViews(UiView uiView, WorldView worldView) {
            UiView = uiView as TUiView;
            WorldView = worldView as TWorldView;
        }

        public void DestroyViews() {
            Object.Destroy(UiView.gameObject);
            Object.Destroy(WorldView.gameObject);
        }

        public void ReleaseAssets(string stateId) {
            Context.AssetLoaderFactory.ReleaseStateLoadedAssets(stateId);
        }

    }
}