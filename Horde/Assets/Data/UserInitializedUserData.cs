using System;
using Data.Models;
using JetBrains.Annotations;

namespace Data
{
    [Serializable]
    public class UserInitializedUserData : IUserData
    {
        [UsedImplicitly]
        public bool UserInitialized;

        public UserInitializedUserData()
        {
            UserInitialized = true;
        }
    }
}