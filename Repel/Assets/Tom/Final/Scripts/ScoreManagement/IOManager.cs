using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Repel
{
    public class IOManager : MonoBehaviour
    {
        [SerializeField]
        private string _LocalScoreFilePath;

        private int _LocalScoreLength;
        private List<int> _LocalHighScores = new List<int>();
        private string _FilePath;
        private int _PlayerScore;


        //Call all the functions which should be handled when the game starts.
        private void Awake()
        {
            //Make sure not destroy this gameobject when sceneloading.
            DontDestroyOnLoad(gameObject);

            _FilePath = @Application.dataPath + "\\" + _LocalScoreFilePath + ".LTPS";

            //At the start of the look if the localhighscore file exists, if so continue else create one.
            if (!File.Exists(_FilePath))
            {
                File.Create(_FilePath);
            }
            else
            {
                //Get the list if the localhighscore file did exist.
                _LocalHighScores = ReadLocalHighscoreList();
            }
        }
    

        //Gets the length of the maximum amount of scores inside the score file.
        private List<int> ReadLocalHighscoreList()
        {
            List<int> localHighScores = new List<int>();

            using (BinaryReader breader = new BinaryReader(File.Open(_FilePath, FileMode.Open)))
            {
                int pos = 0;
                int fileLength = (int)breader.BaseStream.Length;
                while(pos < fileLength)
                {
                    localHighScores.Add(breader.ReadInt32());
                    pos += sizeof(int);
                }
            }

            return localHighScores;
        }


        //Writes content away in a file, returns true with success returns false when something went wrong.
        public void SetLocalHighscoreInList(int playerScore)
        {
            //Make sure to save the playerscore of the last run.
            _PlayerScore = playerScore;

            int localHighscoresLength = _LocalHighScores.Count;
            if (localHighscoresLength <= 0)
            {
                _LocalHighScores.Add(playerScore);
            }
            else if(localHighscoresLength > 0)
            {
                SetScoreInHighscoreListAndRemoveLastIndex(_LocalHighScores);
            }

            //Make sure to add the changes to the file.
            OverrideOldFile();
        }


        //Check if the playerscore is high enough to reach the highscore list.
        private List<int> SetScoreInHighscoreListAndRemoveLastIndex(List<int> localHighscores)
        {
            //Check if the highscore is even supposed to be in the list.
            if(_PlayerScore > localHighscores[localHighscores.Count - 1])
            {
                //Add the last highscore to the list.
                localHighscores.Add(_PlayerScore);

                int localHighscoresLength = localHighscores.Count;
                for (int x = 0; x < localHighscoresLength - 1; x++)
                {
                    for (int y = 0; y < localHighscoresLength - 1; y++)
                    {
                        //The highscorelist goes from localhighscores [0] being the highest, localhighscores.length being the lowest.
                        if (localHighscores[y] < localHighscores[y + 1])
                        {
                            int currScore = localHighscores[y];
                            localHighscores[y] = localHighscores[y + 1];
                            localHighscores[y + 1] = currScore;
                        }
                    }
                }


                //Make sure to check if the list gets to long, if so remove the lowest highscore.
                if (localHighscoresLength > 10)
                {
                    localHighscores.RemoveAt(localHighscoresLength - 1);
                }
            }

            return localHighscores;
        }


        //Writes the new highscore in the file.
        private void OverrideOldFile()
        {
            using (BinaryWriter bwriter = new BinaryWriter(File.Open(_FilePath, FileMode.Open)))
            {
                //Write the highscorelist.
                int localHighscoresLength = _LocalHighScores.Count;
                for (int i = 0; i < localHighscoresLength; i++)
                {
                    bwriter.Write(_LocalHighScores[i]);
                }
            }
        }


        //Returns the localhighscore, but when there is none yet, just return -1.
        public int GetPlayerHighScore()
        {
            return _LocalHighScores[0];
        }


        //The playerscore.
        public int GetPlayerScore()
        {
            return _PlayerScore;
        }
    }
}