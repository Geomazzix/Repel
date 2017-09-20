using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NetworkManager: Connects to the database with highscores and gets/sets requested scores in it. 
*/

public class NetworkManager : MonoBehaviour {

    //Make sure this object does not get destroyed and can always be called for saving/getting scores.
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    //Sends a playerscore.
    public bool SendPlayerHighScore(string token, float score)
    {
        if (true)
        {
            return true;
        }

        return false;
    }


    //Gets the highest score archieved by some random player.
    public float GetWorldHighScore()
    {
        return 10f;
    }
}
