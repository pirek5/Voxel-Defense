using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement;


public class TutorialController : MonoBehaviour {
    //Set in Editor
    [SerializeField] private Text uiText;
    [SerializeField] private string[] tutorialTexts2;

    [SerializeField] private FlashingObject[] flashingObjects0; 
    [SerializeField] private FlashingObject[] flashingObjects1;
    [SerializeField] private FlashingObject[] flashingObjects2;
    [SerializeField] private FlashingObject[] flashingObjects3;
    [SerializeField] private FlashingObject[] flashingObjects4;
    [SerializeField] private FlashingObject[] flashingObjects5;
    [SerializeField] private FlashingObject[] flashingObjects6;

    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject backToMainMenu;

    [SerializeField] GameObject UIMenu;

    //state
    List<FlashingObject[]> allFlashingObjects = new List<FlashingObject[]>();
    int currentStep = 0;
    FlashingObject[] currentFlashingObjects;

    //singleton
    private static TutorialController instance;
    public static TutorialController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            var canvas = GetComponentInChildren<Canvas>();
            if (canvas != null)
            {
                canvas.worldCamera = UICamera.Instance.GetComponent<Camera>();
            }
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }


    // Use this for initialization
    void Start () {
        allFlashingObjects.Add(flashingObjects0);
        allFlashingObjects.Add(flashingObjects1);
        allFlashingObjects.Add(flashingObjects2);
        allFlashingObjects.Add(flashingObjects3);
        allFlashingObjects.Add(flashingObjects4);
        allFlashingObjects.Add(flashingObjects5);
        allFlashingObjects.Add(flashingObjects6);
        NextStep();
    }

    private void NextStep()
    {
        uiText.text = tutorialTexts2[currentStep];
        currentFlashingObjects = allFlashingObjects[currentStep];
        foreach(FlashingObject obj in currentFlashingObjects)
        {
            obj.StartFlashing();
        }
    }

    private void StopFlashingInCurrentStep()
    {
        foreach (FlashingObject obj in currentFlashingObjects)
        {
            obj.StopFlashing();
        }
        currentStep++;
    }

    public void OnNextButtonPressed()
    {

        StopFlashingInCurrentStep();
        NextStep();
        if (currentStep == tutorialTexts2.Length -1)
        {
            nextButton.SetActive(false);
            backToMainMenu.SetActive(true);
        }
    }

    public void OnBackToMainMenuPressed()
    {
        LevelLoader.instance.LoadMainMenuLevel();
        MainMenu.Open();
    }

    public void OnOffUi()
    {
        UIMenu.SetActive(!UIMenu.activeSelf);
    }

}
