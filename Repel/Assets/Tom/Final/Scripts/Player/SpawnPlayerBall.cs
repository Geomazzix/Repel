using UnityEngine;

namespace Repel
{
    public sealed class SpawnPlayerBall : MonoBehaviour
    {
        #region Inspector
        [Header("The spawnlayer of the ball.")]
        [Tooltip("This layer resembles the height of the pivot of the ball.")]
        [SerializeField]
        private LayerMask _SpawnLayer;

        [Header("BallGrowspeed when spawning him.")]
        [SerializeField]
        private float _GrowSpeed = 0.03f;

        [Header("Ball scale values.")]
        [SerializeField]
        private Vector3 _MinScale;
        [SerializeField]
        private Vector3 _MaxScale;

        [SerializeField]
        private PlayerRunManager _PlayerRunManager;

        [SerializeField]
        private PoolController _PoolController;
        #endregion

        #region private members
        private bool _MaySpawnPlayerBall;
        private GameObject _SpawningPlayerBall;
        #endregion


        //Set subscribtions.
        private void Awake()
        {
            _PlayerRunManager.InPlayerRunEvent += GiveSpawnPermission;
            _PlayerRunManager.PauseGameEvent += ResetSpawnPermission;
            _PlayerRunManager.ResumeGameEvent += GiveSpawnPermission;
        }


        //Gets called when the game starts.
        private void GiveSpawnPermission()
        {
            _MaySpawnPlayerBall = true;
        }



        public void ResetSpawnPermission()
        {
            _MaySpawnPlayerBall = false;
        }


        private void Update()
        {
            if (_MaySpawnPlayerBall)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _SpawnLayer))
                {
                    //Start spawning the ball.
                    if (Input.GetButtonDown("Fire1"))
                    {
                        _SpawningPlayerBall = _PoolController.ActivatePoolObject(hit.point, new Vector3(0,0,0), _MinScale);
                    }

                    //When the spawningPlayerball turns out to be null it means the pool was full.
                    if(_SpawningPlayerBall != null)
                    {
                        //When a ball is being spawned make sure to scale him.
                        if (Input.GetButton("Fire1"))
                        {
                            _SpawningPlayerBall.transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                            _SpawningPlayerBall.transform.localScale += new Vector3(_GrowSpeed, _GrowSpeed, _GrowSpeed);
                            _SpawningPlayerBall.transform.localScale = new Vector3(
                                Mathf.Clamp(_SpawningPlayerBall.transform.localScale.x, _MinScale.x, _MaxScale.x),
                                Mathf.Clamp(_SpawningPlayerBall.transform.localScale.y, _MinScale.y, _MaxScale.y),
                                Mathf.Clamp(_SpawningPlayerBall.transform.localScale.z, _MinScale.z, _MaxScale.z));
                            _SpawningPlayerBall.transform.rotation = Quaternion.Euler(-90, 0, 0);
                        }

                        //Make sure to reset the spawning ball.
                        if (Input.GetButtonUp("Fire1"))
                        {
                            //_SpawningPlayerBall.GetComponent<SphereCollider>().enabled = true;
                            //_SpawningPlayerBall.GetComponent<BoxCollider>().enabled = true;
                            _SpawningPlayerBall.tag = "PlayerBall";
                            _SpawningPlayerBall = null;
                        }
                    }
                }
            }
        }
    }
}

