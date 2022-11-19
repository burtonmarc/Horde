using System;

namespace Data.Models
{
    [Serializable]
    public class UserModelData : IModelData
    {
        public int Level;
        public int Gold;
        public int Gems;
    }
    
    public class UserModel : SaveableBaseModel, IModel
    {
        private UserModelData userModelData;
        
        // References
        public EquipmentModel EquipmentModel;
        
        // Unsaved Data
        
        // Saved Data

        public int Level
        {
            get => userModelData.Level;
            set
            {
                userModelData.Level = value;
                DataGateway.UpdateUserData(userModelData);
            }
        }
        
        public int Gold
        {
            get => userModelData.Gold;
            set
            {
                userModelData.Gold = value;
                DataGateway.UpdateUserData(userModelData);
            }
        }
        
        public int Gems
        {
            get => userModelData.Gems;
            set
            {
                userModelData.Gems = value;
                DataGateway.UpdateUserData(userModelData);
            }
        }
        
        public override void AddModelData(IModelData modelData)
        {
            userModelData = modelData as UserModelData;
        }

        public void InjectDependencies(EquipmentModel equipmentModel)
        {
            EquipmentModel = equipmentModel;
        }
    }
}