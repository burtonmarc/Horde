using Data;
using Data.Models;
using Persistance.Gateway;

namespace Controllers.ModelsFactory
{
    public class ModelFactory : AModelFactory
    {
        private readonly DataGateway dataGateway;

        private readonly ModelCreator<UserModel, UserModelData> userModelCreator;
        private readonly ModelCreator<EquipmentModel, EquipmentModelData> equipmentModelCreator;
        private readonly ModelCreator<PlayerModel, PlayerModelData> playerModelCreator;
        private readonly ModelCreator<LevelModel, LevelModelData> levelModelCreator;

        public ModelFactory(DataGateway dataGateway)
        {
            this.dataGateway = dataGateway;

            userModelCreator = new ModelCreator<UserModel, UserModelData>(dataGateway);
            equipmentModelCreator = new ModelCreator<EquipmentModel, EquipmentModelData>(dataGateway);
            playerModelCreator = new ModelCreator<PlayerModel, PlayerModelData>(dataGateway);
            levelModelCreator = new ModelCreator<LevelModel, LevelModelData>(dataGateway);
        }

        public override UserModel GetUserModel()
        {
            return userModelCreator.GetModel();
        }

        public override EquipmentModel GetEquipmentModel()
        {
            return equipmentModelCreator.GetModel();
        }

        public override PlayerModel GetPlayerModel()
        {
            return playerModelCreator.GetModel();
        }

        public override EnemyModel GetEnemyModel()
        {
            return new EnemyModel();
        }

        public override LevelModel GetLevelModel()
        {
            return levelModelCreator.GetModel();
        }
    }
}