namespace Data.Models
{
    public abstract class SaveableBaseModel
    {
        protected BinarySaveSystem BinarySaveSystem;

        public void AddSaveSystem(BinarySaveSystem binarySaveSystem)
        {
            BinarySaveSystem = binarySaveSystem;
        }

        public abstract void AddModelData(IModelData modelData);

    }
}