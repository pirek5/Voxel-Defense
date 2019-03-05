using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        [SerializeField] private InputField inputField;

        public void OnPlayPressed()
        {
            LevelLoader.instance.LoadNextLevel();
            GameMenu.Open();
        }

        public void OnTutorialPressed()
        {
            LevelLoader.instance.LoadLevel(2);
            GameMenu.Open();
        }

        public void OnHighscorePressed()
        {
            HighscoreMenu.Open();
        }

        public void OnCreditsPressed()
        {
            CreditsMenu.Open();
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}
