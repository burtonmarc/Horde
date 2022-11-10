using System;

namespace Data.Models
{
    [Serializable]
    public class UserModelData : IModelData
    {
        public int Level;
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
                SaveSystem.SaveModelData(userModelData);
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