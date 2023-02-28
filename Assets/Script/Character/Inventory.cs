using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private List<string> items;
    //public string obj = "prova"; 
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        items = new List<string>(); 

        //items.Add(obj, 0);
        //addItem(prova);
    }

    public void AddItem(string item)
    {
        items.Add(item);
        //Debug.Log(items[prova]);
    }
}
