using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class GameplayCameraController : GameplayControllerBase
    {
        public GameplayCameraView GameplayCameraView;

        private Vector3 cachedVector;
        
        public GameplayCameraController(Context context, PlayerController playerController) : base(context, playerController)
        {
        }

        public override void Init(GameplayViewBase gameplayView, IModel model, object args)
        {
            base.Init(gameplayView, model, args);
            
            if (gameplayView is GameplayCameraView gameplayCameraView)
            {
                GameplayCameraView = gameplayCameraView;
            }

        }

        public override void OnUpdate()
        {
            
        }

        public override void OnLateUpdate()
        {
            cachedVector.x = PlayerController.PlayerPosition.x;
            cachedVector.y = PlayerController.PlayerPosition.y;
            cachedVector.z = GameplayCameraView.Transform.position.z;
            GameplayCameraView.MoveCamera(cachedVector);
        }
    }
}
