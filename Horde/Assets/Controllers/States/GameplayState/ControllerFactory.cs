using System;
using System.Collections.Generic;
using ControllersPool;
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
        };
        
        public static T CreateController<T>(Context context, PlayerController playerController) where T : GameplayControllerBase
        {
            if (controllerViewPairs.TryGetValue(typeof(T), out var controllersViewType))
            {
                var controllerViewPair = PoolController.TryGetPooledControllerViewPair<T>();
                if (controllerViewPair != null)
                {
                    var controller = controllerViewPair.GameplayControllerBase;
                    var view = controllerViewPair.GameplayView;
                    controller.Init(view);
                    view.Init();
                    OnControllerCreated?.Invoke(controller);
                    return controller as T;
                }
                else
                {
                    object[] args = { context, playerController };
                    // TODO: Check performance of activator with args
                    var controller = Activator.CreateInstance(typeof(T), args) as T;
                    var currentState = context.ScreenMachine.CurrentState;
                    var stateSpawnables = currentState.GetStateAsset<StateSpawnables>();
                    var view = stateSpawnables.spawnables.Find(spawnable => typeof(GameplayView).IsAssignableFrom(controllersViewType));
                    var viewInstance = Object.Instantiate(view);
                    controller?.Init(viewInstance);
                    viewInstance.Init();
                    OnControllerCreated?.Invoke(controller);
                    return controller;
                }
            }

            throw new Exception($"The type {typeof(T)} is not declared in the ControllerFactory");
        }
    }
}