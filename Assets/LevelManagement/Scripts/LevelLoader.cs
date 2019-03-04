using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        private static int mainMenuIndex = 0;
        private static int tutorialIndex = 2;
        public static LevelLoader instance;
        public static bool levelIsReady = false;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        public void LoadLevel(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                if (levelIndex == mainMenuIndex)
                {
                    MainMenu.Open();
                }
                StartCoroutine(LoadLevelAsync(levelIndex));
            }
            else
            {
                Debug.LogWarning("LEVELLOADER Loadlevel Error: invalid scene specified!");
            }
        }

        public void ReloadLevel()
        {
            StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex));
        }

        public void LoadNextLevel()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if(nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                LoadMainMenuLevel();
            }
            else
            {
                LoadLevel(nextSceneIndex);
            }
        }

        public void LoadMainMenuLevel()
        {
            LoadLevel(mainMenuIndex);
        }

        IEnumerator LoadLevelAsync(int level)
        {
            
            AsyncOperation async = SceneManager.LoadSceneAsync(level);
            LoadingIcon.instance.Init();
            while (!async.isDone)
            {
                yield return null;
            }
            levelIsReady = true;
        }

        public bool CheckIfMainMenu()
        {
            return (SceneManager.GetActiveScene().buildIndex == mainMenuIndex);
        }

        public bool CheckIfTutorial()
        {
            return (SceneManager.GetActiveScene().buildIndex == tutorialIndex);
        }
    }
}



