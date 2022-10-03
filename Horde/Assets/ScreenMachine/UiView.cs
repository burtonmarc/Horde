using UnityEngine;
using UnityEngine.UI;

namespace ScreenMachine
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class UiView : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster graphicRaycaster;

        public virtual void Init() { }

        public virtual void OnUpdate() { }

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