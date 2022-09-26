using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public SerializableDictionary<string, bool> DungeonKey;

    public GameData()
    {
        DungeonKey = new SerializableDictionary<string, bool>();
    }

}
