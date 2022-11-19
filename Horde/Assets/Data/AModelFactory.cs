using Data.Models;

namespace Data
{
    public abstract class AModelFactory
    {
        public abstract UserModel GetUserModel();
        public abstract EquipmentModel GetEquipmentModel();
        public abstract PlayerModel GetPlayerModel();
        public abstract EnemyModel GetEnemyModel();
        public abstract LevelModel GetLevelModel();
    }
}