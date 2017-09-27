using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _FollowPoint;

        [Tooltip("The player turning causes the camera to slow down, this creates a very unsatisfying effect which can be 'fixed' by putting a lesser speed on the camera then on the player.")]
        [SerializeField]
        private float _FollowPointSpeedOffset;

        [SerializeField]
        private PlayerController _Player;


        //Move the camera.
        private void Update()
        {
            Vector3 followPos = new Vector3(transform.position.x, transform.position.y, _FollowPoint.position.z);
            transform.position = Vector3.Lerp(transform.position, followPos, (_Player.MoveSpeed - _FollowPointSpeedOffset) * Time.deltaTime);
        }
    }
}