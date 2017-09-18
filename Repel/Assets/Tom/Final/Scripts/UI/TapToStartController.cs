using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Repel
{
    public sealed class TapToStartController : IMenuManager
    {
        [SerializeField]
        private GameObject _Visuals;

        [SerializeField]
        private float _FadeSpeed, _MinFadeValue, _MaxFadeValue;

        [SerializeField]
        private int _StartingDirection;

        [SerializeField]
        private CanvasGroup _CanvasGroup;

        private bool _Loop = false;


        //When loaded in make sure the alpha is 0, just in case.
        private void Awake()
        {
            _CanvasGroup.alpha = 0f;
        }


        //When enabled make sure to flicker the screen.
        public override void EnableVisuals()
        {
            _Visuals.SetActive(true);
            StartCoroutine(FlickerMenu(_CanvasGroup, _StartingDirection, _FadeSpeed, _MinFadeValue, _MaxFadeValue));
        }


        //When the object disables make sure to stop the IEnumerator.
        public override void DisableVisuals()
        {
            _Loop = false;
        }


        //Fades a menu in or out, depending on the direction.
        private IEnumerator FlickerMenu(CanvasGroup canvasGroup, int startingDirection, float fadeSpeed, float minFadeValue, float maxFadeValue)
        {
            int direction = startingDirection;
            bool FlickerMenuLoop = true;
            _Loop = true;

            while (FlickerMenuLoop)
            {
                //Check if the flicking is still supposed to be looping. If not, change the fade direction to go down and stop the fade when it reaches 0.
                if (_Loop)
                {
                    if ((direction == 1) && (canvasGroup.alpha >= maxFadeValue) && (_Loop))
                    {
                        direction = -1;
                    }
                    else if ((direction == -1) && (canvasGroup.alpha <= minFadeValue) && (_Loop))
                    {
                        direction = 1;
                    }
                }
                else
                {
                    direction = -1;

                    //Change the direction and keep checking to end the fade.
                    if (canvasGroup.alpha <= 0)
                    {
                        _Visuals.SetActive(false);
                        FlickerMenuLoop = false;
                    }
                }

                //When the values changed apply them to the canvasgroup alpha.
                canvasGroup.alpha += direction * fadeSpeed * Time.deltaTime;

                yield return null;
            }
        }
    }
}