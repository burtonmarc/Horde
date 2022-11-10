namespace Data.Models
{
    public abstract class SaveableBaseModel
    {
        protected SaveSystem SaveSystem;

        public void AddSaveSystem(SaveSystem saveSystem)
        {
            SaveSystem = saveSystem;
        }

        public abstract void AddModelData(IModelData modelData);

    }
}