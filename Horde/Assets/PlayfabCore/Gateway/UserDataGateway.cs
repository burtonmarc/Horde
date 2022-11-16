using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using PlayFab.ClientModels;

namespace PlayFabCore
{
    public class UserDataGateway
    {
        private Dictionary<string, UserDataRecord> userData;
        private UserDataUpdater userDataUpdater;

        public UserDataGateway(GetUserDataResult userDataResult, UserDataUpdater userDataUpdater)
        {
            userData = userDataResult.Data;
            this.userDataUpdater = userDataUpdater;
        }

        public void UpdateUserDataResult(GetUserDataResult userDataResult)
        {
            // If user is not initialized, we add the title data to playfab and download
            // it again to update the UserDataGateway with the new userData.
            userData = userDataResult.Data;
        }

        public T Get<T>() where T : class
        {
            userData.TryGetValue(typeof(T).Name, out var userDataField);

            if (userDataField != null)
            {
                return userDataField.Value as T;
            }
            
            throw new Exception($"Type {typeof(T).Name} does not exist in the user data");
        }

        public async Task Update<T>(T value) where T : class
        {
            userDataUpdater.AddFieldAsDirty(value);

            if (userDataUpdater.CanUpdate())
            {
                await userDataUpdater.Update();
            }
        }
    }
}