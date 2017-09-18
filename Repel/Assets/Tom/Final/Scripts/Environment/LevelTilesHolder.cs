using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{ 
    /*
        Controlls one row of tiles. It is allowed to select and move these tiles because it serves as their parent pivot point. It also saves lots of performance this way.
    */
    public sealed class LevelTilesHolder : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _MoveToPoint;

        [SerializeField]
        private GameObject[] _Tiles;


        //Activates the selected leveltile.
        public void SelectAndEnableLevelTile(int tileIndex)
        {
            int tilesLength = _Tiles.Length;

            //Check if the given index is valud.
            if (tilesLength < tileIndex)
            {
                Debug.LogError("Array out of index, tiles length is smaller than the given index.");
            }
            else
            {
                for (int i = 0; i < tilesLength; i++)
                {
                    //Activate the requested tile else check if the tile is active, if so disable it.
                    if (_Tiles[i] == _Tiles[tileIndex])
                    {
                        _Tiles[i].gameObject.SetActive(true);
                    }
                    else if (_Tiles[i].gameObject.activeInHierarchy)
                    {
                        _Tiles[i].gameObject.SetActive(false);
                    }
                }
            }
        }


        //Moves the all the tiles.
        public void MoveTiles()
        {
            transform.position += _MoveToPoint;
        }
    }
}