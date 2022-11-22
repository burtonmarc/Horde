using System;
using UnityEngine;

namespace Persistance
{
    public class RequestError : MonoBehaviour
    {
        public static RequestError Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowLoading()
        {
            
        }

        public void HideLoading()
        {
            
        }
    }
}