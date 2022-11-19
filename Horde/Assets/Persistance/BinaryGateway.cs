using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Persistance.Gateway;
using UnityEngine;

namespace Persistance
{
    public class BinaryGateway
    {
        public void UpdateUserData<T>(T model)
        {
            var formatter = new BinaryFormatter();

            var path = Application.persistentDataPath + "/" + typeof(T).Name;
            var stream = new FileStream(path, FileMode.Create);
            
            formatter.Serialize(stream, model);
            stream.Close();
        }

        public UserDataField<T> GetUserData<T>() where T : class
        {
            var path = Application.persistentDataPath + "/" + typeof(T).Name;

            if (!File.Exists(path)) return null;
            
            var formatter = new BinaryFormatter();
            
            var stream = new FileStream(path, FileMode.Open);

            var model = formatter.Deserialize(stream) as UserDataField<T>;
            
            stream.Close();
            
            return model;

        }
    }
}