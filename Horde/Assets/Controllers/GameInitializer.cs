using Catalogs.Scripts;
using Controllers.ModelsFactory;
using Controllers.States.GameplayState;
using Controllers.States.StartupState;
using Data;
using Data.Models;
using Persistance;
using Persistance.Gateway;
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
            var playFabLogin = new PlayFabLogin();
            
            playFabLogin.OnLoginComplete += OnLoginComplete;
            
            playFabLogin.StartLogin();
        }

        private void OnLoginComplete(DataGateway dataGateway)
        {
            Application.targetFrameRate = 60;
            
            var assetLoaderFactory = new AssetLoaderFactory();

            screenMachine = new ScreenMachine(catalogs.StatesCatalog, assetLoaderFactory);
            
            var modelFactory = new ModelFactory(dataGateway);

            var userModel = CreateUserModel(modelFactory);

            var context = new Context(catalogs, assetLoaderFactory, screenMachine, modelFactory, dataGateway, userModel);

            ControllerViewFactory.Context = context;
            
            screenMachine.PresentState(new StartupStateController(context));
        }

        private void Update()
        {
            screenMachine?.OnUpdate();
        }

        private void FixedUpdate()
        {
            screenMachine?.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            screenMachine?.OnLateUpdate();
        }

        private UserModel CreateUserModel(ModelFactory modelFactory)
        {
            var equipmentModel = modelFactory.GetEquipmentModel();
            
            var userModel = modelFactory.GetUserModel();
            userModel.InjectDependencies(equipmentModel);

            return userModel;
        }
    }
}
