using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Persistance.Login
{
    public class IosLogin : BaseLogin
    {
        public IosLogin(RequestError requestError) : base(requestError)
        {
        }

        public override void Login(Action<LoginResult> onLoginSuccess)
        {
            var request = new LoginWithIOSDeviceIDRequest
            {
                CreateAccount = true,
                DeviceId = SystemInfo.deviceUniqueIdentifier
            };
            
            var requester = new Requester<LoginWithIOSDeviceIDRequest, LoginResult>(
                PlayFabClientAPI.LoginWithIOSDeviceID,
                request,
                RequestError);
            
            requester.RequestAsync(onLoginSuccess);
        }
    }
}