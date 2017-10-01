using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Repel
{
    public class HighscoreUIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI[] _HighscoreListDisplay;

        int[] _TopHighscores;
        private IOManager _IOManager;

        private void Awake()
        {
            _IOManager = FindObjectOfType<IOManager>();
            _TopHighscores = new int[5];

            int length = _HighscoreListDisplay.Length;
            for (int i = 0; i < length; i++)
            {
                _HighscoreListDisplay[i].text = _IOManager.GetLocalScoreAtIndex(i).ToString();
            }
        }
    }
}