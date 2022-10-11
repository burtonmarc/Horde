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