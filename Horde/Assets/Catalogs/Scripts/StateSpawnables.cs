using System;
using System.Collections.Generic;
using UnityEngine;
using Views.States.GameplayState;

namespace Catalogs.Scripts
{
    public class StateSpawnables : ScriptableObject
    {
        public List<SpawnableData> spawnables;
    }

    [Serializable]
    public class SpawnableData
    {
        public GameplayViewBase view;

        public ScriptableObject config;
    }
}