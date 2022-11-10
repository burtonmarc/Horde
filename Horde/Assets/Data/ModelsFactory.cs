using System;
using System.Collections.Generic;
using System.IO;
using Data.Models;
using UnityEngine;

namespace Data
{
    public class ModelsFactory
    {
        private SaveSystem saveSystem;

        public ModelsFactory(SaveSystem saveSystem)
        {
            this.saveSystem = saveSystem;
        }
        
        private T LoadOrCreateModelData<T>() where T : class, new()
        {
            var path = Application.persistentDataPath + "/" + typeof(T).Name;
            
            if (File.Exists(path))
            {
                return saveSystem.LoadModelData<T>(path);
            }

            return new T();
        }

        private T CreateModel<T>(IModelData modelData) where T : class, IModel, new()
        {
            if (typeof(SaveableBaseModel).IsAssignableFrom(typeof(T)))
            {
                var model = new T() as SaveableBaseModel;
                model?.AddSaveSystem(saveSystem);
                model?.AddModelData(modelData);
                return model as T;
            }
            
            return new T();
        }

        public UserModel GetUserModel()
        {
            var modelData = LoadOrCreateModelData<UserModelData>();
            var model = CreateModel<UserModel>(modelData);
            return model;
        }

        public EquipmentModel GetEquipmentModel()
        {
            var modelData = LoadOrCreateModelData<EquipmentModelData>();
            var model = CreateModel<EquipmentModel>(modelData);
            return model;
        }

        public PlayerModel GetPlayerModel()
        {
            var modelData = LoadOrCreateModelData<PlayerModelData>();
            var model = CreateModel<PlayerModel>(modelData);
            return model;
        }

        public EnemyModel GetEnemyModel()
        {
            return new EnemyModel();
        }

        public LevelModel GetLevelModel()
        {
            var modelData = LoadOrCreateModelData<LevelModelData>();
            var model = CreateModel<LevelModel>(modelData);
            return model;
        }
    }
}