using System.IO;
using Data.Models;
using Persistance.Gateway;
using UnityEngine;

namespace Controllers.ModelsFactory
{
    public class ModelCreator<TModel, TModelData>
        where TModel : class, IModel, new()
        where TModelData : class, IModelData, new()
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

        private TModel CreateModel(IModelData modelData)
        {
            if (typeof(SaveableBaseModel).IsAssignableFrom(typeof(TModel)))
            {
                var model = new TModel() as SaveableBaseModel;
                model?.AddSaveSystem(dataGateway);
                model?.AddModelData(modelData);
                return model as TModel;
            }
            
            return new TModel();
        }
    }
}