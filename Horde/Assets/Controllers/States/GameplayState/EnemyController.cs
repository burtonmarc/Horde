using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class EnemyController : GameplayControllerBase
    {
        public EnemyView EnemyView;

        public EnemyController(Context context, PlayerController playerController) : base(context, playerController)
        {

        }

        public override void Init(GameplayView gameplayView)
        {
            EnemyView = gameplayView as EnemyView;
        }
        
        public void Activate()
        {
            
        }

        public override void OnUpdate()
        {
            EnemyView.OnUpdate(PlayerController.PlayerPosition);
        }

        public override void OnDestroy()
        {
            var pool = Context.ControllersPool as PoolController;
            var controllerViewPair = new ControllerViewPair(this, EnemyView);
            pool.StoreControllerViewPair<EnemyController>(controllerViewPair);
        }
    }
}