using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
ScoreManager: Keeps track of all the player scores. Can be called to get highscores or to send scores to the highsore list. 
*/

public class ScoreManager : MonoBehaviour
{
    private float _PlayerScore;


    //Make sure this object does not get destroyed and can always be called for saving/getting scores.
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    //Gets the best score of the playing player.
    public float GetPlayerHighScore(float score)
    {
        return 10f;
    }

    
    //Sets the score of the player.
    public void SetScore(float score)
    {
        _PlayerScore = score;
    }
}
