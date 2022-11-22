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

        public readonly AModelFactory ModelFactory;

        public readonly ITitleDataRetriever TitleDataRetriever;

        public readonly UserModel UserModel;

        public IControllersPool PoolController;

        public Context(CatalogsHolder catalogsHolder,
            AssetLoaderFactory assetLoaderFactory,
            IScreenMachine screenMachine,
            AModelFactory modelFactory,
            ITitleDataRetriever titleDataRetriever,
            UserModel userModel
        )
        {
            CatalogsHolder = catalogsHolder;
            AssetLoaderFactory = assetLoaderFactory;
            ScreenMachine = screenMachine;
            ModelFactory = modelFactory;
            TitleDataRetriever = titleDataRetriever;
            UserModel = userModel;
        }
    }
}