using System.Collections.Generic;
using UnityEngine;

namespace Catalogs.Scripts.Entries
{
    [CreateAssetMenu(fileName = "LevelEntry", menuName = "ScriptableObjects/Entries/Create Level Entry", order = 1)]
    public class LevelEntry : CatalogEntryId
    {
        public List<Wave> Waves;
    }
}