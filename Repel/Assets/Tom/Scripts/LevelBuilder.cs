using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject _Player;

    [SerializeField]
    private float _LevelBuilderGap;

    [Tooltip("Make sure this array is as long as the different tiles.")]
    [SerializeField]
    private float[] _GapDistances;

    [SerializeField]
    private GameObject[] _GroundTiles, _LeftWallTiles, _RightWallTiles;

    private Queue<GameObject> _GroundQ, _LeftWallQ, _RightWallQ;


    //Add all the objects to the queues.
    private void Start()
    {
        _GroundQ = new Queue<GameObject>();
        _LeftWallQ = new Queue<GameObject>();
        _RightWallQ = new Queue<GameObject>();

        EnqueueArray(_GroundQ, _GroundTiles);
        EnqueueArray(_LeftWallQ, _LeftWallTiles);
        EnqueueArray(_RightWallQ, _RightWallTiles);
    }


    //Enqueue all the tiles.
    private void EnqueueArray(Queue<GameObject> queue, GameObject[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            queue.Enqueue(array[i]);
        }
    }


    //Moves the tiles from under the camera to above the camera.
    private void MoveTiles()
    {
        MoveTile(_GroundQ, _GapDistances[0]);
        MoveTile(_LeftWallQ, _GapDistances[1]);
        MoveTile(_RightWallQ, _GapDistances[2]);
    }


    //Moves a tile.
    private void MoveTile(Queue<GameObject> queue, float gap)
    {
        //Move the ground.
        if(queue != null)
        {
            GameObject movingTile = queue.Peek();
            queue.Dequeue();
            movingTile.transform.position += new Vector3(0, 0, gap);
            queue.Enqueue(movingTile);
        }
        else
        {
            Debug.LogError("Given queue is empty");
        }
    }


    //Moves the levelbuilder.
    private void MoveLevelBuilder()
    {
        transform.position += new Vector3(0, 0, _LevelBuilderGap);
    }


    //Check if the object is part of the environment, if so, move the tiles 1 time.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _Player)
        {
            MoveTiles();
            MoveLevelBuilder();
        }
    }
}
