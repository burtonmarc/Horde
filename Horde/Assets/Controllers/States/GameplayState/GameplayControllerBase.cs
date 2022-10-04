using Controllers.States.GameplayState;
using Data;
using UnityEngine;

namespace Views.States.GameplayState
{
    public abstract class GameplayControllerBase
    {
        public Context Context;

        public PlayerController PlayerController;
        
        public GameplayControllerBase(Context context, PlayerController playerController)
        {
            Context = context;
            PlayerController = playerController;
        }
        public abstract void OnUpdate();
        
        public abstract void OnDestroy();
    }
}