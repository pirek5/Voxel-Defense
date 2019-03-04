using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        [SerializeField] private InputField inputField;
        private DataManager dataManager;

        protected override void Awake()
        {
            base.Awake();
            dataManager = FindObjectOfType<DataManager>();
        }

        protected override void Start()
        {
            base.Start();
            LoadData();
        }

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

        private void LoadData()
        {
            if(dataManager != null && inputField != null)
            {
                dataManager.Load();
            }       
        }

    }
}
