using System;

namespace Data.Models
{
    public class PlayerModelData : IModelData
    {
        public int HealthPoints;
    }
    
    [Serializable]
    public class PlayerModel : SaveableBaseModel, IModel
    {
        private PlayerModelData playerModelData;
        
        // References
        
        // Unsaved Data
        public float MovementSpeed;
        
        // Saved Data
        public int HealthPoints
        {
            get => playerModelData.HealthPoints;
            set
            {
                playerModelData.HealthPoints = value;
                DataGateway.UpdateUserData(playerModelData);
            }
        }

        public PlayerModel()
        {
            // For Testing
            HealthPoints = 100;
            MovementSpeed = 2;
        }

        public override void AddModelData(IModelData modelData)
        {
            playerModelData = modelData as PlayerModelData;
        }
    }
}