using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallController : MonoBehaviour
{
    //Make sure to reset the collider so it won't interact with the player when spawned in again.
    private void OnDisable()
    {
        GetComponent<SphereCollider>().enabled = false;
    }
}
