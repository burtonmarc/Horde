namespace Data
{
    public class UserModel
    {
        public int level;
        
        public UserModel(UserData userData)
        {
            level = userData.level;
        }
    }
}