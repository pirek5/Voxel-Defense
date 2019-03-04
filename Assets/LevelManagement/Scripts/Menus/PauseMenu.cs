using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class PauseMenu : Menu<PauseMenu>
    {
        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                OnResumePressed();
            }
        }

        private void OnEnable()
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.OnOffUi();
            }
            else if(TutorialController.Instance != null)
            {
                TutorialController.Instance.OnOffUi();
            }
        }

        public void OnResumePressed()
        {
            Time.timeScale = 1;
            base.OnBackPressed();
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnOffUi();
            }
            else if (TutorialController.Instance != null)
            {
                TutorialController.Instance.OnOffUi();
            }
        }

        public void OnRestartPressed()
        {
            if (GameManager.Instance != null)
            {
                Time.timeScale = 1;
                LevelLoader.instance.ReloadLevel();
                base.OnBackPressed();
            }
        }

        public void OnMainMenuPressed()
        {
            Time.timeScale = 1;
            LevelLoader.instance.LoadMainMenuLevel();
            MainMenu.Open();
        }

        public void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}


