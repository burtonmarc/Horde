using Data;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class EnemyWavesController : GameplayControllerBase
    {
        // TODO: Add scriptable with wave info
        
        public EnemyWavesController(Context context, PlayerController playerController) : base(context, playerController)
        {
            //var enemyView = (EnemyView) ControllerFactory.CreateController<EnemyController>();
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnDestroy()
        {
            
        }
    }
}