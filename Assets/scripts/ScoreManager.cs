using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour {

    //set in editor
    [SerializeField] private Mesh[] digits = new Mesh[10];
    [SerializeField] private MeshFilter[] DigitsPlaces = new MeshFilter[4];

    //status
    public  int score = 0;

    //Singleton
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } set { instance = value; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void AddScore()
    {
        score++;
        ScoreToVoxelNumber();
    }

    private void ScoreToVoxelNumber()
    {
        string scoreString = score.ToString();
        int numberOfDigits = scoreString.Length;
        for (int i = 0; i < numberOfDigits;i++)
        {
            int index = Convert.ToInt32(scoreString[i].ToString()); //char to int
            DigitsPlaces[i+DigitsPlaces.Length-numberOfDigits].mesh = digits[index];
        }
    }
}
