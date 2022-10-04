using Controllers.States.MainMenuState;
using Controllers.States.StartupState;
using Data;
using Data.Scripts;
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
            var assetLoaderFactory = new AssetLoaderFactory();

            screenMachine = new ScreenMachine(catalogs.StatesCatalog, assetLoaderFactory);
            
            var context = new Context(catalogs, assetLoaderFactory, screenMachine);
            
            screenMachine.PresentState(new StartupStateController(context));
        }

        private void Update()
        {
            screenMachine.OnUpdate();
        }
    }
}
