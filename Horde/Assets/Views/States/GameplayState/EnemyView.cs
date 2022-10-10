using UnityEngine;

namespace Views.States.GameplayState
{
    public class EnemyView : GameplayView
    {
        public override void Init()
        {
            // Reset variables here
        }

        public override void Activate(Transform parent, Vector3 spawnPosition)
        {
            transform.SetParent(parent);
            transform.position = spawnPosition;
        }
        
        public void OnUpdate()
        {
            
        }
        
        public void OnFixedUpdate(Vector3 playerPosition)
        {
            var direction = playerPosition - transform.position;
            transform.Translate(direction * Time.fixedDeltaTime);
        }
    }
}