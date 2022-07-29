using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private PlayerData playerData;
    private List<InterfaceDataPersistance> dataPersistanceObjects;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Error. Only one DataPersistenceManager should exist.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataPersistanceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private List<InterfaceDataPersistance> FindAllDataPersistenceObjects()
    {
        IEnumerable<InterfaceDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<InterfaceDataPersistance>();

        return new List<InterfaceDataPersistance>(dataPersistanceObjects);
    }

    public void NewGame()
    {
        this.playerData = new PlayerData();
    }
    public void LoadGame()
    {
        if(this.playerData == null)
        {
            Debug.Log("No data was found. Initializing data to default");
            NewGame();
        }

        foreach(InterfaceDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(playerData);
        }

        Debug.Log("Load Score count = " + playerData.scores);
    }
    public void SaveGame()
    {
        foreach (InterfaceDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref playerData);
        }

        Debug.Log("Save Score count = " + playerData.scores);

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
