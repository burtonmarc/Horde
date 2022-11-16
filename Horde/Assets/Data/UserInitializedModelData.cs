using System;
using JetBrains.Annotations;

namespace Controllers
{
    [Serializable]
    public class UserInitializedModelData
    {
        [UsedImplicitly]
        public bool UserInitialized;
    }
}