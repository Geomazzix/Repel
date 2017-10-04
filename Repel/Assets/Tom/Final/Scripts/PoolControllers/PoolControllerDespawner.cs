using UnityEngine;

namespace Repel
{
    public class PoolControllerDespawner : MonoBehaviour
    {
        [SerializeField]
        private PoolController _PoolController;


        //Check if the object is the other collider, after that check if it is even incluced in the poolcontroller.
        private void OnTriggerEnter(Collider other)
        {
            GameObject otherGM = other.gameObject;

            if(_PoolController.IsPoolObjectInPool(otherGM))
            {
                _PoolController.DeactivatePoolObject(otherGM);
            }
        }
    }
}