using System.Collections.Generic;
using Catalogs.Scripts.Entries;

namespace Data.Models
{
    public class LevelModelData : IModelData
    {
        public int CurrentWaveCount;
    }
    
    public class LevelModel : SaveableBaseModel, IModel
    {
        private LevelModelData levelModelData;
        
        // References
        
        // Unsaved Data
        public List<Wave> Waves;
        
        // Saved Data
        public int CurrentWaveCount
        {
            get => levelModelData.CurrentWaveCount;
            set
            {
                levelModelData.CurrentWaveCount = value;
                BinarySaveSystem.SaveModelData(this);
            }
        }

        public LevelModel()
        {
            CurrentWaveCount = 0;
        }

        public override void AddModelData(IModelData modelData)
        {
            levelModelData = modelData as LevelModelData;
        }

        public void InjectDependencies(List<Wave> waves)
        {
            Waves = waves;
        }
    }
}