using System;
using System.Collections.Generic;
using ControllersPool;
using Data;

namespace Controllers.States.GameplayState
{
    public class PoolController : GameplayControllerBase, IControllersPool
    {
        private readonly Dictionary<Type, Stack<ControllerViewPair>> controllersPool;
        
        public PoolController(Context context) : base(context)
        {
            controllersPool = new Dictionary<Type, Stack<ControllerViewPair>>();
        }

        public ControllerViewPair TryGetPooledControllerViewPair<T>()
        {
            if (!controllersPool.TryGetValue(typeof(T), out var controllerViewPairStackOfTypeT)) return null;
            
            if (controllerViewPairStackOfTypeT.Count <= 0) return null;
            
            return controllerViewPairStackOfTypeT.Pop();
        }

        public void StoreControllerViewPair<T>(ControllerViewPair controllerViewPair)
        {
            controllerViewPair.GameplayView.gameObject.SetActive(false);
            
            if (!controllersPool.TryGetValue(typeof(T), out var controllerViewPairStackOfTypeT))
            {
                var stack = new Stack<ControllerViewPair>();
                stack.Push(controllerViewPair);
                controllersPool.Add(typeof(T), stack);
            }
            else
            {
                controllerViewPairStackOfTypeT.Push(controllerViewPair);
            }
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnDestroy()
        {
            
        }
    }
}