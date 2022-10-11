using UnityEngine;

namespace Views.States.GameplayState
{
    public class PlayerView : GameplayViewBase
    {
        public Transform WeaponAnchor;

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

        public void Move(Vector2 direction)
        {
            transform.Translate(direction * Time.deltaTime);
        }
    }
}