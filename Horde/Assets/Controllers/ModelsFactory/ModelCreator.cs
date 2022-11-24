using System;
using Data.Models;
using Persistance.Gateway;

namespace Controllers.ModelsFactory
{
    public class ModelCreator<TModel, TUserData>
        where TModel : class, IModel, new()
        where TUserData : class, IUserData
    {
        private readonly DataGateway dataGateway;

        public ModelCreator(DataGateway dataGateway)
        {
            this.dataGateway = dataGateway;
        }
        
        public TModel GetModel()
        {
            var userData = GetUserData();
            var model = CreateModel(userData);
            return model;
        }

        private TUserData GetUserData()
        {
            var userData = dataGateway.GetUserData<TUserData>();
            return userData;
        }

        private TModel CreateModel(IUserData userData)
        {
            if (typeof(ModelWithUserData<TUserData>).IsAssignableFrom(typeof(TModel)))
            {
                var model = new TModel() as ModelWithUserData<TUserData>;
                model?.AddSaveSystem(dataGateway);
                model?.AddUserData(userData);
                return model as TModel;
            }
            
            return new TModel();
        }
    }

    public class ModelCreator<TModel, TUserData, TTitleData> : ModelCreator<TModel, TUserData>
        where TModel : class, IModel, new()
        where TUserData : class, IUserData
        where TTitleData : class, ITitleData
    {
        private readonly DataGateway dataGateway;

        public ModelCreator(DataGateway dataGateway) : base(dataGateway)
        {
            this.dataGateway = dataGateway;
        }

        public new TModel GetModel()
        {
            var model = base.GetModel();
            var titleData = GetTitleData();

            if (model is ModelWithUserDataAndTitleData<TUserData, TTitleData> configurableModel)
            {
                // TODO: Check if model.userdata is null and titledata is not null to add it to user data?
                // Usecase: When a new titledata is added and current users don't have it in their user data?
                
                
                configurableModel.AddTitleData(titleData);
                return configurableModel as TModel;
            }

            throw new Exception($"Model {typeof(TModel).Name} does not have Title Data, check {nameof(GetType)}");
        }

        private TTitleData GetTitleData()
        {
            var titleData = dataGateway.GetTitleData<TTitleData>();
            return titleData;
        }
    }
}