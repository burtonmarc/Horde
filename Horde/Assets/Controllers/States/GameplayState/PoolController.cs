using System;
using System.Collections.Generic;
using ControllersPool;
using Data;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class PoolController : GameplayControllerBase, IControllersPool
    {
        private Dictionary<Type, Stack<ControllerViewPair>> controllersPool;
        
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

        public override void OnUpdate()
        {
            
        }

        public override void OnDestroy()
        {
            
        }
    }
}