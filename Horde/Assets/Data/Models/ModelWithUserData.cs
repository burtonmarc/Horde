namespace Data.Models
{
    public abstract class ModelWithUserData<TUserData> where TUserData : class, IUserData
    {
        protected IUserDataUpdater UserDataUpdater;
        protected TUserData UserData;

        public void AddSaveSystem(IUserDataUpdater userDataUpdater)
        {
            UserDataUpdater = userDataUpdater;
        }

        public void AddUserData(IUserData userData)
        {
            UserData = userData as TUserData;
        }
    }

    public abstract class ModelWithUserDataAndTitleData<TUserData, TTitleData> : ModelWithUserData<TUserData>
        where TUserData : class, IUserData
        where TTitleData : class, ITitleData
    {
        protected TTitleData TitleData;

        public void AddTitleData(ITitleData titleData)
        {
            TitleData = titleData as TTitleData;
        }
    }
}