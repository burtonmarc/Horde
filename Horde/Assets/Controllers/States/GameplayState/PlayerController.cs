using Catalogs.Scripts;
using Controllers.States.GameplayState.PlayerWeapons;
using Data;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState
{
    public class PlayerController : GameplayControllerBase
    {
        public Vector3 PlayerPosition => PlayerView.transform.position;
        
        public PlayerView PlayerView;

        public Vector3 ViewDirection = Vector3.up;

        public EnemyController closestEnemy;

        public Vector3 closestEnemyDirection;

        private PlayerModel playerModel;

        public PlayerController(Context context, PlayerController playerController) : base(context, playerController)
        {
            
        }

        public override void Init(GameplayViewBase gameplayView, IModel model, object args)
        {
            base.Init(gameplayView, model, args);
            PlayerView = gameplayView as PlayerView;
            playerModel = model as PlayerModel;
        }
        
        public void AddPlayerWeapon()
        {
            var shurikenEntry = Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry("Shuriken");
            var shurikenView = Context.Preloader.GetAsset<ShurikenView>(shurikenEntry.WeaponGameplayView);
            var shurikenModel = new ShurikenModel(Context.Preloader.GetAsset<WeaponConfig>(shurikenEntry.WeaponConfig));
            var shurikenController = ControllerFactory.CreateController<ShurikenController>(shurikenView, shurikenModel);
            var entityArgs = new EntityArgs {EntityType = EntityType.Weapon, Entity = shurikenController};
            OnGameplayEvent(GameplayEvent.AddEntity, entityArgs);
            shurikenController.ShurikenView.Activate(PlayerView.WeaponAnchor, Vector3.zero);
        }

        public override void OnUpdate()
        {
            // TODO: Move to input class
            MovePlayer();
            
            PlayerView.OnUpdate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
        
        public void SetClosestEnemyIfPossible(EnemyController enemyController, Vector3 enemyDirection)
        {
            if (closestEnemy == enemyController)
            {
                closestEnemyDirection = enemyDirection;
            }
            
            if (closestEnemy != null &&
                closestEnemy.isAlive &&
                closestEnemy.QuadraticDistanceToPlayer < enemyController.QuadraticDistanceToPlayer)
            {
                return;
            }

            closestEnemy = enemyController;
            closestEnemyDirection = enemyDirection;
        }

        public bool HasClosestEnemy()
        {
            return closestEnemy != null && closestEnemy.isAlive;
        }

        private void MovePlayer()
        {
            var movementDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.A))
            {
                movementDirection += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movementDirection += Vector2.right;
            }
            if (Input.GetKey(KeyCode.W))
            {
                movementDirection += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementDirection += Vector2.down;
            }

            movementDirection.Normalize();
            
            if (movementDirection != Vector2.zero)
            {
                ViewDirection = movementDirection;
            }

            PlayerView.Move(movementDirection * playerModel.MovementSpeed);
        }
    }
}