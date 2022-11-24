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
    }
    
    [Serializable]
    public class PlayerModel : ModelWithUserDataAndTitleData<PlayerUserData, PlayerTitleData>, IModel
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

        public PlayerModel()
        {
            
        }
    }
}