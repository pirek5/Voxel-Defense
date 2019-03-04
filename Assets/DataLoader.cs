using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour {  //TODO stara klasa raczej do wyrzucenia

    private string secretKey = "123456789";
    public string addScoreURL = "http://localhost/highscore/addscore.php";
    public string highscoreURL = "http://localhost/highscore/highscoresdata.php";
    public Text highscorelist;

    //We start by just getting the HighScores, this should be removed, when you are done setting up.
    void Start()
    {
        StartCoroutine(GetScores());
    }
    // This is for debugging purposes, you can run this when clicking
    // on a button, to see that scores are added. Remove when done setting up.
    public void PostRandomScore()
    {
        int randomscore = (int)Random.Range(15.0f, 400.0f);
        PostScores("tester", randomscore);
    }

    // This is for debugging purposes, you can run this when clicking on 
    // a button, to see the highscores that have been added. Remove when done setting up.
    public void GetTheScores()
    {
        StartCoroutine(GetScores());
    }


    //This is where we post 
    public void PostScores(string name, int score)
    {
        string hash = Md5Sum(name + score + secretKey);
        WWWForm form = new WWWForm();
        form.AddField("namePost", name);
        form.AddField("scorePost", score);
        form.AddField("hashPost", hash);
        WWW www = new WWW(addScoreURL, form);
    }
    //This co-rutine gets the score, and print it to a text UI element.
    IEnumerator GetScores()
    {
        WWW wwwHighscores = new WWW(highscoreURL);
        yield return wwwHighscores;
        if (wwwHighscores.error != null)
        {
            print("There was an error getting the high score: " + wwwHighscores.error);
        }
        else
        {
            highscorelist.text = wwwHighscores.text;
            
        }
    }
    // This is used to create a md5sum - so that we are sure that only legit scores are submitted.
    // We use this when we post the scores.
    // This should probably be placed in a seperate class. But isplaced here to make it simple to understand.
    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);
        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);
        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";
        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }
        return hashString.PadLeft(32, '0');
    }
}
