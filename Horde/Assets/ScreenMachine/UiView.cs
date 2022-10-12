using UnityEngine;
using UnityEngine.UI;

namespace ScreenMachine
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class UiView : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster graphicRaycaster;

        /// <summary>
        /// Init() is used to reset variables for pooling and should not be used to add the View's data.
        /// To add data create an Activate() method with parameters and call it from the Controler
        /// </summary>
        public virtual void ResetUiView() { }

        public virtual void OnUpdate() { }
        
        public virtual void OnFixedUpdate() { }

        public void EnableRaycast()
        {
            graphicRaycaster.enabled = true;
        }

        public void DisableRaycast()
        {
            graphicRaycaster.enabled = false;
        }
    }
}