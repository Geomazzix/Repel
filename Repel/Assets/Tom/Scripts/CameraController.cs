using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _FollowTransform;

    [SerializeField]
    private float _FollowSpeed;

    private void Update()
    {
        Vector3 followPos = new Vector3(transform.position.x, _FollowTransform.position.y, _FollowTransform.position.z);
        transform.position = Vector3.Lerp(transform.position, followPos, _FollowSpeed * Time.deltaTime);
    }
}
