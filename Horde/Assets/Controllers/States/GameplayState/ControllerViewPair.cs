using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class ControllerViewPair
    {
        public GameplayControllerBase GameplayControllerBase;
        public GameplayView GameplayView;
        
        public ControllerViewPair(GameplayControllerBase gameplayControllerBase, GameplayView gameplayView)
        {
            GameplayControllerBase = gameplayControllerBase;
            GameplayView = gameplayView;
        }
    }
}