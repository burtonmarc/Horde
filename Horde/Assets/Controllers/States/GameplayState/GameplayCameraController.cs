using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class GameplayCameraController : GameplayControllerBase
    {
        public GameplayCameraView GameplayCameraView;

        private Vector3 cachedPosition;
        
        public GameplayCameraController(Context context, PlayerController playerController) : base(context, playerController)
        {
        }

        public override void Init(GameplayViewBase gameplayView, IModel model, object args)
        {
            base.Init(gameplayView, model, args);
            
            GameplayCameraView = gameplayView as GameplayCameraView;
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnLateUpdate()
        {
            cachedPosition.x = PlayerController.PlayerPosition.x;
            cachedPosition.y = PlayerController.PlayerPosition.y;
            GameplayCameraView.MoveCamera(cachedPosition);
        }
    }
}
