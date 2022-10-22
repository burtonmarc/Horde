using UnityEngine;
using Views.States.GameplayState;

namespace Catalogs.Scripts.Configs
{
    [CreateAssetMenu(fileName = "JoystickConfig", menuName = "ScriptableObjects/Configs/Create Joystick Config", order = 1)]
    public class JoystickConfig : ScriptableObject
    {
        public JoystickView JoystickView;

        public float xPosition;

        public float yPosition;

        public float MaxDistanceFromCenter;
    }
}