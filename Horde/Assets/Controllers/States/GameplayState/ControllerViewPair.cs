using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class ControllerViewPair
    {
        public GameplayControllerBase GameplayController;
        public GameplayViewBase GameplayView;
        
        public ControllerViewPair(GameplayControllerBase gameplayController, GameplayViewBase gameplayView)
        {
            GameplayController = gameplayController;
            GameplayView = gameplayView;
        }
    }
}