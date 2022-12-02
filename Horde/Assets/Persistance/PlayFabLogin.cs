using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Persistance.Gateway;
using Persistance.Login;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Persistance
{
    public class PlayFabLogin
    {
        public Action<DataGateway> OnLoginComplete;
        private DataGateway dataGateway;

        public void StartLogin()
        {
            var login = LoginFactory.Create(RequestError.Instance);
            login.Login(OnLoginSuccess);
        }

        private async void OnLoginSuccess(LoginResult result)
        {
            Debug.Log("Login Successful: " + result.PlayFabId);
            
            dataGateway = new DataGateway();

            var titleDataResult = await GetTitleData();
            
            Debug.Log("Title Data Retrieved");
            
            var titleDataGateway = new TitleDataGateway(titleDataResult);
            
            dataGateway.AddTitleDataGateway(titleDataGateway);
            
            var userDataUpdater = new UserDataUpdater();

            var userDataResult = await GetUserData();
            
            Debug.Log("User Data Retrieved");

            var binarySaveSystem = new BinaryGateway();

            var userDataGateway = new UserDataGateway(binarySaveSystem, userDataResult, userDataUpdater);

            dataGateway.AddUserDataGateway(userDataGateway);
            
            OnUserDataResult(userDataResult);
        }

        private void OnUserDataResult(GetUserDataResult userDataResult)
        {
            if (!userDataResult.Data.ContainsKey(typeof(UserInitializedUserData).Name))
            {
                Debug.Log("User is NOT initialized, initializing");
                InitializeUser();
            }
            else
            {
                Debug.Log("User is already initialized");
                OnLoginComplete?.Invoke(dataGateway);
            }
        }
    
        private async void InitializeUser()
        {
            Debug.Log("Adding initial User Data");

            var updateTasks = new List<Task>
            {
                dataGateway.UpdateUserData(new UserInitializedUserData()),
                dataGateway.UpdateUserData(new UserUserData(dataGateway.GetTitleData<UserTitleData>())),
                dataGateway.UpdateUserData(new EquipmentUserData(dataGateway.GetTitleData<EquipmentTitleData>())),
                dataGateway.UpdateUserData(new LevelUserData()),
                dataGateway.UpdateUserData(new PlayerUserData(dataGateway.GetTitleData<PlayerTitleData>())),
            };

            await Task.WhenAll(updateTasks);

            Debug.Log("Initial User Data added successfully");
            
            Debug.Log("Get User Data after adding initial User Data");
            
            var userDataResult = await GetUserData();
            
            dataGateway.UpdateUserDataResult(userDataResult);
            
            Debug.Log("User Data after initialization successfully retrieved");
            
            OnLoginComplete?.Invoke(dataGateway);
        }

        private async Task<GetUserDataResult> GetUserData()
        {
            var userDataRequest = new GetUserDataRequest
            {
                Keys = new List<string>
                {
                    nameof(UserInitializedUserData),
                    nameof(UserUserData),
                    nameof(EquipmentUserData),
                    nameof(LevelUserData),
                    nameof(PlayerUserData),
                }
            };

            var userDataRequester = new Requester<GetUserDataRequest, GetUserDataResult>(
                PlayFabClientAPI.GetUserData,
                userDataRequest,
                RequestError.Instance);

            var userDataResult = await userDataRequester.RequestAsync();
            return userDataResult;
        }
        
        private async Task<GetTitleDataResult> GetTitleData()
        {
            var request = new GetTitleDataRequest();
            var requester = new Requester<GetTitleDataRequest, GetTitleDataResult>(
                PlayFabClientAPI.GetTitleData,
                request,
                RequestError.Instance);
            
            var titleDataResult = await requester.RequestAsync();
            return titleDataResult;
        }
    }
}