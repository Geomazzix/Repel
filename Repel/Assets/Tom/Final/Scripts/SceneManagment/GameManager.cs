using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Repel
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        private string _FirstScene;

        [SerializeField]
        private Image _FadeOutImageOverlay;


        //Make sure this object is static.
        private void Awake()
        {
            StartCoroutine(LoadSceneAsyncWithFadeout("MainMenu", _FadeOutImageOverlay));
        }


        //Loads the requested scene.
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }


        //Loads the requested scene anysc, with loading bar.
        public void LoadSceneASync(string sceneName)
        {
            StartCoroutine(LoadSceneAsyncWithFadeout(sceneName, _FadeOutImageOverlay));
        }


        //Loading the scene.
        private IEnumerator LoadSceneAsyncWithFadeout(string levelName, Image fadeOutOverlay)
        {
            AsyncOperation scene = SceneManager.LoadSceneAsync(levelName);
            while (!scene.isDone)
            {
                fadeOutOverlay.color = new Color(1,1,1, -scene.progress / 0.9f);

                yield return null;
            }
        }
    }
}
