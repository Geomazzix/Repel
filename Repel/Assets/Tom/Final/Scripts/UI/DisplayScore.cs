using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Repel
{
    public class DisplayScore : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _PlayerController;

        [SerializeField]
        private TextMeshProUGUI _Textmesh; 

        private void Update()
        {
            _Textmesh.text = ((int)_PlayerController.Score).ToString();
        }
    }
}