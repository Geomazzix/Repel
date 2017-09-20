using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Header("SpawnValues")]
    [SerializeField]
    private float _MinSpawnTimer;
    [SerializeField]
    private float _MaxSpawnTimer;

    [Header("The pool which contains the obstacles.")]
    [SerializeField]
    private PoolController _ObstaclePool;

    [Header("Spawnpositioning")]
    [SerializeField]
    private Transform _OuterSpawnPosLeft;
    [SerializeField]
    private Transform _OuterSpawnPosRight;

    [Header("Spawn scales")]
    [SerializeField]
    private float _MinSpawnScale;
    [SerializeField]
    private float _MaxSpawnScale;

    private float _SpawnTimer;


    private void Start()
    {
        _SpawnTimer = Random.Range(_MinSpawnTimer, _MaxSpawnTimer);
    }


    //Timer for the spawning.
    private void Update()
    {
        _SpawnTimer -= Time.deltaTime;
        if(_SpawnTimer <= 0)
        {
            SpawnObstacle();
            _SpawnTimer = Random.Range(_MinSpawnTimer, _MaxSpawnTimer);
        }
    }


    //Calls the obstaclePool to activate an object.
    private void SpawnObstacle()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(_OuterSpawnPosLeft.position.x, _OuterSpawnPosRight.position.x),
            _OuterSpawnPosLeft.position.y, 
            _OuterSpawnPosLeft.position.z);
        Vector3 spawnRot = new Vector3(0, 0, 0);    //SpawnRot = eulerangles.
        float spawnScale = Random.Range(_MinSpawnScale, _MaxSpawnScale);

        _ObstaclePool.ActivatePoolObject(spawnPos, spawnRot, new Vector3(spawnScale, spawnScale, spawnScale));
    }


    //This does not take much performance in because the pool array has a max amount of 10 objects.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DeadlyObstacle"))
        {
            if (_ObstaclePool.IsPoolObjectInPool(other.gameObject))
            {
                _ObstaclePool.DeactivatePoolObject(other.gameObject);
            }
        }
    }
}
