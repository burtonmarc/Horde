using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using PlayFab.ClientModels;
using UnityEngine;

namespace Persistance.Gateway
{
    public class UserDataGateway
    {
        private readonly BinaryGateway binaryGateway;
        private Dictionary<string, UserDataRecord> userDataRecords;
        private readonly UserDataUpdater userDataUpdater;

        public UserDataGateway(
            BinaryGateway binaryGateway, 
            GetUserDataResult userDataResult,
            UserDataUpdater userDataUpdater)
        {
            this.binaryGateway = binaryGateway;
            userDataRecords = userDataResult.Data;
            this.userDataUpdater = userDataUpdater;
        }

        public void UpdateUserDataResult(GetUserDataResult userDataResult)
        {
            // If user is not initialized, we add the title data to playfab and download
            // it again to update the UserDataGateway with the new userData.
            userDataRecords = userDataResult.Data;
        }

        public T Get<T>() where T : class
        {
            var localData = binaryGateway.GetUserData<T>();
            var userData = GetUserDataFieldFromUserData<T>();

            var localDataNull = localData == null;
            var userDataNull = userData == null;

            if (localDataNull && !userDataNull)
            {
                return userData.Data;
            }

            if (!localDataNull && userDataNull)
            {
                return localData.Data;
            }

            if (localDataNull && userDataNull)
            {
                return null;
            }

            if (userData.LastUpdate >= localData.LastUpdate)
            {
                return userData.Data;
            }

            return localData.Data;
        }

        public async Task Update<T>(T value) where T : IModelData
        {
            binaryGateway.UpdateUserData(value);
            
            userDataUpdater.AddFieldAsDirty(value);

            if (userDataUpdater.CanUpdate())
            {
                await userDataUpdater.Update();
            }
        }
        
        private UserDataField<T> GetUserDataFieldFromUserData<T>() where T : class
        {
            userDataRecords.TryGetValue(typeof(T).Name, out var userDataRecord);

            if (userDataRecord != null)
            {
                var modelData = JsonUtility.FromJson<T>(userDataRecord.Value);
                return new UserDataField<T>(modelData, userDataRecord.LastUpdated);
            }

            return null;
        }
    }
}