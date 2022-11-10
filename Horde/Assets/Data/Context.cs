using Catalogs.Scripts;
using ControllersPool;
using Data.Models;
using ScreenMachine;

namespace Data
{
    public class Context
    {
        public AssetLoader Preloader;

        public readonly CatalogsHolder CatalogsHolder;
        
        public readonly AssetLoaderFactory AssetLoaderFactory;

        public readonly IScreenMachine ScreenMachine;

        public readonly ModelsFactory ModelsFactory;

        public readonly UserModel UserModel;

        public IControllersPool PoolController;

        public Context(CatalogsHolder catalogsHolder,
            AssetLoaderFactory assetLoaderFactory,
            IScreenMachine screenMachine,
            ModelsFactory modelsFactory,
            UserModel userModel
        )
        {
            CatalogsHolder = catalogsHolder;
            AssetLoaderFactory = assetLoaderFactory;
            ScreenMachine = screenMachine;
            ModelsFactory = modelsFactory;
            UserModel = userModel;
        }
    }
}