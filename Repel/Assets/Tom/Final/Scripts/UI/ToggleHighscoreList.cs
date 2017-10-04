using UnityEngine;

namespace Repel
{
    public sealed class ToggleHighscoreList : MonoBehaviour
    {
        [SerializeField]
        private IMenuManager[] _MenuManagers;


        //Sets all the values when the game starts.
        private void Start()
        {
            for (int i = 0; i < _MenuManagers.Length; i++)
            {
                _MenuManagers[i].DisableVisuals();
            }

            _MenuManagers[0].EnableVisuals();
        }


        //Gets called when the ingamemenu pause button gets pressed.
        public void SetActivateHighscoreList(int menuIndex)
        {
            //Disable all the menus and enable afterwards the pausemenu.
            for (int i = 0; i < _MenuManagers.Length; i++)
            {
                if (_MenuManagers[i] == _MenuManagers[menuIndex])
                {
                    _MenuManagers[i].EnableVisuals();
                }
                else if ((_MenuManagers[i].gameObject.activeInHierarchy) && (_MenuManagers[i] != _MenuManagers[menuIndex]))
                {
                    _MenuManagers[i].DisableVisuals();
                }
            }
        }
    }
}