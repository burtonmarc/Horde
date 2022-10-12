using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Data
{
    public class SaveSystem
    {
        private const string UserDataPath = "/UserData.txt";
        
        public void SaveUserData(UserModel userModel)
        {
            var formatter = new BinaryFormatter();

            var path = Application.persistentDataPath + UserDataPath;
            var stream = new FileStream(path, FileMode.Create);
            
            var userData = new UserData(userModel);
            
            formatter.Serialize(stream, userData);
            stream.Close();
        }

        public UserData LoadUserData()
        {
            var path = Application.persistentDataPath + UserDataPath;
            if (File.Exists(path))
            {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(path, FileMode.Open);

                var userData = formatter.Deserialize(stream) as UserData;
                stream.Close();

                return userData;
            }
            
            return new UserData();
        }
    }
}