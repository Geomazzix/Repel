using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Repel
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField]
        private string _SceneName;
        private Button _UIButton;
        private GameManager _GameManager;


        //Search for the gamemanager and add the function to the delegate.
        private void Awake()
        {
            _UIButton = GetComponent<Button>();
            _GameManager = FindObjectOfType<GameManager>();
            _UIButton.onClick.AddListener(CallGameManagerForSceneReload);

            Scene scene = SceneManager.GetActiveScene();
        }


        //Call the gamemanager for a scene reload.
        private void CallGameManagerForSceneReload()
        {
            //Call the gamemanager for the screenfadeout and the scene reload.
            if (_GameManager != null)
            {
                _GameManager.StartSceneFadeOut(_SceneName);
            }
            else
            {
                Debug.LogError("No GameManager found!");
            }
        }
    }
}