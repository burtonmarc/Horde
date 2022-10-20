using Data;
using UnityEngine;

namespace Controllers.States.GameplayState
{
    public class InputController : GameplayControllerBase
    {
        private Vector3 JoystickCenter;

        private Vector3 JoystickDirection;
        
        public InputController(Context context) : base(context)
        {
            
        }
        
        public override void OnUpdate()
        {
            if (TouchBeganThisFrame(0))
            {
                SetInitialPosition();
            }

            if (IsFingerDown(0))
            {
                GetJoystickDirection();
            }
        }

        private bool TouchBeganThisFrame(int touchIndex)
        {
#if UNITY_EDITOR
            return Input.GetMouseButtonDown(touchIndex);
#else
            return Input.touchCount >= touchIndex && Input.GetTouch(touchIndex).phase == TouchPhase.Began;
#endif
        }

        private void SetInitialPosition()
        {
#if UNITY_EDITOR
            JoystickCenter = Input.mousePosition;
#else
            JoystickCenter = Input.touches[0].position;
#endif
        }

        private bool IsFingerDown(int touchIndex)
        {
#if UNITY_EDITOR
            return Input.GetMouseButton(touchIndex);
#else
            return Input.touchCount >= touchIndex;
#endif
        }

        private void GetJoystickDirection()
        {
            SetJoystickDirectionPc();
            
            // TODO: Add Mobile
            
        }

        private void SetJoystickDirectionPc()
        {
            var fingerPosition = Input.mousePosition;

            JoystickDirection = fingerPosition - JoystickCenter;

            PlayerController.MovementDirection = JoystickDirection.normalized;
        }
    }
}