using System;

namespace Data
{
    [Serializable]
    public class UserModel : IModel
    {
        public int level;
        
        public UserModel()
        {
            level = 0;
        }
    }
}