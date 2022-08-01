using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;



    private PlayerData playerData;
    private List<InterfaceDataPersistance> dataPersistanceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Only one DataPersistenceManager should exist. Destroying the newest one");
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
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
        this.playerData = dataHandler.Load();

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

        dataHandler.Save(playerData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
