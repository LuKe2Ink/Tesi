using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistance> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("E' stato trovato più di un Data Persistance Manager.");
        }
        instance = this;
    }

    private void Start()
    {
        //Application.persistentDataPath restituisce la cartella standard per i dati persistenti per un progetto unity
        //Debug.Log(Application.persistentDataPath);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistancesObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        /*Prende i dati salvati dal un file tramite il data handler
        se i dati non posso essere caricati inizializzo un new game*/
        this.gameData = dataHandler.Load();

        if(this.gameData == null)
        {
            Debug.Log("Nessun dato trovato. Inizializzazione di un nuovo salvataggio");
            NewGame();
        }

        //inoltrate i dati presi a tutti gli script a cui servono
        foreach(IDataPersistance dataPersistanceObj in dataPersistenceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        //passa i dati agli altri script aggiornando così i loro dati
        foreach (IDataPersistance dataPersistanceObj in dataPersistenceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }

        //salvare i dati usando il data handler
        dataHandler.Save(gameData);
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistancesObjects()
    {
        IEnumerable<IDataPersistance> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistenceObjects);
    }
}
