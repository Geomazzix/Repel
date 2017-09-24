using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Repel
{
    public class ReloadScene : MonoBehaviour
    {
        private string _SceneName;
        private Button _UIButton;
        private GameManager _GameManager;

        [SerializeField]
        private PlayerRunManager _PlayerRunManager;


        //Search for the gamemanager and add the function to the delegate.
        private void Awake()
        {
            _UIButton = GetComponent<Button>();
            _GameManager = FindObjectOfType<GameManager>();

            _UIButton.onClick.AddListener(CallGameManagerForSceneReload);

            Scene scene = SceneManager.GetActiveScene();
            _SceneName = scene.name;
        }


        //Call the gamemanager for a scene reload.
        private void CallGameManagerForSceneReload()
        {
            //Make sure to call this because the fadeout won't work when the timescale is 0.
            _PlayerRunManager.ResumeGame();

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