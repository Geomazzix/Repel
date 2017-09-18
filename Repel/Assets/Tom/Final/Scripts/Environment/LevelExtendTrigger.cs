using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{
    public class LevelExtendTrigger : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _Player;

        [SerializeField]
        private LevelBuilder _LevelBuilder;

        [Tooltip("Keep 0 for default terrain.")]
        [SerializeField]
        private int _LevelTileIndex = 0;

        [SerializeField]
        private float _TriggerMoveGap;


        //Check whenever the player moves through the trigger, when so make sure to signal the levelbuilder of it.
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject == _Player.gameObject)
            {
                _LevelBuilder.ExtendLevel(_LevelTileIndex);
            }
        }


        //Gets called when the level extends, the trigger needs to move too because he is supposed to be always ahead of the player.
        public void MoveTrigger()
        {
            transform.position += new Vector3(0, 0, _TriggerMoveGap);
        }
    }
}