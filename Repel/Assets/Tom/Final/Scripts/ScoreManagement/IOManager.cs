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


        //Call all the functions which should be handled when the game starts.
        private void Awake()
        {
            //At the start of the look if the localhighscore file exists, if so continue else create one.
            if (!File.Exists(_LocalScoreFilePath))
            {
                File.Create(@Application .dataPath + "\\" + _LocalScoreFilePath + ".LTPS");
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

            using (BinaryReader breader = new BinaryReader(File.Open(_LocalScoreFilePath, FileMode.Open)))
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
            int localHighscoresLength = _LocalHighScores.Count;
            if (_LocalHighScores == null)
            {
                _LocalHighScores.Add(playerScore);
            }
            else
            {
                SetScoreInHighscoreListAndRemoveLastIndex(_LocalHighScores, playerScore);
            }

            //Make sure to add the changes to the file.
            OverrideOldFile();
        }


        //Check if the playerscore is high enough to reach the highscore list.
        private List<int> SetScoreInHighscoreListAndRemoveLastIndex(List<int> localHighscores, int playerScore)
        {
            //Check if the highscore is even supposed to be in the list.
            if(playerScore > localHighscores[localHighscores.Count - 1])
            {
                //Add the last highscore to the list.
                localHighscores.Add(playerScore);

                int localHighscoresLength = localHighscores.Count;
                for (int i = 0; i < localHighscoresLength; i++)
                {
                    //The highscorelist goes from localhighscores [0] being the highest, localhighscores.length being the lowest.
                    if (localHighscores[i] < localHighscores[i + 1])
                    {
                        int currScore = localHighscores[i];
                        localHighscores[i] = localHighscores[i + 1];
                        localHighscores[i + 1] = currScore;
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
            using (BinaryWriter bwriter = new BinaryWriter(File.Open(_LocalScoreFilePath, FileMode.Open)))
            {
                int localHighscoresLength = _LocalHighScores.Count;
                for (int i = 0; i < localHighscoresLength; i++)
                {
                    bwriter.Write(_LocalHighScores[i]);
                }
            }
        }
    }
}