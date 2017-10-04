using System.Collections;
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

        private IEnumerator _FadeMenu;


        //Enables the menu visuals.
        public override void EnableVisuals()
        {
            _Visuals.SetActive(true);
            _FadeMenu = FadeMenu(_CanvasGroup, 1, _MenuFadeSpeed);
            StopAllCoroutines();
            StartCoroutine(_FadeMenu);
        }


        //Disables the menu visuals.
        public override void DisableVisuals()
        {
            _FadeMenu = FadeMenu(_CanvasGroup, -1, _MenuFadeSpeed);
            StopAllCoroutines();
            StartCoroutine(_FadeMenu);
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