using ControllersPool;
using Data.Scripts;
using ScreenMachine;

namespace Data
{
    public class Context
    {
        public readonly CatalogsHolder CatalogsHolder;
        
        public readonly AssetLoaderFactory AssetLoaderFactory;

        public readonly IScreenMachine ScreenMachine;

        public IControllersPool ControllersPool { get; set; }
        
        public Context(
            CatalogsHolder catalogsHolder, 
            AssetLoaderFactory assetLoaderFactory,
            IScreenMachine screenMachine
        )
        {
            CatalogsHolder = catalogsHolder;
            AssetLoaderFactory = assetLoaderFactory;
            ScreenMachine = screenMachine;
        }
    }
}