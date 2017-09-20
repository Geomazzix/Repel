using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerBall : MonoBehaviour
{

#region Inspector
    [Header("The spawnlayer of the ball.")]
    [Tooltip("This layer resembles the height of the pivot of the ball.")]
    [SerializeField]
    private LayerMask _SpawnLayer;

    [Header("The playerball prefab.")]
    [SerializeField]
    private GameObject _Playerball;

    [Header("BallGrowspeed when spawning him.")]
    [SerializeField]
    private float _GrowSpeed = 0.03f;

    [Header("Ball scale values.")]
    [SerializeField]
    private Vector3 _MinScale;
    [SerializeField]
    private Vector3 _MaxScale;
    #endregion

    #region private members

    private GameObject _SpawningPlayerBall;
#endregion


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, _SpawnLayer))
        {
            //Start spawning the ball.
            if (Input.GetButtonDown("Fire1"))
            {
                _SpawningPlayerBall = Instantiate(_Playerball, hit.point, Quaternion.identity);
            }

            //When a ball is being spawned make sure to scale him.
            if (Input.GetButton("Fire1"))
            {
                _SpawningPlayerBall.transform.position = hit.point;
                _SpawningPlayerBall.transform.localScale += new Vector3(_GrowSpeed, _GrowSpeed, _GrowSpeed);
                _SpawningPlayerBall.transform.localScale = new Vector3(
                    Mathf.Clamp(_SpawningPlayerBall.transform.localScale.x, _MinScale.x, _MaxScale.x),
                    Mathf.Clamp(_SpawningPlayerBall.transform.localScale.y, _MinScale.y, _MaxScale.y),
                    Mathf.Clamp(_SpawningPlayerBall.transform.localScale.z, _MinScale.z, _MaxScale.z));
            }

            //Make sure to reset the spawning ball.
            if (Input.GetButtonUp("Fire1"))
            {
                _SpawningPlayerBall.tag = "PlayerBall";
                _SpawningPlayerBall = null;
            }
        }
    }
}
