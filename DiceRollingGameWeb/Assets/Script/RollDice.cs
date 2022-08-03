using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RollDice : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI rerollValueText;
    [SerializeField] private TextMeshProUGUI playerNameText;

    [SerializeField] private TextMeshProUGUI[] dieResult = new TextMeshProUGUI[3];  //Intent to display result for individual die
    [SerializeField] private TMP_InputField cheatInput; //Intent to allow the player to manually enter each dice number

    private int[] diceMatching = new int[3];  // Intent to set up different condition after three dice are rolled
    public int currentScore = 0;
    int die;
    int diceRolledCount = 0;
    int rerollCount = 5;

    bool gameOver = false;
    enum AddScoreCondtion { twoDice, threeDice, straightDice, none };
    [SerializeField] private AddScoreCondtion asc;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject rollButton;
    [SerializeField] private GameObject gameOverText;

    [SerializeField] private GameObject triplePtText;
    [SerializeField] private GameObject doublePtText;
    [SerializeField] private GameObject straightPtText;
    [SerializeField] private GameObject reroll1Text;
    [SerializeField] private GameObject reroll2Text;
    [SerializeField] private GameObject highScoreButton;


    private void Start()
    {
        for (int x = 0; x < dieResult.Length; x++)
        {
            dieResult[x].text = "?";
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
    public void NextRoll()
    {
        for (int x = 0; x < dieResult.Length; x++)
        {
            dieResult[x].text = "?";
        }
        asc = AddScoreCondtion.none;
        rerollCount--;
        triplePtText.SetActive(false);
        doublePtText.SetActive(false);
        straightPtText.SetActive(false);
        reroll1Text.SetActive(false);
        reroll2Text.SetActive(false);
    }

 

    public int ScoreDistribution()
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
                straightPtText.SetActive(true);
                break;
            case AddScoreCondtion.twoDice:
                addScore += 200;
                doublePtText.SetActive(true);
                reroll1Text.SetActive(true);
                rerollCount++;
                break;
            case AddScoreCondtion.threeDice:
                addScore += 300;
                rerollCount = rerollCount + 2;
                triplePtText.SetActive(true);
                reroll2Text.SetActive(true);
                break;
            default:
                addScore += 0;
                break;
        }
        return addScore;
    }

    private void DisplayScore()
    {
        if (rerollCount <= 0 && gameOver == false) // When game is over
        {
            gameOverText.SetActive(true);
            highScoreButton.SetActive(true);
            restartButton.SetActive(false);
            rollButton.SetActive(false);
            gameOver = true;
            PlayerPrefs.SetInt("TotalScore", currentScore);
        }


        if (diceRolledCount >= 3) //When you can still reroll
        {
            currentScore += ScoreDistribution();
            diceRolledCount = 0;
            restartButton.SetActive(true);
        }

        playerNameText.text = PlayerPrefs.GetString("PlayerName");
        scoreText.text = currentScore.ToString();
        rerollValueText.text = rerollCount.ToString();
    }

    public void TitleScreenButton()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void HighScoreButton()
    {
 
        SceneManager.LoadScene("HighScore");

    }



}
