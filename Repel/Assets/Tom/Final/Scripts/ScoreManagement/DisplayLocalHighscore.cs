using UnityEngine;
using TMPro;

namespace Repel
{
    public sealed class DisplayLocalHighscore : MonoBehaviour
    {
        [Header("The text.")]
        [SerializeField]
        private TextMeshProUGUI _TextMeshText;
        
        private int _LocalHighscore;


        private void Awake()
        {
            IOManager IOManager = FindObjectOfType<IOManager>();
            _LocalHighscore = IOManager.GetPlayerHighScore();
            _TextMeshText.text = _LocalHighscore.ToString();
        }
    }
}