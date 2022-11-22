namespace Data.Models
{
    public abstract class SaveableBaseModel
    {
        protected IUserDataUpdater UserDataUpdater;

        public void AddSaveSystem(IUserDataUpdater userDataUpdater)
        {
            UserDataUpdater = userDataUpdater;
        }

        public abstract void AddModelData(ISerializableData userData);

    }
}