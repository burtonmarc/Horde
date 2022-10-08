using System;
using System.Collections.Generic;
using Data;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class ControllerViewPair
    {
        public GameplayControllerBase GameplayControllerBase;
        public IGameplayView GameplayView;
    }
    
    public class PoolController : GameplayControllerBase
    {
        
        
        private Dictionary<Type, Stack<ControllerViewPair>> controllersPool;
        
        public PoolController(Context context) : base(context)
        {
            
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