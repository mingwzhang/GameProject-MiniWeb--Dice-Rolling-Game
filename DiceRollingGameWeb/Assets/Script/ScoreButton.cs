using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreButton : MonoBehaviour, InterfaceDataPersistance
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Update()
    {
        DisplayScore();
    }
    public void AddPointClick()
    {
        score = score + 1;
    }

    public void MinusPointClick()
    {
        score = score - 1;
    }

    private void DisplayScore()
    {
        scoreText.text = score + "";
    }

    public void LoadData(PlayerData data)
    {
        this.score = data.scores;
    }

    public void SaveData(ref PlayerData data)
    {
        data.scores = this.score;

    }
}
