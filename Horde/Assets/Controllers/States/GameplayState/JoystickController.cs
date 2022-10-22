using Catalogs.Scripts.Configs;
using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class JoystickController : GameplayControllerBase
    {
        public JoystickView JoystickView;

        private bool usingJoystick;

        private Vector3 joystickBasePosition;

        private Vector3 JoystickBasePosition
        {
            get => joystickBasePosition;
            set
            {
                JoystickView.SetJoystickBasePosition(value);
                joystickBasePosition = value;
            }
        }

        private Vector3 joystickMovablePosition;

        private Vector3 JoystickMovablePosition
        {
            get => joystickMovablePosition;
            set
            {
                JoystickView.SetJoystickMovablePosition(value);
                joystickMovablePosition = value;
            }
        }

        private Vector3 joystickDirection;
        private Vector3 JoystickDirection
        {
            get => joystickDirection;
            set
            {
                PlayerController.MovementDirection = value.normalized;
                joystickDirection = value;
            }
        }

        private JoystickConfig joystickConfig;

        private Vector3 defaultJoystickPosition;

        public JoystickController(Context context, PlayerController playerController) : base(context, playerController)
        {

        }

        public override void Init(GameplayViewBase gameplayView, IModel model, object args = null)
        {
            base.Init(gameplayView, model, args);

            JoystickView = gameplayView as JoystickView;
            joystickConfig = args as JoystickConfig;

            SetDefaultJoystickPosition();
        }

        private void SetDefaultJoystickPosition()
        {
            var xPosition = Screen.width * joystickConfig.xPosition;
            var yPosition = Screen.height * joystickConfig.yPosition;
            defaultJoystickPosition = new Vector3(xPosition, yPosition, 0);
        }

        public override void OnUpdate()
        {
            if (TouchBeganThisFrame(0) && GetFingerPosition().y < Screen.height * 0.8)
            {
                usingJoystick = true;
                SetInitialPosition();
            }

            if (IsFingerDown(0) && usingJoystick)
            {
                GetJoystickDirection();
            }

            if (TouchEndedThisFrame(0))
            {
                usingJoystick = false;
                ResetJoystick();
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
            JoystickBasePosition = GetFingerPosition();
            JoystickMovablePosition = JoystickBasePosition;
        }

        private bool IsFingerDown(int touchIndex)
        {
#if UNITY_EDITOR
            return Input.GetMouseButton(touchIndex);
#else
            return Input.touchCount >= touchIndex;
#endif
        }

        private bool TouchEndedThisFrame(int touchIndex)
        {
#if UNITY_EDITOR
            return Input.GetMouseButtonUp(touchIndex);
#else
            return Input.touchCount >= touchIndex;
#endif
        }

        private void GetJoystickDirection()
        {
            var fingerPosition = GetFingerPosition();

            JoystickMovablePosition = fingerPosition;

            JoystickDirection = fingerPosition - JoystickBasePosition;
        }
        
        private void ResetJoystick()
        {
            JoystickBasePosition = defaultJoystickPosition;
            JoystickMovablePosition = JoystickBasePosition;
            JoystickDirection = Vector3.zero;
        }

        private Vector3 GetFingerPosition()
        {
#if UNITY_EDITOR
            return Input.mousePosition;
#else
            return Input.touches[0].position;
#endif
        }
    }
}