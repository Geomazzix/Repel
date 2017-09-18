using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repel
{
    public sealed class UIManager : MonoBehaviour
    {
        #region Inspector
        [SerializeField]
        private IMenuManager[] _MenuManagers;

        [SerializeField]
        private GameManager _GameManager;
        #endregion


        //Sets all the values when the game starts.
        private void Start()
        {
            for (int i = 0; i < _MenuManagers.Length; i++)
            {
                _MenuManagers[i].DisableVisuals();
            }

            _MenuManagers[0].EnableVisuals();
            _GameManager.UpdateGameStateEvent += EnableMenu;
        }


        //Gets called when the ingamemenu pause button gets pressed.
        public void SetActiveMenu(int menuIndex)
        {
            //Disable all the menus and enable afterwards the pausemenu.
            for (int i = 0; i < _MenuManagers.Length; i++)
            {
                if (_MenuManagers[i] == _MenuManagers[menuIndex])
                {
                    _MenuManagers[i].EnableVisuals();
                }
                else if((_MenuManagers[i].gameObject.activeInHierarchy) && (_MenuManagers[i] != _MenuManagers[menuIndex]))
                {
                    _MenuManagers[i].DisableVisuals();
                }
            }
        }


        //Gets called when the menu is supposed to change.
        public void EnableMenu(int menuIndex)
        {
            SetActiveMenu(menuIndex);
        }
    }
}