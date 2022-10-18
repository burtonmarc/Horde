using UnityEngine;

namespace Views.States.GameplayState
{
    public class EnemyView : GameplayViewBase
    {
        public override void Init()
        {
            base.Init();
        }

        public override void Activate(Transform parent, Vector3 spawnPosition)
        {
            base.Activate(parent, spawnPosition);
        }
        
        public void OnUpdate(Vector3 playerPosition, float movementSpeed)
        {
            
        }
        
        public void OnFixedUpdate(Vector3 playerPosition, float movementSpeed)
        {
            var direction = playerPosition - transform.position;
            direction.Normalize();
            transform.Translate(movementSpeed * Time.fixedDeltaTime * direction);
        }
    }
}