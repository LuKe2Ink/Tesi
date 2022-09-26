using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dirPath, string fileName)
    {
        this.dataDirPath = dirPath;
        this.dataFileName = fileName;
    }

    public GameData Load()
    {
        /*dato che i sistemi operativi usano separatori diversi, al posto di concatenare la
        stringa con '/' si usa questa funzione*/
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //caricamento dei dati serializzati dal file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserializzazione dei dati da Json a GameData object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.Log("Errore nella lettura dei file di gioco in: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        /*dato che i sistemi operativi usano separatori diversi, al posto di concatenare la
        stringa con '/' si usa questa funzione*/
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //creazione della directory nel caso non esista già nel computer
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serializzazione del GameData object in Json
            string dataToStore = JsonUtility.ToJson(data, true);

            //scrittura della serializzazione sul file
            //usiamo using perché si è sicuri che una volta finita la lettura o scrittura il file sarà chiuso
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("Errore nella scrittura dei file di gioco in: " + fullPath +"\n"+e);
        }
    }
}
