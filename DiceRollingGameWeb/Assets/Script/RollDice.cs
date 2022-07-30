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

    int score = 0;
    int die;
    int diceCount = 0;

    private void Start()
    {
        for (int x = 0; x < dieResult.Length; x++)
        {
            dieResult[x].text = "???";
        }
       
        diceMatching[0] = 1;
        diceMatching[1] = 1;
        diceMatching[2] = 1;

      //Dice roll pattern check conditions:
      // 123, 234, 345, 456
      // 654, 543, 432, 321
      // 111 - 666

    }
    private void Update()
    {
        DisplayScore();
    }

    public void RollDie()
    {
        die = Random.Range(1, 7);
        if (diceCount <= 2)
        {
            dieResult[diceCount].text = die.ToString();
            diceMatching[diceCount] = die;
            diceCount++;
        }
    }

    public void CheatInput()
    {
        if (diceCount <= 2)
        {
            dieResult[diceCount].text = cheatInput.text;
            diceMatching[diceCount] = int.Parse(cheatInput.text);
            diceCount++;
        }
    }
    public void Retry()
    {
        SceneManager.LoadScene("DiceRollingGame");
    }

    private int ScoreDistribution()
    {
        int totalScore = 0;

        if(diceMatching[0] == 1 || diceMatching[0] == 2)   //Add 200 score if the dice pattern is 123, 234
        {
            if((diceMatching[1] == diceMatching[0] + 1) && (diceMatching[2] == diceMatching[1] + 1))
                totalScore += 200;
        }
        else if (diceMatching[0] == 3 || diceMatching[0] == 4) //Add 200 score if the dice pattern is 345, 456, 432, 321
        {
            if ((diceMatching[1] == diceMatching[0] + 1) && (diceMatching[2] == diceMatching[1] + 1))
                totalScore += 200;
            if ((diceMatching[1] == diceMatching[0] - 1) && (diceMatching[2] == diceMatching[1] - 1))
                totalScore += 200;
        }
        else if (diceMatching[0] == 5 || diceMatching[0] == 6) //Add 200 score if the dice pattern is 543, 654
        {
            if ((diceMatching[1] == diceMatching[0] - 1) && (diceMatching[2] == diceMatching[1] - 1))
                totalScore += 200;
        }
        
        if (diceMatching[0] == diceMatching[1] && diceMatching[1] ==  diceMatching[2]) //Add 500 score if all dice have same number
        {
            totalScore += 500;
        }

        return totalScore;
    }

    private void DisplayScore()
    {
        scoreText.text = ScoreDistribution().ToString();
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
