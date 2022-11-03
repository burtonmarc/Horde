using System;

namespace Data.Models
{
    [Serializable]
    public class UserModel : IModel
    {
        public int Level;

        public EquipmentModel EquipmentModel;
        
        public UserModel()
        {
            Level = 0;
            
            EquipmentModel = new EquipmentModel();
        }
    }
}