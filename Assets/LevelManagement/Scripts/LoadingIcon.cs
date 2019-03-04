using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement;


public class LoadingIcon : MonoBehaviour { //TODO refactor

    //set in editor
    [SerializeField] private Text loadingText;
    [SerializeField] private Text pressAnyKeyText;
    [SerializeField] private Image background;

    //config
    [SerializeField] private float period = 1;
    [SerializeField] private float brighteningTime;

    //state
    private string[] possibleTexts = { "LOADING","LOADING.", "LOADING..", "LOADING..." };

    //singleton
    public static LoadingIcon instance;

    private void Awake() 
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            gameObject.SetActive(false);
        }
        
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void Init()
    {
        gameObject.SetActive(true);
        pressAnyKeyText.gameObject.SetActive(false);
        StartCoroutine(LoadingCoroutine());
    }

    private IEnumerator LoadingCoroutine()
    {
        background.color = Color.black;
        int i = 0;
        while (!LevelLoader.levelIsReady)  // level loading
        {
            loadingText.text = possibleTexts[i];
            i++;
            if (i >= possibleTexts.Length)
            {
                i = 0;
            }
            yield return new WaitForSeconds(period);
        }
        StartCoroutine(BackgroundBrightening());

        loadingText.gameObject.SetActive(false);
        pressAnyKeyText.gameObject.SetActive(true);
        bool isStartPressed = false;
        while (!isStartPressed)  // waiting to press any key
        {
            isStartPressed = Input.anyKeyDown || LevelLoader.instance.CheckIfMainMenu() || LevelLoader.instance.CheckIfTutorial();
            yield return null;
        }
        LevelLoader.levelIsReady = false;
        loadingText.gameObject.SetActive(true);
        isStartPressed = false;
        gameObject.SetActive(false);
        if(GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
    }

    IEnumerator BackgroundBrightening()
    {
        float t = 0;
        while(t < brighteningTime)
        {
            t += Time.deltaTime;
            Color tempColor = background.color;
            tempColor.a = 1 - (t / brighteningTime);
            background.color = tempColor;
            yield return null;
        }
    }

}
