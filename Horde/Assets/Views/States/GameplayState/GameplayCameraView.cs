using UnityEngine;

namespace Views.States.GameplayState
{
    public class GameplayCameraView : GameplayViewBase
    {
        public void MoveCamera(Vector3 position)
        {
            Transform.position = position;
        }
    }
}