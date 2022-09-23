using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats instance;
    public int key;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    public void addKey()
    {
        key++;
    }
    public void resetKey()
    {
        key = 0;
    }
    public void hasKeys(int keyNeeded)
    {
        if(key == keyNeeded)
        {
            Debug.Log("si si");
        }
    }
}
