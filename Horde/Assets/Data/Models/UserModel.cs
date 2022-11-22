using System;

namespace Data.Models
{
    [Serializable]
    public class UserTitleData : ISerializableData
    {
        public int Gold;
        public int Gems;
    }
    
    [Serializable]
    public class UserUserData : ISerializableData
    {
        public int Level;
        public int Gold;
        public int Gems;
        
        public int MainChapterLevel;

        public UserUserData(UserTitleData userTitleData)
        {
            Level = 1;
            Gold = userTitleData.Gold;
            Gems = userTitleData.Gems;
            MainChapterLevel = 1;
        }
    }
    
    public class UserModel : SaveableBaseModel, IModel
    {
        private UserUserData userUserData;
        
        // References
        public EquipmentModel EquipmentModel;
        
        // Unsaved Data
        
        // Saved Data

        public int Level
        {
            get => userUserData.Level;
            set
            {
                userUserData.Level = value;
                UserDataUpdater.UpdateUserData(userUserData);
            }
        }
        
        public int Gold
        {
            get => userUserData.Gold;
            set
            {
                userUserData.Gold = value;
                UserDataUpdater.UpdateUserData(userUserData);
            }
        }
        
        public int Gems
        {
            get => userUserData.Gems;
            set
            {
                userUserData.Gems = value;
                UserDataUpdater.UpdateUserData(userUserData);
            }
        }

        public int MainChapterLevel
        {
            get => userUserData.MainChapterLevel;
            set
            {
                userUserData.MainChapterLevel = value;
                UserDataUpdater.UpdateUserData(userUserData);
            }
        }
        
        public override void AddModelData(ISerializableData userData)
        {
            userUserData = userData as UserUserData;
        }

        public void InjectDependencies(EquipmentModel equipmentModel)
        {
            EquipmentModel = equipmentModel;
        }
    }
}