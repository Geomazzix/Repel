using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{
    [System.Serializable]
    public struct LevelTiles
    {
        public LevelTilesHolder[] DiffTiles;
    }


    /*
     Contains all the different leveltile arrays, puts them into queues and tells them to move whenever the ExtendLevel function is called.
    */
    public sealed class LevelBuilder : MonoBehaviour
    {
        [SerializeField]
        private LevelExtendTrigger _Trigger;

        [SerializeField]
        private LevelTiles[] _LevelTileArrays;
        private Queue<LevelTilesHolder>[] _LevelTilesQueues;


        //Fill the queues.
        private void Awake()
        {
            //Initliaze the queues.
            _LevelTilesQueues = new Queue<LevelTilesHolder>[3];

            //Fill the queues with the starting objects.
            int levelTileQueuesLength = _LevelTilesQueues.Length;
            for (int i = 0; i < levelTileQueuesLength; i++)
            {
                _LevelTilesQueues[i] = new Queue<LevelTilesHolder>();
                StartCoroutine(FillQueues(_LevelTilesQueues[i], _LevelTileArrays[i].DiffTiles));
            }
        }

        
        //Extends the level by choosing the leveltile which the parameter defines and placing that on the new position.
        public void ExtendLevel(int levelTileIndex)
        {
            //Move the trigger.
            _Trigger.MoveTrigger();

            //Extend the level.
            for (int i = 0; i < _LevelTilesQueues.Length; i++)
            {
                LevelTilesHolder levelTile = _LevelTilesQueues[i].Peek();
                MoveTile(levelTileIndex, _LevelTilesQueues[i], levelTile);
            }
        }


        //Move the requested tile and choose it's type of leveltile.
        private void MoveTile(int levelTileIndex, Queue<LevelTilesHolder> levelTileQueue, LevelTilesHolder levelTile)
        {
            levelTileQueue.Dequeue();
            levelTile.SelectAndEnableLevelTile(levelTileIndex);
            levelTile.MoveTiles();
            levelTileQueue.Enqueue(levelTile);
        }

        
        //Fills the given queue with the given array objects.
        private IEnumerator FillQueues(Queue<LevelTilesHolder> levelTileQueue, LevelTilesHolder[] levelTileArray)
        {
            int levelTileArraysSize = levelTileArray.Length;
            for (int i = 0; i < levelTileArraysSize; i++)
            {
                levelTileQueue.Enqueue(levelTileArray[i]);
            }

            yield return null;
        }
    }
}