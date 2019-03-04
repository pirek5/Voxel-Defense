using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelManagement
{
    public class HighscoreMenu : Menu<HighscoreMenu>
    {
        //set in editor
        [SerializeField] private Text[] highscoresName;
        [SerializeField] private Text[] highscoresScore;
        [SerializeField] private GameObject RefreshIcon;
        [SerializeField] private GameObject ConnectionErrorWindow;
        
        //cached
        [HideInInspector] public string highscores; //OnlineHighscoreManager send data here after succesful download data

        private void OnEnable()
        {
            ConnectionErrorWindow.SetActive(false); //default state

            if (highscores != "")// show last/offline highscores data in highscore menu
            {
                ShowHighscore();
            }

            if (OnlineHighscoreManager.Instance != null) 
            {
                OnlineHighscoreManager.Instance.GetScores(); //refresh highscores data, if succesful, call ShowHighscore() method again
            }
        }

        public void ShowHighscore()
        {
            string[] highscoresArray = highscores.Split(';');
            for (int i = 0; i < highscoresArray.Length && i < highscoresName.Length; i++)
            {
                string[] namePlusScoreArray = highscoresArray[i].Split(':');
                highscoresName[i].text = namePlusScoreArray[0];
                highscoresScore[i].text = namePlusScoreArray[1];
            }
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
