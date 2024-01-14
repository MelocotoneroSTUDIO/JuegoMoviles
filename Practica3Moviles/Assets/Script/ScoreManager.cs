using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] EventManager eventManager;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI HighScoreText;
    [SerializeField] int Score = 0;
    [SerializeField] int HighScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        eventManager.OnDeath+=ResetScore;
        eventManager.OnScore+=AddScore;
        HighScore = PlayerPrefs.GetInt("HighScore");
        HighScoreText.text = $"HighScore: {HighScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScore() 
    {
        Score = 0;
        ScoreText.text = $"Score: {Score}";
    }

    public void AddScore() 
    {
        Debug.Log("Added 1");
        Score++;
        ScoreText.text = $"Score: {Score}";
        if (Score > HighScore) 
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore",HighScore);
            HighScoreText.text = $"HighScore: {HighScore}";
        }
    }
}
