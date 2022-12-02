using System;

namespace Data.Models
{
    [Serializable]
    public class PlayerTitleData : ITitleData
    {
        public int BaseHealthPoints;
        public float BaseMovementSpeed;
    }
    
    [Serializable]
    public class PlayerUserData : IUserData
    {
        public int HealthPoints;

        public PlayerUserData(PlayerTitleData playerTitleData)
        {
            HealthPoints = playerTitleData.BaseHealthPoints;
        }
    }
    
    [Serializable]
    public class PlayerModel : ModelWithTitleAndUserData<PlayerTitleData, PlayerUserData>, IModel
    {
        // References
        
        // Unsaved Data
        public float MovementSpeed => TitleData.BaseMovementSpeed;

        // Saved Data
        public int HealthPoints
        {
            get => UserData.HealthPoints;
            set
            {
                UserData.HealthPoints = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }
    }
}