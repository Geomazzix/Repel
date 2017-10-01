using UnityEngine;

namespace Repel
{
    public sealed class ObstacleController : MonoBehaviour
    {
        [Header("SpawnValues")]
        [SerializeField] private float _MinSpawnTimer;
        [SerializeField] private float _MaxSpawnTimer;

        [Tooltip("This value is the time it takes for the 1st spawn")]
        [SerializeField] private float _SpawnTimer;

        [Header("The pool which contains the obstacles.")]
        [SerializeField] private PoolController _ObstaclePool;

        [Header("Spawnpositioning")]
        [SerializeField] private Transform _OuterSpawnPosLeft;
        [SerializeField] private Transform _OuterSpawnPosRight;

        [Header("Spawn scales")]
        [SerializeField] private float _MinSpawnScale;
        [SerializeField] private float _MaxSpawnScale;

        [Header("MoveSpeed values")]
        [SerializeField] private PlayerController _Player;


        //Timer for the spawning.
        private void Update()
        {
            SpawnObstacleTimer();
            FollowPlayer();
        }


        //Make sure to match the playerspeed.
        private void FollowPlayer()
        {
            transform.position += new Vector3(0, 0, _Player.MoveSpeed * Time.deltaTime);
        }


        //Counts down the timer required to spawn the obstacles.
        private void SpawnObstacleTimer()
        {
            _SpawnTimer -= Time.deltaTime;
            if (_SpawnTimer <= 0)
            {
                SpawnObstacle();
                _SpawnTimer = Random.Range(_MinSpawnTimer, _MaxSpawnTimer);

                if (Random.value > 0.75f)
                {
                    SpawnObstacle();
                }
            }
        }


        //Calls the obstaclePool to activate an object.
        private void SpawnObstacle()
        {
            //Set all the other spawn values.
            Vector3 spawnPos = new Vector3(
                Random.Range(_OuterSpawnPosLeft.position.x, _OuterSpawnPosRight.position.x),
                _OuterSpawnPosLeft.position.y,
                _OuterSpawnPosLeft.position.z);
            Vector3 spawnRot = new Vector3(0, 0, 0);    //SpawnRot = eulerangles.
            float spawnScale = Random.Range(_MinSpawnScale, _MaxSpawnScale);

            //Spawn the obstacle.
            _ObstaclePool.ActivatePoolObject(spawnPos, spawnRot, new Vector3(spawnScale, spawnScale, spawnScale));
        }
    }
}
