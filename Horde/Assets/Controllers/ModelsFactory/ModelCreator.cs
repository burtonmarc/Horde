using System;
using Data.Models;
using Persistance.Gateway;

namespace Controllers.ModelsFactory
{
    public class ModelCreatorWithTitleData<TModel, TTitleData>
        where TModel : ModelWithTitleData<TTitleData>, new()
        where TTitleData : class, ITitleData
    {
        private readonly DataGateway dataGateway;

        public ModelCreatorWithTitleData(DataGateway dataGateway)
        {
            this.dataGateway = dataGateway;
        }
        
        public TModel GetModel()
        {
            var titleData = dataGateway.GetTitleData<TTitleData>();
            var model = new TModel();
            model.AddTitleData(titleData);
            return model;
        }
    }

    public class ModelCreatorWithUserData<TModel, TUserData>
        where TModel : ModelWithUserData<TUserData>, new()
        where TUserData : class, IUserData
    {
        private readonly DataGateway dataGateway;

        public ModelCreatorWithUserData(DataGateway dataGateway)
        {
            this.dataGateway = dataGateway;
        }
        
        public TModel GetModel()
        {
            var userData = dataGateway.GetUserData<TUserData>();
            var model = CreateModel(userData);
            return model;
        }

        private TModel CreateModel(IUserData userData)
        {
            var model = new TModel();
            model.AddUserDataUpdater(dataGateway);
            model.AddUserData(userData);
            return model;
        }
    }

    public class ModelCreatorWithTitleAndUserData<TModel, TTitleData, TUserData>
        where TModel : ModelWithTitleAndUserData<TTitleData, TUserData>, new()
        where TTitleData : class, ITitleData
        where TUserData : class, IUserData
    {
        private readonly DataGateway dataGateway;

        public ModelCreatorWithTitleAndUserData(DataGateway dataGateway)
        {
            this.dataGateway = dataGateway;
        }
        
        public TModel GetModel()
        {
            var titleData = dataGateway.GetTitleData<TTitleData>();
            var userData = dataGateway.GetUserData<TUserData>();
            var model = CreateModel(titleData, userData);
            return model;
        }

        private TModel CreateModel(TTitleData titleData, TUserData userData)
        {
            var model = new TModel();
            model.AddTitleData(titleData);
            model.AddUserDataUpdater(dataGateway);
            model.AddUserData(userData);
            return model;
        }
    }
}