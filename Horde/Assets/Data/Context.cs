using Data.Scripts;
using ScreenMachine;

namespace Data
{
    public class Context
    {
        public CatalogsHolder CatalogsHolder;
        
        public AssetLoaderFactory AssetLoaderFactory;

        public IScreenMachine ScreenMachine;

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