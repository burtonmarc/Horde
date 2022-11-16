using System.IO;
using Data.Models;
using UnityEngine;

namespace Data
{
    public class ModelCreator<TModel, TModelData>
        where TModel : class, IModel, new()
        where TModelData : class, IModelData, new()
    {
        private BinarySaveSystem binarySaveSystem;

        public TModel GetModel()
        {
            var modelData = LoadOrCreateModelData();
            var model = CreateModel(modelData);
            return model;
        }

        public ModelCreator(BinarySaveSystem binarySaveSystem)
        {
            this.binarySaveSystem = binarySaveSystem;
        }
        
        private TModelData LoadOrCreateModelData()
        {
            var path = Application.persistentDataPath + "/" + typeof(TModelData).Name;
            
            if (File.Exists(path))
            {
                return binarySaveSystem.LoadModelData<TModelData>(path);
            }

            return new TModelData();
        }

        private TModel CreateModel(IModelData modelData)
        {
            if (typeof(SaveableBaseModel).IsAssignableFrom(typeof(TModel)))
            {
                var model = new TModel() as SaveableBaseModel;
                model?.AddSaveSystem(binarySaveSystem);
                model?.AddModelData(modelData);
                return model as TModel;
            }
            
            return new TModel();
        }
    }
}