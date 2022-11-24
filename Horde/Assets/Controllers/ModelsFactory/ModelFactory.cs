using Data;
using Data.Models;
using Persistance.Gateway;

namespace Controllers.ModelsFactory
{
    public class ModelFactory : AModelFactory
    {
        private readonly ModelCreator<UserModel, UserUserData> userModelCreator;
        private readonly ModelCreator<EquipmentModel, EquipmentUserData> equipmentModelCreator;
        private readonly ModelCreator<PlayerModel, PlayerUserData> playerModelCreator;
        private readonly ModelCreator<LevelModel, LevelUserData, LevelTitleData> levelModelCreator;

        public ModelFactory(DataGateway dataGateway)
        {
            userModelCreator = new ModelCreator<UserModel, UserUserData>(dataGateway);
            equipmentModelCreator = new ModelCreator<EquipmentModel, EquipmentUserData>(dataGateway);
            playerModelCreator = new ModelCreator<PlayerModel, PlayerUserData>(dataGateway);
            levelModelCreator = new ModelCreator<LevelModel, LevelUserData, LevelTitleData>(dataGateway);
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