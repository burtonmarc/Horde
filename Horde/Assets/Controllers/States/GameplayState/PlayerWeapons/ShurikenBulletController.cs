using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState.PlayerWeapons
{
    public struct ShurikenBulletArgs
    {
        public Vector3 movementDirection;
        public float speed;
    }
    
    public class ShurikenBulletController : GameplayControllerBase
    {
        public ShurikenBulletView ShurikenBulletView;

        private Vector3 movementDirection;

        private float speed;

        public ShurikenBulletController(Context context, PlayerController playerController) : base(context, playerController)
        {
            
        }

        public override void Init(GameplayViewBase gameplayView, IModel model, object args)
        {
            base.Init(gameplayView, model, args);
            
            ShurikenBulletView = gameplayView as ShurikenBulletView;
            
            if (args is ShurikenBulletArgs shurikenBulletArgs)
            {
                movementDirection = shurikenBulletArgs.movementDirection;
                speed = shurikenBulletArgs.speed;
            }
        }

        public override void OnUpdate()
        {
            if (MarkedToDestroy) return;
        }

        public override void OnFixedUpdate()
        {
            if (MarkedToDestroy) return;
            
            ShurikenBulletView.OnUpdate(movementDirection * speed);
            
            if (ShurikenBulletView.transform.position.IsOutsideOfBounds(PlayerController.PlayerPosition))
            {
                OnDestroy();
            }
        }
        
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}