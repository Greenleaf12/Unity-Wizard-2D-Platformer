using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;
    public static int score;
    public static int globalScore;
    public string pname = "New Player";

    void Awake()
    {
        pname = MainMenu.globalUserName;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        score = 0;
        TimerController.instance.BeginTimer();

        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeGold(int coinValue)
    {
        score += coinValue;

        text.text = "X  " + score.ToString();
    }

    public void ChangeScore(int scoreValue)
    {
        globalScore += scoreValue;
        text2.text = "Score = " + globalScore.ToString();
        //ChangeGlobalScore(globalScore);
    }

    public void ChangeGlobalScore(int scoreValue)
    {
        //text2.text = "Score  " + globalScore.ToString();
        FindObjectOfType<HighscoreTable2>().AddHighscoreEntry(globalScore, pname);
        //HighscoreTable2.AddHighscoreEntry(globalScore, "ROR");
    }
}
