using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement;

public class OnlineHighscoreManager : MonoBehaviour {
    
    //config
    [SerializeField] private string addScoreURL = "http://localhost/highscore/addscore.php";
    [SerializeField] private string highscoreURL = "http://localhost/highscore/highscoresdata.php";
    private string secretKey = "123456789";

    //singleton
    private static OnlineHighscoreManager instance;
    public static OnlineHighscoreManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GetScores();
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }


    public void PostRandomScore() //debug only
    {
        int randomscore = (int)Random.Range(1.0f, 10.0f);
        PostScores("Maxim RZACK", randomscore);
        
    }

    public void GetScores()
    {
        StartCoroutine(GetScoresCoroutine());
    }

    public void PostScores(string name, int score)
    {
        StartCoroutine(PostScoresCoroutine(name, score));
    }

    private IEnumerator PostScoresCoroutine(string name, int score)
    {
        string hash = Md5Sum(name + score + secretKey);
        WWWForm form = new WWWForm();
        form.AddField("namePost", name);
        form.AddField("scorePost", score);
        form.AddField("hashPost", hash);

        WWW sentScore = new WWW(addScoreURL, form);

        if (LoseScreen.Instance != null)
        {
            LoseScreen.Instance.Connecting(true); //enable refresh icon
        }

        yield return sentScore;

        if (LoseScreen.Instance != null)
        {
            LoseScreen.Instance.Connecting(false); //disable refresh icon
        }
        if (sentScore.error != null)
        {
            if (LoseScreen.Instance != null)
            {
                LoseScreen.Instance.ConnectionError();
            }
            print("There was an error sending the highscores: " + sentScore.error);
        }
        else
        {
            if (LoseScreen.Instance != null && GameManager.Instance != null)
            {
                LoseScreen.Instance.ShowPostScoreWindow(false);
                LoseScreen.Instance.postScoreButton.SetActive(false);
                GameManager.Instance.ScoreSent = true;
            }
        }
    }

    //gets the score, cached it as a string in HighscoreMenu class and show it in HighscoreMenu.
    private IEnumerator GetScoresCoroutine()
    {
        WWW wwwHighscores = new WWW(highscoreURL);
        if (HighscoreMenu.Instance != null)
        {
            HighscoreMenu.Instance.Connecting(true); //enable refresh icon
        }

        yield return wwwHighscores;

        if (HighscoreMenu.Instance != null) // disable refresh icon
        {
            HighscoreMenu.Instance.Connecting(false);
        }

        if (wwwHighscores.error != null)
        {
            if (HighscoreMenu.Instance != null)
            {
                HighscoreMenu.Instance.ConnectionError();
            }
            Debug.Log("There was an error getting the high score: " + wwwHighscores.error);
        }
        else
        {
            if(HighscoreMenu.Instance != null)
            {
                HighscoreMenu.Instance.highscores = wwwHighscores.text;
                HighscoreMenu.Instance.ShowHighscore();
            }
        }
    }
    
 
    public string Md5Sum(string strToEncrypt) // This is used to create a md5sum - php script checks that only legit scores are submitted.
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
