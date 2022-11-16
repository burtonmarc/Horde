using System.Threading.Tasks;
using Data;
using Data.Models;

namespace PlayFabCore
{
    public class DataGateway
    {
        private TitleDataGateway titleDataGateway;
        private UserDataGateway userDataGateway;
        private BinarySaveSystem binarySaveSystem;

        public void AddTitleDataGateway(TitleDataGateway titleDataGateway)
        {
            this.titleDataGateway = titleDataGateway;
        }

        public void AddUserDataGateway(UserDataGateway userDataGateway)
        {
            this.userDataGateway = userDataGateway;
        }

        #region UserData

        public async Task UpdateUserData<T>(T data) where T : class
        {
            await userDataGateway.Update(data);
        }

        public T GetUserData<T>() where T : class
        {
            //var binaryData = binarySaveSystem.LoadModelData<T>();
            var userData = userDataGateway.Get<T>();
            return userData;
        }

        #endregion


        #region TitleData

        public T GetTitleData<T>()
        {
            return titleDataGateway.Get<T>();
        }

        #endregion
    }
}