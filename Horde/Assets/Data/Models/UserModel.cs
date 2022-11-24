using System;

namespace Data.Models
{
    [Serializable]
    public class UserTitleData : ITitleData
    {
        public int Gold;
        public int Gems;
    }
    
    [Serializable]
    public class UserUserData : IUserData
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
    
    public class UserModel : ModelWithUserData<UserUserData>, IModel
    {
        // References
        public EquipmentModel EquipmentModel;
        
        // Unsaved Data
        
        // Saved Data

        public int Level
        {
            get => UserData.Level;
            set
            {
                UserData.Level = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }
        
        public int Gold
        {
            get => UserData.Gold;
            set
            {
                UserData.Gold = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }
        
        public int Gems
        {
            get => UserData.Gems;
            set
            {
                UserData.Gems = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }

        public int MainChapterLevel
        {
            get => UserData.MainChapterLevel;
            set
            {
                UserData.MainChapterLevel = value;
                UserDataUpdater.UpdateUserData(UserData);
            }
        }
        
        public void InjectDependencies(EquipmentModel equipmentModel)
        {
            EquipmentModel = equipmentModel;
        }
    }
}