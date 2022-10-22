using UnityEngine;

namespace Catalogs.Scripts.Configs
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/Configs/Create Camera Config", order = 1)]
    public class CameraConfig : ScriptableObject
    {
        public GameObject Camera;
    }
}