using System.Collections.Generic;
using Catalogs.Scripts.Entries;

namespace Data
{
    public class LevelModel : IModel
    {
        public List<Wave> Waves;

        public int CurrentWaveCount;

        public LevelModel(List<Wave> waves)
        {
            Waves = waves;

            CurrentWaveCount = 0;
        }
    }
}