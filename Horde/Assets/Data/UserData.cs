using System;

namespace Data
{
    [Serializable]
    public class UserData
    {
        public int level;

        /// <summary>
        /// Constructor used for non-first time users
        /// </summary>
        /// <param name="userModel"></param>
        public UserData(UserModel userModel)
        {
            level = userModel.level;
        }

        /// <summary>
        /// Constructor used for new users
        /// </summary>
        public UserData()
        {
            level = 0;
        }
    }
}