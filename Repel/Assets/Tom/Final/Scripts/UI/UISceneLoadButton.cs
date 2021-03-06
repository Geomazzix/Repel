﻿using UnityEngine;

namespace Repel
{
    public class UISceneLoadButton : MonoBehaviour
    {
        [SerializeField]
        private MenuManager _MenuManager;
        private GameManager _GameManager;


        //Make sure that when the scene loads in the menu fades in as well.
        private void Awake()
        {
            _GameManager = FindObjectOfType<GameManager>();
            _MenuManager.EnableVisuals();
        }


        //Loads in a new scene.
        public void LoadScene(string sceneName)
        {
            _GameManager.StartSceneFadeOut(sceneName);
        }
    }
}
