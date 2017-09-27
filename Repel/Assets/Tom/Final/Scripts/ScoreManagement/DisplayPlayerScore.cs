using UnityEngine;
using TMPro;

namespace Repel
{
    public sealed class DisplayPlayerScore : MonoBehaviour
    {
        [Header("The text.")]
        [SerializeField]
        private TextMeshProUGUI _TextMeshText;

        [Header("The countspeed")]
        [SerializeField]
        private float _CountSpeed;

        private float _PlayerScore;
        private bool _ScoreReached = false;
        private float _Text;


        //Make sure to get the playerScore.
        private void Awake()
        {
            IOManager IOManager = FindObjectOfType<IOManager>();
            _PlayerScore = IOManager.GetPlayerScore();
        }


        //Update the score.
        private void Update()
        {
            if (!_ScoreReached)
            {
                _Text += _CountSpeed;
                _TextMeshText.text = _Text.ToString();
                if ((_Text >= _PlayerScore))
                {
                    _ScoreReached = true;
                }
            }
        }
    }
}