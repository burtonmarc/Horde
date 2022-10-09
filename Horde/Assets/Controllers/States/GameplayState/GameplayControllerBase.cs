using Controllers.States.GameplayState;
using Data;
using UnityEngine;

namespace Views.States.GameplayState
{
    public abstract class GameplayControllerBase
    {
        protected Context Context;

        public PlayerController PlayerController;

        protected GameplayControllerBase(Context context, PlayerController playerController)
        {
            Context = context;
            PlayerController = playerController;
        }

        // This constructor is only used for controllers that don't have a view, like PoolController
        protected GameplayControllerBase(Context context)
        {
            Context = context;
        }

        public virtual void Init(GameplayView gameplayView) { }
        
        public abstract void OnUpdate();

        public virtual void OnFixedUpdate() { }

        public abstract void OnDestroy();
    }
}