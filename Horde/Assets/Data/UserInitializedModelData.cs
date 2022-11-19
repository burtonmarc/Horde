using System;
using Data.Models;
using JetBrains.Annotations;

namespace Controllers
{
    [Serializable]
    public class UserInitializedModelData : IModelData
    {
        [UsedImplicitly]
        public bool UserInitialized;

        public UserInitializedModelData()
        {
            UserInitialized = true;
        }
    }
}