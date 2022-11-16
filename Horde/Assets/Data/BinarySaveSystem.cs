using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Data.Models;
using UnityEngine;

namespace Data
{
    public class BinarySaveSystem
    {
        public void SaveModelData<T>(T model)
        {
            var formatter = new BinaryFormatter();

            var path = Application.persistentDataPath + "/" + typeof(T).Name;
            var stream = new FileStream(path, FileMode.Create);
            
            formatter.Serialize(stream, model);
            stream.Close();
        }

        public T LoadModelData<T>(string path) where T : class
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            var model = formatter.Deserialize(stream) as T;
            stream.Close();

            return model;
        }
    }
}