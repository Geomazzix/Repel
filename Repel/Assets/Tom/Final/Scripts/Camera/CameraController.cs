using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _Player;

        [SerializeField]
        private Vector3 _MoveDirection;

        [SerializeField]
        private float _StartingSpeed, _Acceleration;

        private float _MoveSpeed;


        //Set starting values of the camera.
        private void Awake()
        {
            _MoveSpeed = _StartingSpeed;
        }


        private void Update()
        {
            transform.Translate(_MoveDirection * _MoveSpeed * Time.deltaTime, Space.World);
        }
    }
}