using System.Threading.Tasks;
using Data;
using Data.Models;
using PlayFab.ClientModels;
using UnityEngine;

namespace Persistance.Gateway
{
    public class DataGateway : IUserDataUpdater
    {
        private TitleDataGateway titleDataGateway;
        private UserDataGateway userDataGateway;

        public void AddTitleDataGateway(TitleDataGateway titleDataGateway)
        {
            this.titleDataGateway = titleDataGateway;
        }

        public void AddUserDataGateway(UserDataGateway userDataGateway)
        {
            this.userDataGateway = userDataGateway;
        }

        public void UpdateUserDataResult(GetUserDataResult userDataResult)
        {
            userDataGateway.UpdateUserDataResult(userDataResult);
        }

        #region UserData

        public async Task UpdateUserData<T>(T data) where T : IUserData
        {
            Debug.Log($"Started updating {typeof(T)}");
            await userDataGateway.Update(data);
            Debug.Log($"Finished updating {typeof(T)}");
        }

        public T GetUserData<T>() where T : class, IUserData
        {
            var userData = userDataGateway.Get<T>();

            if (userData != null) return userData;

            return null;
        }

        #endregion


        #region TitleData

        public T GetTitleData<T>() where T : class, ITitleData
        {
            return titleDataGateway.Get<T>();
        }

        #endregion

    }
}