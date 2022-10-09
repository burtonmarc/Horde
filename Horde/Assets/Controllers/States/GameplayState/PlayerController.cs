using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class PlayerController : GameplayControllerBase
    {
        public Vector3 PlayerPosition => PlayerView.transform.position;
        
        public PlayerView PlayerView;

        public PlayerController(Context context, PlayerController playerController) : base(context, playerController)
        {
            
        }

        public override void Init(GameplayView gameplayView)
        {
            PlayerView = gameplayView as PlayerView;
        }

        public override void OnUpdate()
        {
            // TODO: Move to input class
            GetInput();
            
            PlayerView.OnUpdate();
        }

        public override void OnDestroy()
        {
            Object.Destroy(PlayerView.gameObject);
        }

        private void GetInput()
        {
            var direction = Vector2.zero;

            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
            }

            PlayerView.Move(direction.normalized);
        }
        
    }
}