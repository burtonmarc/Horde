using System;

namespace Data.Models
{
    [Serializable]
    public class PlayerTitleData : ISerializableData
    {
        
    }
    
    [Serializable]
    public class PlayerUserData : ISerializableData
    {
        public int HealthPoints;
    }
    
    [Serializable]
    public class PlayerModel : SaveableBaseModel, IModel
    {
        private PlayerUserData playerUserData;
        
        // References
        
        // Unsaved Data
        public float MovementSpeed;
        
        // Saved Data
        public int HealthPoints
        {
            get => playerUserData.HealthPoints;
            set
            {
                playerUserData.HealthPoints = value;
                UserDataUpdater.UpdateUserData(playerUserData);
            }
        }

        public PlayerModel()
        {
            // For Testing
            HealthPoints = 100;
            MovementSpeed = 2;
        }

        public override void AddModelData(ISerializableData userData)
        {
            playerUserData = userData as PlayerUserData;
        }
    }
}