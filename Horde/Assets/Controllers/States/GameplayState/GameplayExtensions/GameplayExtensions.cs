using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState.GameplayExtensions
{
    public static class GameplayExtensions
    {
        public static Transform GetGameplayLayer(this Context context, GameplayLayer gameplayLayer)
        {
            var gameplayState = context.ScreenMachine.CurrentState as GameplayStateController;
            var layer = gameplayState?.GetGameplayLayer(gameplayLayer);
            return layer;
        }
    }
}