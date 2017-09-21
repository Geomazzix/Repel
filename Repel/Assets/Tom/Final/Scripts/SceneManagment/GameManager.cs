using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Repel
{
    public sealed class GameManager : Singleton
    {
        [SerializeField]
        private string _FirstScene;

        [SerializeField]
        private Image _FadeOverlay;

        [SerializeField]
        private float _FadeSpeed;

        private MenuManager[] _MenuManagers;


        private float _FadeValue = 0f;


        //Make sure this object is static.
        private void Awake()
        {
            StartCoroutine(LoadSceneAsync(_FirstScene));
            StartSceneFadeIn(_FirstScene);
        }


        //Loads the requested scene.
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }


        //Starts the screen fadeout.
        public void StartSceneFadeOut(string sceneName)
        {
            _FadeOverlay.gameObject.SetActive(true);
            _FadeValue = 0f;
            StartCoroutine(FadeCoroutine(1, _FadeSpeed, _FadeOverlay, sceneName));
        }


        //Starts the screen fadein.
        public void StartSceneFadeIn(string sceneName)
        {
            _FadeOverlay.gameObject.SetActive(true);
            _FadeValue = 1f;
            StartCoroutine(FadeCoroutine(-1, _FadeSpeed, _FadeOverlay, sceneName));
        }


        //Fades into the image's alpha value into the direction with the given fadespeed.
        private IEnumerator FadeCoroutine(int direction, float fadeSpeed, Image fadeOverlay, string sceneName)
        {
            //Transform t = null;
            //t.set
            bool fading = true;
            while (fading)
            {
                _FadeValue += direction * fadeSpeed * Time.deltaTime;
                fadeOverlay.color = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, _FadeValue);

                //Check when to stop the coroutine.
                if ((direction == -1) && (_FadeValue <= 0))
                {
                    fadeOverlay.gameObject.SetActive(false);
                    fading = false;
                }
                else if ((direction == 1) && (_FadeValue >= 1))
                {
                    StartCoroutine(LoadSceneAsync(sceneName));
                    direction = -1;
                }

                //I use the WaitForEndOfFrame here so the Fade IEnumerator functions as a second update, which can use the Time.deltatime.
                yield return null;
            }
        }


        //Loading the scene.
        private IEnumerator LoadSceneAsync(string levelName)
        {
            AsyncOperation scene = SceneManager.LoadSceneAsync(levelName);
            while ((!scene.isDone))
            {
                yield return null;
            }
        }
    }
}
