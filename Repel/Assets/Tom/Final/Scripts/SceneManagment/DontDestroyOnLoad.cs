using UnityEngine;

namespace Repel
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            //Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
        }
    }
}