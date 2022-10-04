using UnityEngine;
using UnityEngine.UI;

namespace ScreenMachine
{
    public abstract class WorldView : MonoBehaviour
    {
        //[SerializeField] private GraphicRaycaster graphicRaycaster;

        public virtual void Init() { }

        public virtual void OnUpdate() { }

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