using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Controllers.States.GameplayState
{
    public class EntitiesCollisionController : GameplayControllerBase
    {
        private readonly List<GameplayControllerBase> pickablesControllers;
        
        private readonly List<GameplayControllerBase> pickablesControllersEndOfFrame;

        private readonly List<GameplayControllerBase> weaponsControllers;
        
        private readonly List<GameplayControllerBase> weaponsControllersEndOfFrame;
        
        private readonly List<GameplayControllerBase> enemiesControllers;
        
        private readonly List<GameplayControllerBase> enemiesControllersEndOfFrame;

        public EntitiesCollisionController(Context context) : base(context)
        {
            pickablesControllers = new List<GameplayControllerBase>(128);
            pickablesControllersEndOfFrame = new List<GameplayControllerBase>(128);
            weaponsControllers = new List<GameplayControllerBase>(32);
            weaponsControllersEndOfFrame = new List<GameplayControllerBase>(32);
            enemiesControllers = new List<GameplayControllerBase>(64);
            enemiesControllersEndOfFrame = new List<GameplayControllerBase>(64);

            AddPickable += AddNewEnemy;
            AddWeapon += AddNewWeapon;
            AddEnemy += AddNewEnemy;
        }

        public override void OnUpdate()
        {
            foreach (var pickablesController in pickablesControllers)
            {
                pickablesController.OnUpdate();
            }

            foreach (var weaponController in weaponsControllers)
            {
                weaponController.OnUpdate();
            }

            foreach (var enemyController in enemiesControllers)
            {
                enemyController.OnUpdate();
            }

            for (var index = pickablesControllersEndOfFrame.Count - 1; index >= 0; index--)
            {
                var pickableController = pickablesControllersEndOfFrame[index];
                pickablesControllers.Add(pickableController);
                pickablesControllersEndOfFrame.RemoveAt(index);
            }

            for (var index = weaponsControllersEndOfFrame.Count - 1; index >= 0; index--)
            {
                var weaponController = weaponsControllersEndOfFrame[index];
                weaponsControllers.Add(weaponController);
                weaponsControllersEndOfFrame.RemoveAt(index);
            }

            for (var index = enemiesControllersEndOfFrame.Count - 1; index >= 0; index--)
            {
                var enemyController = enemiesControllersEndOfFrame[index];
                enemiesControllers.Add(enemyController);
                enemiesControllersEndOfFrame.RemoveAt(index);
            }
        }

        public override void OnFixedUpdate()
        {
            foreach (var pickablesController in pickablesControllers)
            {
                pickablesController.OnFixedUpdate();
            }
            
            foreach (var weaponController in weaponsControllers)
            {
                weaponController.OnFixedUpdate();
            }
            
            foreach (var enemyController in enemiesControllers)
            {
                enemyController.OnFixedUpdate();
            }
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();

            for (var index = pickablesControllers.Count - 1; index >= 0; index--)
            {
                var pickablesController = pickablesControllers[index];
                pickablesController.OnLateUpdate();
            }

            for (var index = weaponsControllers.Count - 1; index >= 0; index--)
            {
                var weaponController = weaponsControllers[index];
                weaponController.OnLateUpdate();
            }

            for (var index = enemiesControllers.Count - 1; index >= 0; index--)
            {
                var enemyController = enemiesControllers[index];
                enemyController.OnLateUpdate();
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            
            
        }

        private void AddNewPickable(GameplayControllerBase pickableController)
        {
            pickablesControllersEndOfFrame.Add(pickableController);
            pickableController.UpdateListReference = pickablesControllers;
        }
        
        private void AddNewWeapon(GameplayControllerBase weaponController)
        {
            weaponsControllersEndOfFrame.Add(weaponController);
            weaponController.UpdateListReference = weaponsControllers;
        }
        
        private void AddNewEnemy(GameplayControllerBase enemyController)
        {
            enemiesControllersEndOfFrame.Add(enemyController);
            enemyController.UpdateListReference = enemiesControllers;
        }
        
        public void RemoveRandomEnemy()
        {
            var index = Random.Range(0, enemiesControllers.Count - 1);
            enemiesControllers[index].OnDestroy();
        }

        public void RemoveAllEnemies()
        {
            for (var index = enemiesControllers.Count - 1; index >= 0; index--)
            {
                var waveEntity = enemiesControllers[index];
                waveEntity.OnDestroy();
            }
        }
    }
}