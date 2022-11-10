using System;
using Catalogs.Scripts;
using Controllers.States.GameplayState;
using Controllers.States.StartupState;
using Data;
using Data.Models;
using ScreenMachine;
using UnityEngine;

namespace Controllers
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private CatalogsHolder catalogs;
        
        private ScreenMachine screenMachine;

        void Start()
        {
            Application.targetFrameRate = 60;
            
            var assetLoaderFactory = new AssetLoaderFactory();

            screenMachine = new ScreenMachine(catalogs.StatesCatalog, assetLoaderFactory);
            
            var saveSystem = new SaveSystem();

            var modelsGateway = new ModelsFactory(saveSystem);

            var userModel = CreateUserModel(modelsGateway);

            var context = new Context(catalogs, assetLoaderFactory, screenMachine, modelsGateway, userModel);

            ControllerViewFactory.Context = context;
            
            screenMachine.PresentState(new StartupStateController(context));
        }

        private void Update()
        {
            screenMachine.OnUpdate();
        }

        private void FixedUpdate()
        {
            screenMachine.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            screenMachine.OnLateUpdate();
        }

        private UserModel CreateUserModel(ModelsFactory modelsFactory)
        {
            var equipmentModel = modelsFactory.GetEquipmentModel();
            
            var userModel = modelsFactory.GetUserModel();
            userModel.InjectDependencies(equipmentModel);

            return userModel;
        }
    }
}
