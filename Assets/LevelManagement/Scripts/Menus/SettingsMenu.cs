using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class SettingsMenu : Menu<SettingsMenu>
    {
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private GameObject confiramtionWindow;

        private DataManager dataManager;

        protected override void Awake()
        {
            base.Awake();

            dataManager = FindObjectOfType<DataManager>();
        }

        private void Start()
        {
            LoadData();
        }

        public void OnMasterVolumeChanged(float volume)
        {
            if (dataManager != null)
            {
                dataManager.MasterVolume = volume;
            }
        }

        public void OnSFXVolumeChanged(float volume)
        {
            if (dataManager != null)
            {
                dataManager.SFXVolume = volume;
            }
        }

        public void OnMusicVolumeChanged(float volume)
        {
            if (dataManager != null)
            {
                dataManager.MusicVolume = volume;
            }
        }

        public void OnResetPressed()
        {
            confiramtionWindow.SetActive(true);
            dataManager.Save();
        }

        public void OnResetYesPressed()
        {
            confiramtionWindow.SetActive(false);

            dataManager.Save();
        }

        public void OnResetCancelPressed()
        {
            confiramtionWindow.SetActive(false);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            if(dataManager != null)
            {
                dataManager.Save();
            }
        }

        public void LoadData()
        {
            if(dataManager != null && masterVolumeSlider !=null && sfxVolumeSlider != null && musicVolumeSlider != null)
            {
                dataManager.Load();
                masterVolumeSlider.value = dataManager.MasterVolume;
                sfxVolumeSlider.value = dataManager.SFXVolume;
                musicVolumeSlider.value = dataManager.MusicVolume;
            }
            
        }
    }

}

