using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Repel
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerRunManager _PlayerRunManager;

        [SerializeField]
        private PlayerController _Player;

        private float _PlayerScore;
        private IOManager _IOManager;


        //Set subscription for the playerdeadevent, this can only be done with the playermanager, because this object HAS to be in the scene or the whole game won't work.
        private void Awake()
        {
            _PlayerRunManager.PlayerDeadEvent += GetPlayerScore;
            _IOManager = FindObjectOfType<IOManager>();
        }


        //When the player dies, get his score.
        private void GetPlayerScore()
        {
            _PlayerScore = _Player.Score;
            _IOManager.SetLocalHighscoreInList((int)_PlayerScore);
        }
    }
}