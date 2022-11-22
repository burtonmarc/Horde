using Data.Models;
using Persistance.Gateway;

namespace Controllers.ModelsFactory
{
    public class ModelCreator<TModel, TModelData>
        where TModel : class, IModel, new()
        where TModelData : class, ISerializableData
    {
        private DataGateway dataGateway;

        public TModel GetModel()
        {
            var modelData = LoadOrCreateModelData();
            var model = CreateModel(modelData);
            return model;
        }

        public ModelCreator(DataGateway dataGateway)
        {
            this.dataGateway = dataGateway;
        }

        private TModelData LoadOrCreateModelData()
        {
            var modelData = dataGateway.GetUserData<TModelData>();
            return modelData;
        }

        private TModel CreateModel(ISerializableData userData)
        {
            if (typeof(SaveableBaseModel).IsAssignableFrom(typeof(TModel)))
            {
                var model = new TModel() as SaveableBaseModel;
                model?.AddSaveSystem(dataGateway);
                model?.AddModelData(userData);
                return model as TModel;
            }
            
            return new TModel();
        }
    }
}