using System.Collections.Generic;
using Controllers.States.GameplayState.GameplayExtensions;
using Data;
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

        private List<GameplayControllerBase> shurikenBulletControllers;
    
        public ShurikenController(Context context, PlayerController playerController) : base(context, playerController)
        {
            shurikenBulletControllers = new List<GameplayControllerBase>(8);
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

            foreach (var shurikenBulletController in shurikenBulletControllers)
            {
                shurikenBulletController.OnUpdate();
            }
        }

        public override void OnDestroy()
        {
            for (var index = shurikenBulletControllers.Count - 1; index >= 0; index--)
            {
                var shurikenBulletController = shurikenBulletControllers[index];
                shurikenBulletController.OnDestroy();
            }

            base.OnDestroy();
        }

        private void ShootShuriken()
        {
            var shurikenView = Context.Preloader.GetAsset<ShurikenView>(Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry("Shuriken").ProjectileGameplayView);
            var shurikenBulletDate = new ShurikenBulletArgs {movementDirection = PlayerController.ViewDirection, speed = 2.5f};
            var shurikenBulletController = ControllerFactory.CreateController<ShurikenBulletController>(shurikenView, null, shurikenBulletDate);
            shurikenBulletControllers.AddController(shurikenBulletController);
            var effectsLayer = Context.GetGameplayLayer(GameplayLayer.Effects);
            shurikenBulletController.ShurikenBulletView.Activate(effectsLayer, PlayerController.PlayerPosition);
        }
    }
}
