using Data;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class EnemyController : GameplayControllerBase
    {
        public EnemyView EnemyView;
        
        public EnemyController(Context context, PlayerController playerController) : base(context, playerController)
        {
            
        }

        public override void Init(GameplayViewBase gameplayView, object args)
        {
            base.Init(gameplayView, args);
            EnemyView = gameplayView as EnemyView;
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