namespace Data.Models
{
    public abstract class ModelWithTitleData<TTitleData>
        where TTitleData : class, ITitleData
    {
        protected TTitleData TitleData;
        
        public void AddTitleData(ITitleData titleData)
        {
            TitleData = titleData as TTitleData;
        }
    }
    
    public abstract class ModelWithUserData<TUserData> where TUserData : class, IUserData
    {
        protected IUserDataUpdater UserDataUpdater;
        protected TUserData UserData;

        public void AddUserDataUpdater(IUserDataUpdater userDataUpdater)
        {
            UserDataUpdater = userDataUpdater;
        }

        public void AddUserData(IUserData userData)
        {
            UserData = userData as TUserData;
        }
    }

    public abstract class ModelWithTitleAndUserData<TTitleData, TUserData>
        where TUserData : class, IUserData
        where TTitleData : class, ITitleData
    {
        protected TTitleData TitleData;
        
        protected IUserDataUpdater UserDataUpdater;
        protected TUserData UserData;

        public void AddTitleData(ITitleData titleData)
        {
            TitleData = titleData as TTitleData;
        }
        
        public void AddUserDataUpdater(IUserDataUpdater userDataUpdater)
        {
            UserDataUpdater = userDataUpdater;
        }

        public void AddUserData(IUserData userData)
        {
            UserData = userData as TUserData;
        }
    }
}