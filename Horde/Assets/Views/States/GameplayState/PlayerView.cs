using UnityEngine;

namespace Views.States.GameplayState
{
    public class PlayerView : GameplayView
    {
        public override void Init()
        {
            
        }
        
        public override void Activate(Transform parent, Vector3 spawnPosition)
        {
            transform.SetParent(parent);
            transform.position = spawnPosition;
        }
        
        public void OnUpdate()
        {
            
        }

        public void Move(Vector2 direction)
        {
            transform.Translate(direction * Time.deltaTime);
        }
    }
}