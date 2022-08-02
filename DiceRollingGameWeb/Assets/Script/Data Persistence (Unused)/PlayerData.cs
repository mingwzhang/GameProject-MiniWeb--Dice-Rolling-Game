using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int scores;

    public PlayerData()
    {
        this.playerName = "";
        this.scores = 0;
    }

}
