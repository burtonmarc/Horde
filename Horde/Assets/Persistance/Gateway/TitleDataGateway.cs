using System.Collections.Generic;
using Data.Models;
using PlayFab.ClientModels;
using UnityEngine;

namespace Persistance.Gateway
{
    public class TitleDataGateway
    {
        private readonly RequestError requestError;
        private Dictionary<string, string> titleData;

        public TitleDataGateway(GetTitleDataResult titleDataResult)
        {
            titleData = titleDataResult.Data;
        }
        
        public T Get<T>() where T : class, ISerializableData
        {
            titleData.TryGetValue(typeof(T).Name, out var json);

            if (json != null)
            {
                return JsonUtility.FromJson<T>(json);
            }

            return null;
        }
    }
}