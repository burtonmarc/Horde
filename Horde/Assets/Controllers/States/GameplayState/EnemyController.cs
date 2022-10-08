using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class EnemyController : GameplayControllerBase
    {
        private EnemyView EnemyView;

        public EnemyController(Context context, PlayerController playerController) : base(context, playerController)
        {
            
        }
        
        public override void Init(IGameplayView gameplayView)
        {
            EnemyView = gameplayView as EnemyView;
            if (EnemyView != null)
            {
                EnemyView.Init();
            }
        }
        
        public override void OnUpdate()
        {
            EnemyView.OnUpdate();
        }

        public override void OnDestroy()
        {
            Object.Destroy(EnemyView.gameObject);
        }
    }
}