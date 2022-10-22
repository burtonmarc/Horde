using Data;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class EnemyController : GameplayControllerBase
    {
        public EnemyView EnemyView;

        public float QuadraticDistanceToPlayer;

        public bool isAlive;

        private EnemyModel enemyModel;

        public EnemyController(Context context, PlayerController playerController) : base(context, playerController)
        {

        }

        public override void Init(GameplayViewBase gameplayView, IModel model, object args)
        {
            base.Init(gameplayView, model, args);
            EnemyView = gameplayView as EnemyView;
            enemyModel = model as EnemyModel;
        }

        public void Activate()
        {
            isAlive = true;
        }

        public override void OnUpdate()
        {

        }

        public override void OnFixedUpdate()
        {
            var enemyDirection = PlayerController.PlayerPosition - EnemyView.transform.position;
            QuadraticDistanceToPlayer = enemyDirection.sqrMagnitude;
            EnemyView.OnFixedUpdate(PlayerController.PlayerPosition, enemyModel.MovementSpeed);
            PlayerController.SetClosestEnemyIfPossible(this, -enemyDirection.normalized);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            isAlive = false;
        }
    }
}