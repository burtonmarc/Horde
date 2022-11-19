namespace Data.Models
{
    public abstract class SaveableBaseModel
    {
        protected IDataGateway DataGateway;

        public void AddSaveSystem(IDataGateway dataGateway)
        {
            DataGateway = dataGateway;
        }

        public abstract void AddModelData(IModelData modelData);

    }
}