using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{
    //Delegates.
    public delegate void UpdateGameStateDelegate(int gameState);


    /*
    *Summary: The Gamemanager handles the game state, he is capable of pausing the game and creating a new run for the player to try again.
    */
    public sealed class GameManager : MonoBehaviour
    {
        #region Inspector.
        [Tooltip("StartState of the game.")]
        [SerializeField]
        private string[] _GameState;
        #endregion

        #region Private class members
        private string _CurrGameState;
        public event UpdateGameStateDelegate UpdateGameStateEvent;
        #endregion


        //Changes the current gamestate.
        public void SetGameState(string gameState)
        {
            //Changes the current gameState locally.
            for (int i = 0; i < _GameState.Length; i++)
            {
                if(_GameState[i] == gameState)
                {
                    _CurrGameState = gameState;
                    InvokeUpdateGameStateEvent(i);
                }
            }
        }


        //Invokes the UpdateGameStateEvent.
        private void InvokeUpdateGameStateEvent(int gameStateIndex)
        {
            if(UpdateGameStateEvent != null)
            {
                UpdateGameStateEvent.Invoke(gameStateIndex);
            }
        }
    }
}
