using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [Tooltip("Store here all the different pool objects in.")]
    [SerializeField]
    private GameObject[] _PoolObjects;

    //Activates an entity.
    public void ActivatePoolObject(Vector3 position, Vector3 eulerAngle, Vector3 scale)
    {
        for (int i = 0; i < _PoolObjects.Length; i++)
        {
            if(!_PoolObjects[i].gameObject.activeInHierarchy)
            {
                _PoolObjects[i].transform.position = position;
                _PoolObjects[i].transform.eulerAngles = eulerAngle;
                _PoolObjects[i].transform.localScale = scale;
                _PoolObjects[i].gameObject.SetActive(true);
                break;
            }
        }
    }


    //Deactivates an entity.
    public void DeactivatePoolObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }


    //Check if the requested object is available in the pool.
    public bool IsPoolObjectInPool(GameObject requestedPoolObject)
    {
        for (int i = 0; i < _PoolObjects.Length; i++)
        {
            if(_PoolObjects[i] == requestedPoolObject)
            {
                return true;
            }
        }

        return false;
    }
}
