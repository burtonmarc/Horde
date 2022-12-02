using Data.Models;

namespace Data
{
    public interface IModelFactory
    {
        UserModel GetUserModel();
        EquipmentModel GetEquipmentModel();
        PlayerModel GetPlayerModel();
        EnemyModel GetEnemyModel(string enemyId);
        LevelModel GetLevelModel();
    }
}