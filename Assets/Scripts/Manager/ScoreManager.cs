using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;

    public static ScoreManager Instance
    {
        get => _instance;
    }

    public int score;
    private Text scoreText;

    void Awake()
    {
        MakeSingleton();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    void Start()
    {
        scoreText.text = score.ToString();
    }

    void Update()
    {
        if (scoreText == null) // Sahne yenilendiğinde referans Text yok olduysa;
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
    }

    void MakeSingleton()
    {
        if(_instance != null) Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance); // instance referanslı objeyi sahne yenilendiğinde yok etme
        }
    }

    public void IncreaseScore(int scoreNo)
    {
        score += scoreNo;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore",score);
        }
        scoreText.text = score.ToString();
        //Debug.Log(score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
