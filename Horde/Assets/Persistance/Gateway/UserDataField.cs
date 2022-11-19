using System;

namespace Persistance.Gateway
{
    [Serializable]
    public class UserDataField<T> where T : class
    {
        public readonly T Data;
        public readonly DateTime LastUpdate;

        public UserDataField(T data, DateTime lastUpdate)
        {
            Data = data;
            LastUpdate = lastUpdate;
        }
    }
}