using System.Collections.Generic;
using Data;
using Data.Models;
using UnityEngine;
using Views.States.GameplayState;

namespace Controllers.States.GameplayState.PlayerWeapons
{
    public class ShurikenController : GameplayControllerBase
    {
        public ShurikenView ShurikenView;

        private ShurikenModel shurikenModel;

        private float currentDelay;

        private float shootDelay;

        public ShurikenController(Context context, PlayerController playerController) : base(context, playerController)
        {
            
        }

        public override void Init(GameplayViewBase gameplayView, IModel model, object args)
        {
            base.Init(gameplayView, model, args);
            
            ShurikenView = gameplayView as ShurikenView;

            if (model is ShurikenModel sM)
            {
                shurikenModel = sM;
                shootDelay = shurikenModel.HitSpeed;
                currentDelay = shurikenModel.HitSpeed;
            }
        }

        public override void OnUpdate()
        {
            ShurikenView.OnUpdate();

            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0)
            {
                ShootShuriken();
                currentDelay = shootDelay;
            }

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void ShootShuriken()
        {
            var shurikenEntry = Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry("Shuriken");
            
            var shurikenView = Context.Preloader.GetAsset<ShurikenBulletView>(shurikenEntry.ProjectileGameplayView);

            var movementDirection = GetShootingDirection();
            
            var shurikenBulletArgs = new ShurikenBulletArgs {movementDirection = movementDirection, speed = 2.5f};
            
            var shurikenBulletController = ControllerViewFactory.CreateControllerView<ShurikenBulletController>(shurikenView, null, shurikenBulletArgs);
            
            var entityArgs = new EntityArgs {EntityType = EntityType.Projectile, Entity = shurikenBulletController};

            OnGameplayEvent(GameplayEvent.AddEntity, entityArgs);
            
            var effectsLayer = GameplayUtils.GetGameplayLayer(Context, GameplayLayer.Effects);
            shurikenBulletController.ShurikenBulletView.Activate(effectsLayer, PlayerController.PlayerPosition);
        }

        private Vector3 GetShootingDirection()
        {
            if (PlayerController.HasClosestEnemy())
            {
                return PlayerController.closestEnemyDirection;
            }

            return PlayerController.ViewDirection;
        }
    }
}
