using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Persistance.Login
{
    public class EditorLogin : BaseLogin
    {
        public EditorLogin(RequestError requestError) : base(requestError)
        {
        }
        
        public override void Login(Action<LoginResult> onLoginSuccess)
        {
            var request = new LoginWithCustomIDRequest
            {
                CreateAccount = true,
                CustomId = SystemInfo.deviceUniqueIdentifier
            };
            
            var requester = new Requester<LoginWithCustomIDRequest, LoginResult>(
                PlayFabClientAPI.LoginWithCustomID,
                request,
                RequestError);
            
            requester.RequestAsync(onLoginSuccess);
        }
    }
}