using System;
using System.Collections.Generic;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public static class ControllerFactory
    {
        public static Action<GameplayControllerBase> OnControllerCreated;
        
        private static readonly Dictionary<Type, Type> controllerViewPairs = new Dictionary<Type, Type>
        {
            {typeof(PlayerController), typeof(PlayerView)},
            {typeof(EnemyController), typeof(EnemyView)},
        };
        
        public static IGameplayView CreateController<T>(PoolController poolController) where T : GameplayControllerBase, new()
        {
            if (controllerViewPairs.TryGetValue(typeof(T), out var controllersViewType))
            {
                var controllerViewPair = poolController.TryGetPooledControllerViewPair<T>();
                if (controllerViewPair != null)
                {
                    var controller = controllerViewPair.GameplayControllerBase;
                    var view = controllerViewPair.GameplayView;
                    controller.Init(view);
                    OnControllerCreated(controller);
                    return view;
                }
                else
                {
                    var controller = new T();
                    var view = (IGameplayView)Activator.CreateInstance(controllersViewType);
                    controller.Init(view);
                    OnControllerCreated(controller);
                    return view;
                }
            }

            throw new Exception($"The type {typeof(T)} is not declared in the ControllerFactory");
        }
    }
}