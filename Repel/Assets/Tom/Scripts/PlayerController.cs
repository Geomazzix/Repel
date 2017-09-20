using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    TODO: Make sure the player can't turn or at least see the skybox.
*/

public class PlayerController : MonoBehaviour
{

#region Inspector
    [Header("Layers")]
    [SerializeField]
    private LayerMask _ReflectLayer;

    [Header("Forces")]
    [Tooltip("This movespeed will be the speed at which he starts accelerating.")]
    [SerializeField]
    private float _MoveSpeed;
    [Tooltip("The starting speed is the maximum speed he starts at.")]
    [SerializeField]
    private float _StartingMoveSpeed;
    [Tooltip("The force which he accelerates with towards the starting speed.")]
    [SerializeField]
    private float _StartingAcceleration, _Acceleration;

    [Header("Angles")]
    [SerializeField]
    private float _StartAngleMin = 30f;
    [SerializeField]
    private float _StartAngleMax = 30f, _DirectionAngle = 0f;

    [Header("Maximum angles.")]
    [SerializeField]
    private Vector3 _MaxLeftAngle;
    [SerializeField]
    private Vector3 _MaxRightAngle;

    [Header("The starting point of the playerrun.")]
    [SerializeField]
    private Transform _StartingPoint;
#endregion

#region Private members
    private float _CurrDirectionAngle, _Score = 0f;
    private bool _AngleChanged = false;
#endregion

#region Properties
    public float Score
    {
        get { return _Score; }
    }
#endregion


    //Set a starting direction.
    private void Start()
    {
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x, 
            Mathf.Round(Random.Range(transform.eulerAngles.y - _StartAngleMin, transform.eulerAngles.y + _StartAngleMax)), 
            transform.eulerAngles.z);
    }


    //Call all the updated player functions.
    private void Update()
    {
        MovePlayer();
        ReflectPlayer();
        AdjustPlayerDirection();
        UpdateScore();
    }


    //Moves the player (made it for readability code).
    private void MovePlayer()
    {
        if(_MoveSpeed < _StartingMoveSpeed)
        {
            _MoveSpeed = Mathf.Lerp(_MoveSpeed, _StartingMoveSpeed, _StartingAcceleration * Time.deltaTime);
        }

        _MoveSpeed += _Acceleration * Time.deltaTime;
        transform.Translate(transform.forward * _MoveSpeed * Time.deltaTime, Space.World);
    }


    //Checks if the player needs to reflect, if so then reflect him else ignore it.
    private void ReflectPlayer()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime * _MoveSpeed + 0.5f, _ReflectLayer))
        {
            //Show the normal of the plane
            Debug.DrawRay(transform.position, transform.forward);

            //Reflect the electricity
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);

            //Calculate the turn of the electricity
            float rot = Mathf.Atan2(reflectDir.x, reflectDir.z) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }
        else
        {
            float eulerAnglesY = Mathf.Clamp(transform.eulerAngles.y, _MaxLeftAngle.y, _MaxRightAngle.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, eulerAnglesY, 0));
            Debug.Log("Eulerrotation: " + transform.eulerAngles);
        }
    }


    //Adjust the player direction to the direction he is supposed to be going.
    private void AdjustPlayerDirection()
    {
        transform.eulerAngles += new Vector3(0, _CurrDirectionAngle * Time.deltaTime, 0);
    }

    
    //Keeps adding score according to your distance.
    private void UpdateScore()
    {
        float traveled = transform.position.z - _StartingPoint.position.z;
        if (traveled > 0)
        {
            _Score = traveled;
        }
    }


    //Check when the player enters a player created ball, when entered check the distance and calculate the rotation circle.s
    private void OnTriggerStay(Collider other)
    {
        //Make sure to fix the 9 to an appropraite layer.
        if (other.gameObject.CompareTag("PlayerBall"))
        {
            Vector3 coreSide = transform.position - other.transform.position;

            if(!_AngleChanged)
            {
                if (coreSide.x < 0)
                {
                    //The x,y and z scale are all the same so I just use 1 here.
                    _CurrDirectionAngle = other.transform.localScale.x * -_DirectionAngle;
                    _AngleChanged = true;
                }
                else if (coreSide.x > 0)
                {
                    //The x,y and z scale are all the same so I just use 1 here.
                    _CurrDirectionAngle = other.transform.localScale.x * _DirectionAngle;
                    _AngleChanged = true;
                }
                else
                {
                    _DirectionAngle = 0f;
                }
            }
        }
        else if(other.gameObject.CompareTag("DeadlyObstacle"))
        {
            Die();
        }
    }



    private void OnTriggerExit(Collider other)
    {
        _AngleChanged = false;
    }


    //Destroys the player.
    private void Die()
    {
        //Spawn in particle system for playerdeath.
        //Send score to scoremanager.
        //Update UI by calling the player death delegate.
        gameObject.SetActive(false);
    }
}
