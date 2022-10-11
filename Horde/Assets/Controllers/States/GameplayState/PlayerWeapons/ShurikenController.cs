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

        private const float ShootDelay = 0.5f;
        private float currentDelay = ShootDelay;

        private List<GameplayControllerBase> shurikenBulletControllers;
    
        public ShurikenController(Context context, PlayerController playerController) : base(context, playerController)
        {
            shurikenBulletControllers = new List<GameplayControllerBase>(8);
        }

        public override void Init(GameplayView gameplayView, object args)
        {
            base.Init(gameplayView, args);
            ShurikenView = gameplayView as ShurikenView;
        }

        public override void OnUpdate()
        {
            ShurikenView.OnUpdate();

            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0)
            {
                ShootShuriken();
                currentDelay = ShootDelay;
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
            var shurikenBulletDate = new ShurikenBulletData {movementDirection = PlayerController.ViewDirection, speed = 2.5f};
            var shurikenBulletController = ControllerFactory.CreateController<ShurikenBulletController>(Context, PlayerController, shurikenBulletDate);
            shurikenBulletControllers.AddController(shurikenBulletController);
            var effectsLayer = Context.GetGameplayLayer(GameplayLayer.Effects);
            shurikenBulletController.ShurikenBulletView.Activate(effectsLayer, PlayerController.PlayerPosition);
        }
    }
}
