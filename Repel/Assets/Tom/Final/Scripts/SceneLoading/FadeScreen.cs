using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Repel
{
    public sealed class FadeScreen : MonoBehaviour
    {
        [SerializeField]
        private Image _FadeOverlay;

        [SerializeField]
        private Color _FadeColor;

        [SerializeField]
        private float _FadeSpeed;

        [SerializeField]
        private float _StartingFadeValue;


        private float _FadeValue;


        //When the scene gets loaded in for the first make sure it does have a fade in.
        private void Awake()
        {
            StartFadeIn();
        }


        //Call for a fade-in.
        public void StartFadeIn()
        {
            _FadeOverlay.gameObject.SetActive(true);
            _FadeValue = _StartingFadeValue;
            StartCoroutine(FadeCoroutine(-1, 1f, _FadeSpeed, _FadeOverlay, _FadeColor));
        }


        //Call for a fade-out.
        public void StartFadeOut()
        {
            _FadeOverlay.gameObject.SetActive(true);
            _FadeValue = _StartingFadeValue;
            StartCoroutine(FadeCoroutine(1, 0f, _FadeSpeed, _FadeOverlay, _FadeColor));
        }


        private IEnumerator FadeCoroutine(int direction, float fadeValueStart, float fadeSpeed, Image fadeOverlay, Color fadeColor)
        {
            float fadeValue = fadeValueStart;
            bool fading = true;
            while (fading)
            {
                fadeValue += direction * fadeSpeed * Time.deltaTime;
                fadeOverlay.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, fadeValue);

                //Check when to stop the coroutine.
                if ((direction == -1) && (fadeValue <= 0))
                {
                    fadeOverlay.gameObject.SetActive(false);
                    fading = false;
                }
                else if ((direction == 1) && (fadeValue >= 1))
                {
                    direction = -1;
                }

                //I use the WaitForEndOfFrame here so the Fade IEnumerator functions as a second update, which can use the Time.deltatime.
                yield return null;
            }
        }
    }
}