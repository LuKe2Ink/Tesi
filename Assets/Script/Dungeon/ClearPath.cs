using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClearPath : MonoBehaviour
{
    public static ClearPath instance;
    public GameObject wall;
    public Material activated;
    private void Awake()
    {
        instance = this;
    }
    public void AllKeyGet()
    {
        Destroy(wall);
        this.gameObject.GetComponent<MeshRenderer>().material = activated;
    }
}
