using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonObject : MonoBehaviour, IDataPersistance
{
    public static DungeonObject instance;
    public TextMeshProUGUI scoreText;
    private int keysGet = 0;
    private Dictionary<string, bool> dungeonKeys;

    private void Awake()
    {
        instance = this;
        this.dungeonKeys = new Dictionary<string, bool>();
        this.dungeonKeys.Add("dcfb53be-f995-413c-8d63-33b3febeec23", false);
        /*
         * per le altre chiavi
         this.dungeonKeys["dcfb53be-f995-413c-8d63-33b3febeec23"] = false;
         this.dungeonKeys["dcfb53be-f995-413c-8d63-33b3febeec23"] = false;
        */

        //DontDestroyOnLoad(this.gameObject);
    }

    public void ClearThePath()
    {
        scoreText.text = ": " + this.keysGet;
        if (keysGet == 3)
        {
            ClearPath.instance.AllKeyGet();
        }
    }

    public void setStateOfKey(string id)
    {
        this.keysGet++;
        this.dungeonKeys[id] = true;
        ClearThePath();
    }
    public bool getStateOfKey(string id)
    {
        //Debug.Log(keys);
        //return keys[id];
        return dungeonKeys[id];
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, bool> element in data.DungeonKey)
        {
            //Debug.Log(dungeonKeys[element.Key]);
            if (dungeonKeys.ContainsKey(element.Key))
            {
                if (element.Value)
                {
                    keysGet++;
                }
                dungeonKeys[element.Key] = element.Value;
            }
        }
        ClearThePath();
        //Debug.Log(keys);
    }

    public void SaveData(ref GameData data)
    {
        foreach(KeyValuePair<string, bool> element in dungeonKeys)
        {
            if (!data.DungeonKey.ContainsKey(element.Key))
            {
                data.DungeonKey.Add(element.Key, element.Value);
            }
        }
    }
}
