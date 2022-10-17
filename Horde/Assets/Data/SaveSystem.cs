using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Data
{
    public class SaveSystem
    {
        public void SaveModel<T>(T userModel)
        {
            var formatter = new BinaryFormatter();

            var path = Application.persistentDataPath + "/" + typeof(T).Name;
            var stream = new FileStream(path, FileMode.Create);
            
            formatter.Serialize(stream, userModel);
            stream.Close();
        }

        public T LoadModel<T>() where T : class, new()
        {
            var path = Application.persistentDataPath + "/" + typeof(T).Name;
            if (File.Exists(path))
            {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(path, FileMode.Open);

                var model = formatter.Deserialize(stream) as T;
                stream.Close();

                return model;
            }
            
            return new T();
        }
    }
}