using System.Collections.Generic;
using UnityEngine;

namespace ScreenMachine
{
    public interface IStateBase
    {
        string GetStateId();
        void OnCreate();
        void OnSendToBack();
        void LinkViews(UiView uiView, WorldView worldView);
        void OnBringToFront();
        void OnUpdate();
        void OnFixedUpdate();
        void OnLateUpdate();
        void OnDestroy();
        void DestroyViews();
        void EnableRaycasts();
        void DisableRaycasts();

        void CacheStateAssets(List<ScriptableObject> stateAssets);
        void ReleaseAssets(string stateId);

        T GetStateAsset<T>() where T : ScriptableObject;
    }
}