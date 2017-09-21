using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton _SingleTon;

    private void Awake()
    {
        if (_SingleTon == null)
        {
            DontDestroyOnLoad(gameObject);
            _SingleTon = this;
        }
        else if (_SingleTon != this)
        {
            Destroy(gameObject);
        }
    }
}
