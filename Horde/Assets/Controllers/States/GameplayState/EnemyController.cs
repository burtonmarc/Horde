using Data;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class EnemyController : GameplayControllerBase
    {
        public EnemyView EnemyView;

        private EnemyModel enemyModel;
        
        public EnemyController(Context context, PlayerController playerController) : base(context, playerController)
        {
            
        }

        public override void Init(GameplayViewBase gameplayView, IModel model, object args)
        {
            base.Init(gameplayView, model, args);
            EnemyView = gameplayView as EnemyView;
            enemyModel = model as EnemyModel;
        }
        
        public void Activate()
        {
            
        }
        
        public override void OnUpdate()
        {
            
        }
        
        public override void OnFixedUpdate()
        {
            EnemyView.OnFixedUpdate(PlayerController.PlayerPosition);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}