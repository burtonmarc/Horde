using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public static class GameplayExtensions
    {
        public static readonly float aspectRatio = (float) Screen.height / Screen.width;
        
        public static Transform GetGameplayLayer(this Context context, GameplayLayer gameplayLayer)
        {
            var gameplayState = context.ScreenMachine.CurrentState as GameplayStateController;
            var layer = gameplayState?.GetGameplayLayer(gameplayLayer);
            return layer;
        }

        public static bool IsOutsideOfBounds(this Vector3 position)
        {
            var height = Camera.main.orthographicSize;
            var width = height / aspectRatio;
            
            var leftOutside = position.x < -width;
            var rightOutside = position.x > width;
            var topOutside = position.y > height;
            var bottomOutside = position.y < -height;

            return leftOutside || rightOutside || topOutside || bottomOutside;
        }
    }
}