using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuManager : IMenuManager
    {
        #region Inspector.
        [SerializeField]
        private GameObject _Visuals;

        [SerializeField]
        private float _MenuFadeSpeed;

        [SerializeField]
        private CanvasGroup _CanvasGroup;
        #endregion


        //Make sure to set the canvasgroups alpha to 0, so it would not be visible when the game starts.
        private void Awake()
        {
            _CanvasGroup.alpha = 0f;
        }


        //Enables the menu visuals.
        public override void EnableVisuals()
        {
            _Visuals.SetActive(true);
            StartCoroutine(FadeMenu(_CanvasGroup, 1, _MenuFadeSpeed));
        }


        //Disables the menu visuals.
        public override void DisableVisuals()
        {
            StartCoroutine(FadeMenu(_CanvasGroup , -1, _MenuFadeSpeed));
        }


        //Fades a menu in or out, depending on the direction.
        private IEnumerator FadeMenu(CanvasGroup canvasGroup, int direction, float fadeSpeed)
        {
            bool fadeMenuLoop = true;
            while (fadeMenuLoop)
            {
                if ((direction == 1) && (canvasGroup.alpha >= 1))
                {
                    fadeMenuLoop = false;
                }
                else if ((direction == -1) && (canvasGroup.alpha <= 0))
                {
                    _Visuals.SetActive(false);
                    fadeMenuLoop = false;
                }

                //Apply the changed values to the new alpha.
                canvasGroup.alpha += direction * fadeSpeed * Time.deltaTime;

                yield return null;
            }
        }
    }
}