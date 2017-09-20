using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    IOManager: Writes or reads files from the local files. This is only being used when the games closes so all 
    the scores don't get written away.
*/

public class IOManager : MonoBehaviour
{
    //Make sure that this object can always be called.
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    //Creates a file at a given location with a given name.
    public void CreateFile()
    {

    }

    
    //Deletes a file at a given location with a given name.
    public void DeleteFile()
    {

    }


    //Reads a file's content and returns that.
    public string ReadFile()
    {
        return "";
    }


    //Writes content away in a file, returns true with success returns false when something went wrong.
    public bool WriteFile(string content)
    {
        return false;
    }
}
