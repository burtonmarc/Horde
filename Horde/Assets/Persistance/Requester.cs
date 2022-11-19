using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.SharedModels;
using UnityEngine;

namespace Persistance
{
    public interface IRequester<TResult>
        where TResult: PlayFabResultCommon
    {
        Task<TResult> RequestAsync();
    }
    public class Requester<TRequest, TResult> : IRequester<TResult>
        where TRequest : PlayFabRequestCommon 
        where TResult : PlayFabResultCommon
    {
        private TaskCompletionSource<TResult> tcs;
        
        private readonly TRequest request;

        private const int TOTAL_RETRIES = 3;
        private readonly TimeSpan timeBetweenRetries = new TimeSpan(0, 0, 2);
        private int currentRetires;

        private readonly Action<TRequest, Action<TResult>, Action<PlayFabError>, object, Dictionary<string, string>> method;

        private readonly RequestError requestError;

        public Requester(
            Action<TRequest, Action<TResult>, Action<PlayFabError>, object, Dictionary<string, string>> method,
            TRequest request,
            RequestError requestError
        )
        {
            currentRetires = 0;
            this.method = method;
            this.request = request;
            this.requestError = requestError;
            tcs = new TaskCompletionSource<TResult>();
        }

        public async Task<TResult> RequestAsync()
        {
            method(request, OnSuccess, OnError, null, null);
            return await tcs.Task;
        }

        public void RequestAsync(Action<TResult> callback)
        {
            method(request, r =>
            {
                requestError.HideLoading();
                callback(r);
            },
            async error =>
            {
                var isConnectionError = GetIsConnectionError(error);
                if (isConnectionError)
                {
                    await DealWithConnectionError();
                    return;
                }

                Debug.LogError($"ServerError {error.GenerateErrorReport()}");
            }, null, null);
        }

        private void OnSuccess(TResult result)
        {
            requestError.HideLoading();
            Debug.Log("Request Received with data : " + result.ToJson());
            tcs.SetResult(result);
        }

        private async void OnError(PlayFabError error)
        {
            Debug.Log($"PlayFab Error: {error}");
            
            var isConnectionError = GetIsConnectionError(error);
            if (isConnectionError)
            {
                await DealWithConnectionError();
                return;
            }

            Debug.LogError($"ServerError {error.GenerateErrorReport()}");
            //_utcs.TrySetException(new ConnectionException(error));
        }

        private bool GetIsConnectionError(PlayFabError error)
        {
            return 
                error.Error == PlayFabErrorCode.ServiceUnavailable ||
                error.Error == PlayFabErrorCode.OverLimit ||
                error.Error == PlayFabErrorCode.UnkownError ||
                error.Error == PlayFabErrorCode.UnknownError ||
                error.Error == PlayFabErrorCode.APIRequestLimitExceeded ||
                error.Error == PlayFabErrorCode.DAULimitExceeded ||
                error.Error == PlayFabErrorCode.ConcurrentEditError ||
                error.Error == PlayFabErrorCode.APIClientRequestRateLimitExceeded ||
                error.Error == PlayFabErrorCode.CloudScriptAPIRequestError ||
                error.Error == PlayFabErrorCode.CloudScriptHTTPRequestError ||
                error.Error == PlayFabErrorCode.UnableToConnectToDatabase ||
                error.Error == PlayFabErrorCode.Unknown;
        }

        private async Task DealWithConnectionError()
        {
            if (currentRetires < TOTAL_RETRIES)
            {
                requestError.ShowLoading();

                await Task.Delay(timeBetweenRetries);
                
                Debug.Log($"Retry number: {currentRetires}");
                
                currentRetires++;
                await RequestAsync();
                return;
            }
            
            // TODO: Reset app at this point
            //_reset.Handle();
        }

        public class ConnectionException : Exception
        {
            public readonly PlayFabError Error;

            public ConnectionException(PlayFabError error)
            {
                Error = error;
            }
        }
    }
}