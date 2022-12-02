using System;
using System.Linq;
using Data;
using Data.Models;
using Persistance.Gateway;

namespace Controllers.ModelsFactory
{
    public class ModelFactory : IModelFactory
    {
        private readonly DataGateway dataGateway;
        
        private readonly ModelCreatorWithUserData<UserModel, UserUserData> userModelCreator;
        private readonly ModelCreatorWithUserData<EquipmentModel, EquipmentUserData> equipmentModelCreator;
        private readonly ModelCreatorWithTitleAndUserData<PlayerModel, PlayerTitleData, PlayerUserData> playerModelCreator;
        private readonly ModelCreatorWithTitleData<EnemyModel, EnemiesTitleData> enemyModelCreator;
        private readonly ModelCreatorWithTitleAndUserData<LevelModel, LevelTitleData, LevelUserData> levelModelCreator;

        public ModelFactory(DataGateway dataGateway)
        {
            this.dataGateway = dataGateway;
            
            userModelCreator = new ModelCreatorWithUserData<UserModel, UserUserData>(dataGateway);
            equipmentModelCreator = new ModelCreatorWithUserData<EquipmentModel, EquipmentUserData>(dataGateway);
            playerModelCreator = new ModelCreatorWithTitleAndUserData<PlayerModel, PlayerTitleData, PlayerUserData>(dataGateway);
            enemyModelCreator = new ModelCreatorWithTitleData<EnemyModel, EnemiesTitleData>(dataGateway);
            levelModelCreator = new ModelCreatorWithTitleAndUserData<LevelModel, LevelTitleData, LevelUserData>(dataGateway);
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

        public EnemyModel GetEnemyModel(string enemyId)
        {
            var enemyModel = enemyModelCreator.GetModel();
            enemyModel.Init(enemyId);
            return enemyModel;
        }

        public LevelModel GetLevelModel()
        {
            return levelModelCreator.GetModel();
        }
    }
}