using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerRegister : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerName; //Intent to allow the player to manually enter each dice number

    private void Awake()
    {
    }


    public void StartGame()
    {

        PlayerPrefs.SetString("PlayerName", playerName.text);
        SceneManager.LoadScene("DiceRollingGame");
    }
}
