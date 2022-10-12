using UnityEngine;
using UnityEngine.UI;

namespace ScreenMachine
{
    public abstract class WorldView : MonoBehaviour
    {
        //[SerializeField] private GraphicRaycaster graphicRaycaster;

        // TODO: Rename Init to ResetWorldView?
        public virtual void Init() { }

        public virtual void OnUpdate() { }
        
        public virtual void OnFixedUpdate() { }

        //public void EnableRaycast()
        //{
        //    graphicRaycaster.enabled = true;
        //}

        //public void DisableRaycast()
        //{
        //    graphicRaycaster.enabled = false;
        //}
    }
}