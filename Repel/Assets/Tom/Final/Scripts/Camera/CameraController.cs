using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{
    public sealed class CameraController : MonoBehaviour
    {
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


        //Updates all framecalls.
        private void Update()
        {
            AccelerateCamera();
            MoveCamera();
        }


        //Keep adding speed to the movespeed of the camera so the player will have to keep up.
        private void AccelerateCamera()
        {
            _MoveSpeed += _Acceleration * Time.deltaTime;
        }


        //Moves the camera into the movedirection with the movespeed.
        private void MoveCamera()
        {
            transform.Translate(_MoveDirection * _MoveSpeed * Time.deltaTime, Space.World);
        }
    }
}