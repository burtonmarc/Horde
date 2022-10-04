using UnityEngine;

namespace Views.States.GameplayState
{
    public class PlayerView : MonoBehaviour, IGameplayView
    {
        public void OnUpdate()
        {
            
        }

        public void Move(Vector2 direction)
        {
            transform.Translate(direction * Time.deltaTime);
        }
    }
}