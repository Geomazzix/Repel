using UnityEngine;

namespace Repel
{
    public sealed class UIManager : MonoBehaviour
    {
        #region Inspector
        [SerializeField]
        private IMenuManager[] _MenuManagers;

        [SerializeField]
        private PlayerRunManager _PlayerRunManager;
        #endregion


        private void Awake()
        {
            _PlayerRunManager.InPlayerRunEvent += EnableIngameUI;
        }


        //Sets all the values when the game starts.
        private void Start()
        {
            for (int i = 0; i < _MenuManagers.Length; i++)
            {
                _MenuManagers[i].DisableVisuals();
            }
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


        //Enables the ingameUI.
        public void EnableIngameUI()
        {
            SetActiveMenu(1);
        }


        //Enables the pausemenu UI.
        public void EnableIngamePauseMenu()
        {
            SetActiveMenu(0);
        }
    }
}