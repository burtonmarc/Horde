using System;
using Data;
using Data.Models;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public enum GameplayEvent
    {
        AddEntity,
        RemoveEntity,
    }
    
    public abstract class GameplayControllerBase
    {
        protected readonly Context Context;

        // Most of the controllers will need to do something on the player or calculate
        // something in relation to the player, so I add it in the base directly
        public PlayerController PlayerController;

        // Used at the destroy, to know which type of entity it is in the EntitiesContainerController
        public EntityType EntityType;
        
        public static Action<GameplayEvent, object> OnGameplayEvent;

        public bool MarkedToDestroy;

        public Transform Transform => viewBase.Transform;
        
        // Used as reference to pool this controller and its view in the PoolThisControllerView method
        private GameplayViewBase viewBase;
        
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

        // Used for the first time the controller is created
        public virtual void Init(GameplayViewBase gameplayView, IModel model, object args = null)
        {
            viewBase = gameplayView;
            MarkedToDestroy = false;
        }

        public virtual void Init(SaveableBaseModel model, object args = null)
        {
            MarkedToDestroy = false;
        }

        public abstract void OnUpdate();

        public virtual void OnFixedUpdate() { }

        // Only used by the EntitiesContainerController to remove entities at end of frame
        public virtual void OnLateUpdate() { }

        public virtual void OnDestroy()
        {
            MarkedToDestroy = true;
            OnGameplayEvent(GameplayEvent.RemoveEntity, new EntityArgs
            {
                EntityType = EntityType,
                Entity = this
            });
        }

        public void DestroyCompletely()
        {
            if (viewBase != null)
            {
                EntityType = EntityType.GeneralBehaviour;
                PoolThisControllerView();
            }
        }

        private void PoolThisControllerView()
        {
            var controllerViewPair = new ControllerViewPair(this, viewBase);
            var poolController = Context.PoolController as PoolController;
            poolController?.StoreControllerViewPair(GetType(), controllerViewPair);
        }
    }
}