using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void BeforePlayerRunDelegate();
public delegate void InPlayerRundDelegate();
public delegate void PlayerDeadDelegate();


public class PlayerRunManager : MonoBehaviour
{
    [SerializeField]
    private float _PauseGameSlomo;

    public event BeforePlayerRunDelegate BeforePlayerRunEvent;
    public event InPlayerRundDelegate InPlayerRunEvent;
    public event PlayerDeadDelegate PlayerDeadEvent;


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
    private void InvokeInPlayerRunEvent()
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
    private void InvokePlayerDeadEvent()
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
        Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, Time.deltaTime * _PauseGameSlomo);
    }


    //Resumes the game after it was paused.
    public void ResumeGame()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, Time.deltaTime * _PauseGameSlomo);
    }
}
