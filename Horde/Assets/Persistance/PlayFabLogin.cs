using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers;
using Data.Models;
using Persistance.Gateway;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Persistance
{
    public class PlayFabLogin
    {
        public Action<DataGateway> OnLoginComplete;
        private DataGateway dataGateway;

        public void StartLogin()
        {
            //var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true};
            var request = new LoginWithCustomIDRequest { CustomId = Random.Range(1000, 10000).ToString(), CreateAccount = true};
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

            var binarySaveSystem = new BinaryGateway();

            var userDataGateway = new UserDataGateway(binarySaveSystem, userDataResult, userDataUpdater);

            dataGateway.AddUserDataGateway(userDataGateway);
            
            OnUserDataResult(userDataResult);
        }

        private void OnLoginFailure(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }
    
        private void OnUserDataResult(GetUserDataResult userDataResult)
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

            var updateTasks = new List<Task>
            {
                dataGateway.UpdateUserData(new UserInitializedModelData()),
                dataGateway.UpdateUserData(dataGateway.GetTitleData<UserModelData>()),
                dataGateway.UpdateUserData(dataGateway.GetTitleData<EquipmentModelData>())
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
                    nameof(UserInitializedModelData),
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