using System;
using System.Collections.Generic;
using Catalogs.Scripts.Entries;

namespace Data.Models
{
    [Serializable]
    public class LevelTitleData : ISerializableData
    {
        public List<Wave> Waves;
    }
    
    [Serializable]
    public class LevelUserData : ISerializableData
    {
        public int CurrentWave;
    }
    
    public class LevelModel : SaveableBaseModel, IModel
    {
        private LevelUserData levelUserData;
        
        // References
        
        // Unsaved Data
        
        // Saved Data
        public int CurrentWave
        {
            get => levelUserData.CurrentWave;
            set
            {
                levelUserData.CurrentWave = value;
                UserDataUpdater.UpdateUserData(levelUserData);
            }
        }

        public LevelModel()
        {
            CurrentWave = 0;
        }

        public override void AddModelData(ISerializableData userData)
        {
            levelUserData = userData as LevelUserData;
        }
    }
}