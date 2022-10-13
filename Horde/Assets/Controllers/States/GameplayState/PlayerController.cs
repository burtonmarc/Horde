using Catalogs.Scripts;
using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class PlayerController : GameplayControllerBase
    {
        public Vector3 PlayerPosition => PlayerView.transform.position;
        
        public PlayerView PlayerView;

        public Vector3 ViewDirection = Vector3.up;

        private PlayerModel playerModel;

        public PlayerController(Context context, PlayerController playerController) : base(context, playerController)
        {
            
        }

        public override void Init(GameplayViewBase gameplayView, IModel model, object args)
        {
            base.Init(gameplayView, model, args);
            PlayerView = gameplayView as PlayerView;
            playerModel = model as PlayerModel;
        }

        public override void Pool(object args)
        {
            base.Pool(args);
        }

        public override void OnUpdate()
        {
            // TODO: Move to input class
            GetInput();
            
            PlayerView.OnUpdate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void GetInput()
        {
            var movementDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.A))
            {
                movementDirection += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movementDirection += Vector2.right;
            }
            if (Input.GetKey(KeyCode.W))
            {
                movementDirection += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementDirection += Vector2.down;
            }

            movementDirection.Normalize();
            
            if (movementDirection != Vector2.zero)
            {
                ViewDirection = movementDirection;
            }

            PlayerView.Move(movementDirection * playerModel.MovementSpeed);
        }
        
    }
}