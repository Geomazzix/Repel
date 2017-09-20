using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{
    public sealed class PlayerController : MonoBehaviour
    {
        [Header("All deadly objects.")]
        [Tooltip("This array contains all the objects that kill the player when touched.")]
        [SerializeField]
        private GameObject[] _KillObjects;

        [Header("Other object references.")]
        [SerializeField]
        private GameManager _GameManager;

        [Header("Transformations")]
        [SerializeField]
        private float _DirectionAngle;

        [Header("Forces")]
        [SerializeField]
        private float _StartingMoveSpeed;
        [SerializeField]
        private float _Acceleration;

        private float _MoveSpeed;

        private float _CurrDirectionAngle;
        private bool _PlayerMayAccelerate;


        //Set player forces.
        private void Awake()
        {
            _MoveSpeed = _StartingMoveSpeed;
        }


        //Update all the functions that require a framecall.
        private void Update()
        {
            AcceleratePlayer();
            MovePlayer();
        }


        //Keeps adding speed to the player's movespeed, so he can keep up with the camera.
        //NOTE: Make sure the accelerationspeed is never higher as the camera. Because the player needs to pick up speedboosts to keep up with the camera.
        private void AcceleratePlayer()
        {
            _MoveSpeed += _Acceleration * Time.deltaTime;
        }


        //Move the player forward.
        private void MovePlayer()
        {
            transform.Translate(transform.forward * _MoveSpeed * Time.deltaTime, Space.World);
        }


        //Use the OnTriggerEnter to check if the player bumps into something that could kill him (NOTE: I use this function only for that cause and not for the playerballs
        //because the playerballs can also be spawned onto him which won't trigger the function).
        private void OnTriggerEnter(Collider other)
        {
            int killObjectsLength = _KillObjects.Length;
            for (int i = 0; i < killObjectsLength; i++)
            {
                if(_KillObjects[i] == other.gameObject)
                {
                    Die();
                }
            }
        }


        //Use the OnTriggerStay for object which can be spawned right on top of him, because the OnTriggerEnter will not get called when that happens.
        private void OnTriggerStay(Collider other)
        {
            //If it is not something that is supposed to kill the player, check if it is something which has effect on the player.
            if (other.gameObject.CompareTag("PlayerBall"))
            {
                Vector3 coreSide = transform.position - other.transform.position;
                if (coreSide.x < 0)
                {
                    //The x,y and z scale are all the same so I just use 1 here.
                    _CurrDirectionAngle = other.transform.localScale.x * -_DirectionAngle;
                }
                else if (coreSide.x > 0)
                {
                    //The x,y and z scale are all the same so I just use 1 here.
                    _CurrDirectionAngle = other.transform.localScale.x * _DirectionAngle;
                }
                else
                {
                    _CurrDirectionAngle = 0f;
                }
            }
        }


        //When the player dies make sure to tell the gameManager that, he will give the signal to everyone else.
        public void Die()
        {
            //_GameManager.SetGameState("AFTER_PLAYER_RUN");
        }
    }
}