using System;
using PlayFab.ClientModels;

namespace Persistance.Login
{
    public abstract class BaseLogin
    {
        protected readonly RequestError RequestError;

        protected BaseLogin(RequestError requestError)
        {
            this.RequestError = requestError;
        }

        public abstract void Login(Action<LoginResult> onLoginSuccess);
    }
}