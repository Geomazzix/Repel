using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Repel
{
    public sealed class LoadingSceneAsync : MonoBehaviour
    {
        [SerializeField]
        private Slider _LoadingBar;

        [SerializeField]
        private Text _LoadingPercentageText;


        //Gets called when the game starts.
        public void Awake()
        {
            StartCoroutine(LoadSceneAsync("GameScene"));
        }


        //Loading the scene.
        private IEnumerator LoadSceneAsync(string levelName)
        {
            AsyncOperation scene = SceneManager.LoadSceneAsync(levelName);

            float loadingProgress;
            while (!scene.isDone)
            {
                loadingProgress = scene.progress / 0.9f;
                _LoadingBar.value = loadingProgress;
                _LoadingPercentageText.text = (loadingProgress * 100).ToString();

                yield return null;
            }
        }
    }

}