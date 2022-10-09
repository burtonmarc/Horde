using UnityEngine;

namespace Views.States.GameplayState
{
    public abstract class GameplayView : MonoBehaviour
    {
        public virtual void Init() { }

        public virtual void Activate(Transform parent, Vector3 position) { }
    }
}