using System;
using Data;
using Data.Models;
using Views.States.GameplayState;
using Object = UnityEngine.Object;

namespace Controllers.States.GameplayState
{
    public static class ControllerViewFactory
    {
        public static Context Context;

        public static PlayerController PlayerController;

        public static PoolController PoolController;

        public static T CreateControllerView<T>(GameplayViewBase gameplayViewBase, IModel model, object args = null) 
            where T : GameplayControllerBase
        {
            var controllerViewPair = PoolController.TryGetPooledControllerViewPair<T>();
            if (controllerViewPair != null)
            {
                var controller = controllerViewPair.GameplayController;
                var view = controllerViewPair.GameplayView;
                view.gameObject.SetActive(true);
                controller.Init(view, model, args);
                view.Init();
                return controller as T;
            }
            else
            {
                object[] constructorArgs = {Context, PlayerController};
                // TODO: Check performance of activator with args
                var controller = Activator.CreateInstance(typeof(T), constructorArgs) as T;
                var viewInstance = Object.Instantiate(gameplayViewBase);
                controller?.Init(viewInstance, model, args);
                viewInstance.Init();
                return controller;
            }
        }
    }
}