using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submit : MonoBehaviour
{

    public TMPro.TextMeshProUGUI score;
    public TMPro.TextMeshProUGUI name;
    public int currentScore;

    // Update is called once per frame
    void Update()
    {
        score.text = $"SCORE: {PlayerPrefs.GetInt("highscore")}";        
    }

    public void SendScore()
    {
        if(currentScore > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", currentScore);
            HighScores.UploadScore(name.text, currentScore);
        }
    }
}
