using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayFabCore
{
    public class UserDataUpdater
    {
        private const int DirtyFieldsNecessaryToUpdate = 1;
    
        private readonly Dictionary<string, string> dirtyFields;

        public UserDataUpdater()
        {
            dirtyFields = new Dictionary<string, string>();
        }

        public void AddFieldAsDirty<T>(T dirtyField) where T : class
        {
            var fieldName = typeof(T).Name;
            var json = JsonUtility.ToJson(dirtyField);
            dirtyFields.Add(fieldName, json);
        }

        public bool CanUpdate()
        {
            return dirtyFields.Count >= DirtyFieldsNecessaryToUpdate;
        }

        public async Task Update()
        {
            var requesters = new List<Task<UpdateUserDataResult>>();
            foreach (var dirtyField in dirtyFields)
            {
                var request = new UpdateUserDataRequest{
                    Data = new Dictionary<string, string>
                    {
                        {dirtyField.Key, dirtyField.Value}
                    }
                };
                var requester = new Requester<UpdateUserDataRequest, UpdateUserDataResult>(
                    PlayFabClientAPI.UpdateUserData,
                    request,
                    new RequestError());
                var requestTask = requester.RequestAsync();
                requesters.Add(requestTask);
            }

            await Task.WhenAll(requesters);
        }
    }
}