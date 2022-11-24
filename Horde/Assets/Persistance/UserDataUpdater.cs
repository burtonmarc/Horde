using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Persistance
{
    public class UserDataUpdater
    {
        private const int DirtyFieldsNecessaryToUpdate = 1;
    
        private readonly Dictionary<string, string> dirtyFields;

        private readonly List<string> fieldsUpdated;
        
        private readonly List<Task<UpdateUserDataResult>> requesters;

        public UserDataUpdater()
        {
            dirtyFields = new Dictionary<string, string>();
            fieldsUpdated = new List<string>(DirtyFieldsNecessaryToUpdate);
            requesters = new List<Task<UpdateUserDataResult>>();
        }

        public void AddFieldAsDirty<T>(T dirtyField) where T : IUserData
        {
            var fieldName = typeof(T).Name;
            var json = JsonUtility.ToJson(dirtyField);

            if (dirtyFields.ContainsKey(fieldName))
            {
                dirtyFields[fieldName] = json;
            }
            else
            {
                dirtyFields.Add(fieldName, json);
            }
        }

        public bool CanUpdate()
        {
            return dirtyFields.Count >= DirtyFieldsNecessaryToUpdate;
        }

        public async Task Update()
        {
            // TODO: Test this to send all dirty fields in the same Data. 10?
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
                    RequestError.Instance);
                var requestTask = requester.RequestAsync();
                requesters.Add(requestTask);
                fieldsUpdated.Add(dirtyField.Key);
            }
            
            foreach (var fieldUpdated in fieldsUpdated)
            {
                dirtyFields.Remove(fieldUpdated);
            }

            await Task.WhenAll(requesters);
            
            requesters.Clear();
        }
    }
}