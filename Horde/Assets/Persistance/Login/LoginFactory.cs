namespace Persistance.Login
{
    public class LoginFactory
    {
        public static BaseLogin Create(RequestError requestError)
        {
#if  UNITY_EDITOR
            return new EditorLogin(requestError);
#elif UNITY_ANDROID
            return new AndroidLogin(requestError);
#elif UNITY_IOS
            return new IosLogin(requestError);
#endif
        }
    }
}