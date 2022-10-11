using System.Collections.Generic;
using UnityEngine;
using Views.States.GameplayState;

namespace Catalogs.Scripts
{
    public class StateSpawnables : ScriptableObject
    {
        public List<GameplayViewBase> spawnables;
    }
}