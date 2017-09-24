using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{
    public delegate void BeforePlayerRunDelegate();
    public delegate void InPlayerRundDelegate();
    public delegate void PlayerDeadDelegate();
    public delegate void PauseGameDelegate();
    public delegate void ResumeGameDelegate();


    public class PlayerRunManager : MonoBehaviour
    {
        [SerializeField]
        private UIManager _UIManager;

        [SerializeField]
        private float _PauseGameSlomo, ResumeGameSlomo;

        [SerializeField] private float pauseTransitionTime = 1f;
        [SerializeField] SpawnPlayerBall _SpawnPlayerBallController;

        public event BeforePlayerRunDelegate BeforePlayerRunEvent;
        public event InPlayerRundDelegate InPlayerRunEvent;
        public event PlayerDeadDelegate PlayerDeadEvent;
        public event PauseGameDelegate PauseGameEvent;
        public event ResumeGameDelegate ResumeGameEvent;


        //When the scene loads in make sure to start certain things. Do this in the start so the other objects can subscribe to it in the Awake function.
        private void Start()
        {
            if (BeforePlayerRunEvent != null)
            {
                BeforePlayerRunEvent.Invoke();
            }
            else
            {
                Debug.LogError("BeforePlayerRunEvent doesn't have any subscribers!");
            }
        }


        //Invokes the InPlayerRunEvent.
        public void InvokeInPlayerRunEvent()
        {
            if (InPlayerRunEvent != null)
            {
                InPlayerRunEvent.Invoke();
            }
            else
            {
                Debug.LogError("InvokeInPlayerRunEvent doesn't have any subscribers!");
            }
        }


        //Invokes the InPlayerRunEvent.
        public void InvokePlayerDeadEvent()
        {
            if (PlayerDeadEvent != null)
            {
                PlayerDeadEvent.Invoke();
            }
            else
            {
                Debug.LogError("PlayerDeadEvent doesn't have any subscribers!");
            }
        }


        //Pauses the game is it is not paused yet.
        public void PauseGame()
        {
            _UIManager.EnableIngamePauseMenu();
            if(PauseGameEvent != null)
            {
                PauseGameEvent.Invoke();
            }

            StartCoroutine(PauseGame(-1));
        }


        //Resumes the game after it was paused.
        public void ResumeGame()
        {
            _UIManager.EnableIngameUI();

            if (ResumeGameEvent!= null)
            {
                ResumeGameEvent.Invoke();
            }
            StartCoroutine(PauseGame(1));
        }


        //Pauses or resumes the game.
        public IEnumerator PauseGame(int dir)
        {
            float counter = 0f;
            while (counter < pauseTransitionTime)
            {
                counter += Time.unscaledDeltaTime;
                if (dir == 1)
                {
                    Time.timeScale = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(0f, pauseTransitionTime, counter));
                } else if (dir == -1 )
                {
                    Time.timeScale = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(0f, pauseTransitionTime, counter));
                }
                yield return null;
            }
        }
    }
}