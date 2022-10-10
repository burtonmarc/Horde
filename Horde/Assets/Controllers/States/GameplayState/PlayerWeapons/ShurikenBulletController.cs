using Data;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState.PlayerWeapons
{
    public class ShurikenBulletController : GameplayControllerBase
    {
        public ShurikenBulletView ShurikenBulletView;
        
        public ShurikenBulletController(Context context, PlayerController playerController) : base(context, playerController)
        {
            
        }

        public override void Init(GameplayView gameplayView)
        {
            ShurikenBulletView = gameplayView as ShurikenBulletView;
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnDestroy()
        {
            
        }
    }
}