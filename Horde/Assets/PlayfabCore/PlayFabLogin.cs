using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers;
using Data.Models;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayFabCore
{
    public class PlayFabLogin
    {
        public Action<DataGateway> OnLoginComplete;
        private DataGateway dataGateway;

        public void StartLogin()
        {
            var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true};
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        
            //var androidLoginRequest = new LoginWithAndroidDeviceIDRequest
            //{
            //    AndroidDeviceId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true
            //};
            //PlayFabClientAPI.LoginWithAndroidDeviceID(androidLoginRequest, OnLoginSuccess, OnLoginFailure);
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

            var userDataGateway = new UserDataGateway(userDataResult, userDataUpdater);

            dataGateway.AddUserDataGateway(userDataGateway);
            
            OnUserDataResult(userDataResult, userDataGateway);
        }

        private void OnLoginFailure(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }
    
        private void OnUserDataResult(GetUserDataResult userDataResult, UserDataGateway userDataGateway)
        {
            if (!userDataResult.Data.ContainsKey(typeof(UserInitializedModelData).Name))
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
            
            var updateTasks = new List<Task>();

            await dataGateway.UpdateUserData(dataGateway.GetTitleData<EquipmentModelData>());
            
            //updateTasks.Add(dataGateway.UpdateUserData(dataGateway.GetTitleData<UserModelData>()));
            //updateTasks.Add(dataGateway.UpdateUserData(dataGateway.GetTitleData<EquipmentModelData>()));
            
            //await Task.WhenAll(updateTasks);
            
            Debug.Log("Initial User Data added successfully");
            
            OnLoginComplete?.Invoke(dataGateway);
        }

        private async Task<GetUserDataResult> GetUserData()
        {
            var userDataRequest = new GetUserDataRequest
            {
                Keys = new List<string>
                {
                    nameof(UserModelData),
                    nameof(EquipmentModelData),
                }
            };

            var userDataRequester = new Requester<GetUserDataRequest, GetUserDataResult>(
                PlayFabClientAPI.GetUserData,
                userDataRequest,
                new RequestError());

            var userDataResult = await userDataRequester.RequestAsync();
            return userDataResult;
        }
        
        private async Task<GetTitleDataResult> GetTitleData()
        {
            var request = new GetTitleDataRequest();
            var requester = new Requester<GetTitleDataRequest, GetTitleDataResult>(
                PlayFabClientAPI.GetTitleData,
                request,
                new RequestError());
            
            var titleDataResult = await requester.RequestAsync();
            return titleDataResult;
        }
    }
}