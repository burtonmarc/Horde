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

        // State Shared
        
        void LinkViews(UiView uiView, WorldView worldView);
        void OnBringToFront();
        void OnUpdate();
        void OnDestroy();
        void DestroyViews();
        void EnableRaycasts();
        void DisableRaycasts();

        void CacheStateAssets(List<ScriptableObject> stateAssets);
        void ReleaseAssets(string stateId);

        T GetStateAsset<T>() where T : ScriptableObject;
    }
}