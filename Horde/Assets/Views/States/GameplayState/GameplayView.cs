using UnityEngine;

namespace Views.States.GameplayState
{
    public abstract class GameplayView : MonoBehaviour
    {
        protected Transform Transform;
        
        public virtual void Init()
        {
            Transform = transform;
        }

        public virtual void Activate(Transform parent, Vector3 spawnPosition)
        {
            Transform.SetParent(parent);
            Transform.position = spawnPosition;
        }
    }
}