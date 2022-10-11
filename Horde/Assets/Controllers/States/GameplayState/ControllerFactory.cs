using System;
using System.Collections.Generic;
using Catalogs.Scripts;
using Controllers.States.GameplayState.PlayerWeapons;
using Data;
using Views.States.GameplayState;
using Object = UnityEngine.Object;

namespace Controllers.States.GameplayState
{
    public static class ControllerFactory
    {
        public static Action<GameplayControllerBase> OnControllerCreated;

        public static PoolController PoolController;

        private static readonly Dictionary<Type, Type> controllerViewPairs = new Dictionary<Type, Type>
        {
            {typeof(PlayerController), typeof(PlayerView)},
            {typeof(EnemyController), typeof(EnemyView)},
            {typeof(ShurikenController), typeof(ShurikenView)},
            {typeof(ShurikenBulletController), typeof(ShurikenBulletView)},
        };
        
        public static T CreateController<T>(Context context, PlayerController playerController, object args = null) where T : GameplayControllerBase
        {
            if (controllerViewPairs.TryGetValue(typeof(T), out var controllersViewType))
            {
                var controllerViewPair = PoolController.TryGetPooledControllerViewPair<T>();
                if (controllerViewPair != null)
                {
                    var controller = controllerViewPair.GameplayController;
                    var view = controllerViewPair.GameplayView;
                    view.gameObject.SetActive(true);
                    controller.Init(view, args);
                    view.Init();
                    OnControllerCreated?.Invoke(controller);
                    return controller as T;
                }
                else
                {
                    object[] constructorArgs = {context, playerController};
                    // TODO: Check performance of activator with args
                    var controller = Activator.CreateInstance(typeof(T), constructorArgs) as T;
                    var currentState = context.ScreenMachine.CurrentState;
                    var stateSpawnables = currentState.GetStateAsset<StateSpawnables>();
                    var view = stateSpawnables.spawnables.Find(spawnable => controllersViewType == spawnable.GetType());
                    if (view == null)
                    {
                        throw new Exception($"The type {typeof(T)} does not have a View in the spawnables data");
                    }
                    var viewInstance = Object.Instantiate(view);
                    controller?.Init(viewInstance, args);
                    viewInstance.Init();
                    OnControllerCreated?.Invoke(controller);
                    return controller;
                }
            }

            throw new Exception($"The type {typeof(T)} is not declared in the ControllerFactory");
        }
    }
}