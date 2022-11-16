using System;
using System.Collections.Generic;
using System.IO;
using Data.Models;
using UnityEngine;

namespace Data
{
    public class ModelsFactory
    {
        private readonly BinarySaveSystem binarySaveSystem;

        private readonly ModelCreator<UserModel, UserModelData> userModelCreator;
        private readonly ModelCreator<EquipmentModel, EquipmentModelData> equipmentModelCreator;
        private readonly ModelCreator<PlayerModel, PlayerModelData> playerModelCreator;
        private readonly ModelCreator<LevelModel, LevelModelData> levelModelCreator;

        public ModelsFactory(BinarySaveSystem binarySaveSystem)
        {
            this.binarySaveSystem = binarySaveSystem;
            
            userModelCreator = new ModelCreator<UserModel, UserModelData>(binarySaveSystem);
            equipmentModelCreator = new ModelCreator<EquipmentModel, EquipmentModelData>(binarySaveSystem);
            playerModelCreator = new ModelCreator<PlayerModel, PlayerModelData>(binarySaveSystem);
            levelModelCreator = new ModelCreator<LevelModel, LevelModelData>(binarySaveSystem);
        }

        public UserModel GetUserModel()
        {
            return userModelCreator.GetModel();
        }

        public EquipmentModel GetEquipmentModel()
        {
            return equipmentModelCreator.GetModel();
        }

        public PlayerModel GetPlayerModel()
        {
            return playerModelCreator.GetModel();
        }

        public EnemyModel GetEnemyModel()
        {
            return new EnemyModel();
        }

        public LevelModel GetLevelModel()
        {
            return levelModelCreator.GetModel();
        }
    }
}