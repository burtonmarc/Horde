using System.Collections.Generic;
using UnityEngine;

namespace ScreenMachine
{
    public interface IStateBase
    {
        // State Specific
        string GetStateId();
        void OnCreate();
        void OnSendToBack();

        void DisableRaycast();
        
        // State Shared
        
        void LinkViews(UiView uiView, WorldView worldView);
        void OnBringToFront();
        void EnableRaycast();
        void OnUpdate();
        void OnDestroy();
        void DestroyViews();

        void CacheStateAssets(List<ScriptableObject> stateAssets);

        T GetStateAsset<T>() where T : ScriptableObject;
    }
}