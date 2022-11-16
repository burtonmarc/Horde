using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayFabCore
{
    public class TitleDataGateway
    {
        private readonly RequestError requestError;
        private Dictionary<string, string> titleData;

        public TitleDataGateway(GetTitleDataResult titleDataResult)
        {
            titleData = titleDataResult.Data;
        }
        
        public T Get<T>()
        {
            titleData.TryGetValue(typeof(T).Name, out var json);
            return JsonUtility.FromJson<T>(json);
        }
    }
}