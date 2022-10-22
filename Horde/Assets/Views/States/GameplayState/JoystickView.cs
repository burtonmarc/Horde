using UnityEngine;

namespace Views.States.GameplayState
{
    public class JoystickView : GameplayViewBase
    {
        public Transform JoystickBase;

        public Transform JoystickMovable;

        public void SetJoystickBasePosition(Vector3 basePosition)
        {
            JoystickBase.position = basePosition;
        }

        public void SetJoystickMovablePosition(Vector3 movablePosition)
        {
            JoystickMovable.position = movablePosition;
        }
    }
}