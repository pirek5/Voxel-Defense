using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace LevelManagement
{
    public class LoseScreen : Menu<LoseScreen>
    {
        //set in editor
        [SerializeField] private Text score;
        [SerializeField] private GameObject postScorePopUp;
        [SerializeField] public GameObject postScoreButton;
        [SerializeField] private Text enterYourNameText;
        [SerializeField] private InputField postScoreName;

        [SerializeField] private GameObject RefreshIcon;
        [SerializeField] private GameObject ConnectionErrorWindow;


        private void OnEnable()
        {
            SetScreenToDefault();
            CheckScore();
        }

        private void SetScreenToDefault()
        {
            ConnectionErrorWindow.SetActive(false);
            postScorePopUp.SetActive(false);
            RefreshIcon.SetActive(false);
            enterYourNameText.text = "ENTER YOUR NAME:"; 
            enterYourNameText.fontSize = 80; 
            if (GameManager.Instance != null)
            {
                postScoreButton.SetActive(!GameManager.Instance.ScoreSent); //show post score button if score wasnt send
            }
        }

        private void Update()
        {
            if (postScorePopUp.activeSelf)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    ShowPostScoreWindow(false);
                }
            }
        }

        private void CheckScore()
        {
            var scoreManager = ScoreManager.Instance;
            if (scoreManager != null)
            {
                score.text = scoreManager.score.ToString() + " ENEMIES";
            }
        }

        public void OnRestartPressed()
        {
            base.OnBackPressed();
            LevelLoader.instance.ReloadLevel();
        }

        public void OnMainMenuPressed()
        {
            LevelLoader.instance.LoadMainMenuLevel();
            MainMenu.Open();
        }

        public void ShowPostScoreWindow(bool show)
        {
            postScorePopUp.SetActive(show);
        }

        public void OnSubmitScorePressed()
        {
            string name = postScoreName.text;
            if (name.Length >= 3 && name.Length <= 16 && !name.Contains(";")  && !name.Contains(":"))
            {
                if(OnlineHighscoreManager.Instance != null && ScoreManager.Instance !=null)
                {
                    OnlineHighscoreManager.Instance.PostScores(name, ScoreManager.Instance.score);
                }
            }
            else
            {
                postScoreName.textComponent.color = Color.red;
                enterYourNameText.text = "Name must be 3 - 16 characters long without ' ; ' or ' : ' special marks";
                enterYourNameText.fontSize = 40; 
            }
        }

        public void OnCancelSubmitPressed()
        {
            ShowPostScoreWindow(false);
        }

        public void OnHighscorePressed()
        {
            HighscoreMenu.Open();
        }

        public void OnEditName()
        {
            postScoreName.textComponent.color = Color.black;
        }

        public void Connecting(bool isConnecting)
        {
            RefreshIcon.SetActive(isConnecting);
        }

        public void ConnectionError()
        {
            ConnectionErrorWindow.SetActive(true);
        }

        public void OnConnectionErrorClosePressed()
        {
            ConnectionErrorWindow.SetActive(false);
        }
    }
}
