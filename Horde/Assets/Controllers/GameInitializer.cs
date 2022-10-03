using Data.Scripts;
using Game.States.StartupState;
using ScreenMachine;
using UnityEngine;

namespace Game.GameInitialization
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private CatalogsHolder catalogs;
        
        private ScreenMachine screenMachine;

        void Start()
        {
            var assetLoaderFactory = new AssetLoaderFactory();
            
            screenMachine = new ScreenMachine(catalogs.StatesCatalog, assetLoaderFactory);
            
            screenMachine.PresentState(new StartupStateController());
        }

        private void Update()
        {
            screenMachine.OnUpdate();
        }
    }
}
