using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public abstract class GameplayControllerBase
    {
        protected readonly Context Context;

        // Most of the controllers will need to do something on the player or calculate
        // something in relation to the player, so I add it in the base directly
        public PlayerController PlayerController;

        // Used as reference to pool this controller and its view in the PoolThisControllerView method
        private GameplayViewBase viewBase;

        // This is a reference to the list that updates each controller
        // Used to auto-eliminate yourself from it on the OnDestroy method
        public List<GameplayControllerBase> UpdateListReference;
        
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
        public virtual void Init(GameplayViewBase gameplayView, IModel model, object args)
        {
            viewBase = gameplayView;
        }

        // Used for every time the controller is pooled, so no need to add the view and the config
        public virtual void Pool(object args)
        {
            
        }
        
        public abstract void OnUpdate();

        public virtual void OnFixedUpdate() { }

        public virtual void OnDestroy()
        {
            if (viewBase != null)
            {
                PoolThisControllerView();
            }
            UpdateListReference.Remove(this);
        }

        private void PoolThisControllerView()
        {
            var pool = Context.ControllersPool as PoolController;
            var controllerViewPair = new ControllerViewPair(this, viewBase);
            pool?.StoreControllerViewPair<EnemyController>(controllerViewPair);
        }
    }
}