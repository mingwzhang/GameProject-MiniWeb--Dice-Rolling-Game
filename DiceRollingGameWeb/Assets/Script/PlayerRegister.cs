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
        DontDestroyOnLoad(this.gameObject);
    }


    public void StartGame()
    {

        PlayerPrefs.SetString("Player Name", playerName.text);
        SceneManager.LoadScene("DiceRollingGame");
    }
}
