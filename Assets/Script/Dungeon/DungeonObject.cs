using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonObject : MonoBehaviour, IDataPersistance
{
    public static DungeonObject instance;
    public TextMeshProUGUI scoreText;
    public int totalKeys;
    private int keysGet = 0;
    private Dictionary<string, bool> dungeonKeys;

    private void Awake()
    {
        instance = this;
        this.dungeonKeys = new Dictionary<string, bool>();
        this.dungeonKeys.Add("dcfb53be-f995-413c-8d63-33b3febeec23", false);
        this.dungeonKeys.Add("3be90377-fde2-40e3-b9a2-9839783a7d21", false);
        this.dungeonKeys.Add("8330c3bc-622f-4ae8-972d-5686b59452b7", false);
    }

    public void ClearThePath()
    {
        scoreText.text = ": " + this.keysGet;
        if (keysGet == totalKeys)
        {
            ClearPath.instance.AllKeyGet();
        }
    }

    public void SetStateOfKey(string id)
    {
        this.keysGet++;
        this.dungeonKeys[id] = true;
        ClearThePath();
    }
    public bool GetStateOfKey(string id)
    {
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
