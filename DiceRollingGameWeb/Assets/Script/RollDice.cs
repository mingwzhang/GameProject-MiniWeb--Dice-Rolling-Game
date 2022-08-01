using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RollDice : MonoBehaviour, InterfaceDataPersistance
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI[] dieResult = new TextMeshProUGUI[3];  //Intent to display result for individual die
    [SerializeField] private TMP_InputField cheatInput; //Intent to allow the player to manually enter each dice number

    private int[] diceMatching = new int[3];  // Intent to set up different condition after three dice are rolled
    int currentScore = 0;
    int die;
    int diceRolledCount = 0;
    int reRollCount = 10;

    enum AddScoreCondtion { twoDice, threeDice, straightDice, none };
    [SerializeField] private AddScoreCondtion asc;
    [SerializeField] private GameObject restartButton;

    private void Start()
    {
        for (int x = 0; x < dieResult.Length; x++)
        {
            dieResult[x].text = "???";
        }
        asc = AddScoreCondtion.none;
    }
    private void Update()
    {
        DisplayScore();
    }

    public void RollDie()
    {
        die = Random.Range(1, 7);
        if (diceRolledCount < 3)
        {
            dieResult[diceRolledCount].text = die.ToString();
            diceMatching[diceRolledCount] = die;
            diceRolledCount++;
        }
    }
    public void CheatInput()
    {
        if (diceRolledCount < 3)
        {
            dieResult[diceRolledCount].text = cheatInput.text;
            diceMatching[diceRolledCount] = int.Parse(cheatInput.text);
            diceRolledCount++;
        }
    }
    public void Retry()
    {
        for (int x = 0; x < dieResult.Length; x++)
        {
            dieResult[x].text = "???";
        }
        asc = AddScoreCondtion.none;
    }


    private int ScoreDistribution()
    {
        int addScore = 0;
        if (diceMatching[0] == 1 || diceMatching[0] == 2)   //Add 100 score if the dice pattern is 123, 234
        {
            if ((diceMatching[1] == diceMatching[0] + 1) && (diceMatching[2] == diceMatching[1] + 1))
                asc = AddScoreCondtion.straightDice;
        }
        else if (diceMatching[0] == 3 || diceMatching[0] == 4) //Add 100 score if the dice pattern is 345, 456, 432, 321
        {
            if ((diceMatching[1] == diceMatching[0] + 1) && (diceMatching[2] == diceMatching[1] + 1))
                asc = AddScoreCondtion.straightDice;
            if ((diceMatching[1] == diceMatching[0] - 1) && (diceMatching[2] == diceMatching[1] - 1))
                asc = AddScoreCondtion.straightDice;
        }
        else if (diceMatching[0] == 5 || diceMatching[0] == 6) //Add 100 score if the dice pattern is 543, 654
        {
            if ((diceMatching[1] == diceMatching[0] - 1) && (diceMatching[2] == diceMatching[1] - 1))
                asc = AddScoreCondtion.straightDice;
        }

        if (diceMatching[0] == diceMatching[1] || diceMatching[1] == diceMatching[2] || diceMatching[0] == diceMatching[2]) //Add 200 score if two of the dice have same number
        {
            asc = AddScoreCondtion.twoDice;
        }

        if (diceMatching[0] == diceMatching[1] && diceMatching[1] == diceMatching[2]) //Add 300 score if all dice have same number
        {
            asc = AddScoreCondtion.threeDice;
        }

        switch (asc)
        {
            case AddScoreCondtion.straightDice:
                addScore += 500;
                Debug.Log("straight dice");

                break;
            case AddScoreCondtion.twoDice:
                addScore += 200;
                Debug.Log("double dice");
                break;
            case AddScoreCondtion.threeDice:
                addScore += 300;
                Debug.Log("tripple dice");

                break;
            default:
                addScore += 0;
                break;
        }
        return addScore;
    }

    private void DisplayScore()
    {
        if (diceRolledCount >= 3)
        {
            currentScore += ScoreDistribution();
            diceRolledCount = 0;
            restartButton.SetActive(true);
        }
        scoreText.text = currentScore.ToString();
    }

    public void LoadData(PlayerData data)
    {
        this.currentScore = data.scores;
        Debug.Log("Load");
    }

    public void SaveData(ref PlayerData data)
    {
        data.scores = this.currentScore;
        Debug.Log("Save");

    }
}
