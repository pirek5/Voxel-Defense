using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement.Data;
using UnityEngine.SceneManagement;


namespace LevelManagement
{
    public class WinScreen : Menu<WinScreen>
    {
        [SerializeField] private GameObject firstPage;
        [SerializeField] private GameObject secondPage;

        public void OnContinePressed()
        {
            SwitchPages();
        }

        public void OnNextLevelPressed()
        {
            SwitchPages();
            base.OnBackPressed();
            LevelLoader.instance.LoadNextLevel();
        }

        public void OnRestartPressed()
        {
            SwitchPages();
            base.OnBackPressed();
            LevelLoader.instance.ReloadLevel();
        }

        public void OnMainMenuPressed()
        {
            SwitchPages();
            LevelLoader.instance.LoadMainMenuLevel();
            MainMenu.Open();
        }

        private void SwitchPages()
        {
            firstPage.SetActive(!firstPage.activeSelf);
            secondPage.SetActive(!secondPage.activeSelf);
        }
    }
}


