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
        
        public void Init(IGameplayView gameplayView)
        {
            EnemyView = gameplayView as EnemyView;
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