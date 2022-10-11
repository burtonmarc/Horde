using UnityEngine;

namespace Views.States.GameplayState
{
    public class ShurikenBulletView : GameplayViewBase
    {
        public override void Init()
        {
            base.Init();
        }

        public override void Activate(Transform parent, Vector3 spawnPosition)
        {
            base.Activate(parent, spawnPosition);
        }
        
        public void OnUpdate(Vector3 movementDirection)
        {
            transform.Translate(movementDirection * Time.deltaTime);
        }
    }
}