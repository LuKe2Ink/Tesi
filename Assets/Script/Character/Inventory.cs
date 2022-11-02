using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private Dictionary<string, int> items;
    public string prova = "prova"; 
    private void Awake()
    {
        instance = this;
        items = new Dictionary<string, int>();
        items.Add(prova, 0);
        addItem(prova);
    }

    public void addItem(string item)
    {
        items.TryGetValue(item, out int value);
        if(value >= 0)
        {
            items[item] += 1; 
        }
        Debug.Log(items[prova]);
    }
}
