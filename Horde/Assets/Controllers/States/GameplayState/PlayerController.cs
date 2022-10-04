using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class PlayerController : GameplayControllerBase
    {
        private PlayerView PlayerView;
        
        public PlayerController(Context context) : base(context, null)
        {
            
        }

        public void Init(IGameplayView gameplayView)
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