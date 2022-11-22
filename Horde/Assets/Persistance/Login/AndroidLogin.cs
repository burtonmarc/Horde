using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Persistance.Login
{
    public class AndroidLogin : BaseLogin
    {
        public AndroidLogin(RequestError requestError) : base(requestError)
        {
        }

        public override void Login(Action<LoginResult> onLoginSuccess)
        {
            var request = new LoginWithAndroidDeviceIDRequest
            {
                CreateAccount = true,
                AndroidDeviceId = SystemInfo.deviceUniqueIdentifier
            };
            
            var requester = new Requester<LoginWithAndroidDeviceIDRequest, LoginResult>(
                PlayFabClientAPI.LoginWithAndroidDeviceID,
                request,
                RequestError);

            requester.RequestAsync(onLoginSuccess);
        }
    }
}