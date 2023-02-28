using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClearPath : MonoBehaviour
{
    public static ClearPath instance;
    public Material activated;
    private List<GameObject> walls;
    private void Awake()
    {
        walls = new List<GameObject>();
        foreach(var i in FindObjectsOfType<NavMeshObstacle>())
        {
            GameObject objectParent = i.gameObject.transform.parent.gameObject;
            if (!walls.Contains(objectParent))
            {
                walls.Add(objectParent);
            }
        }
        instance = this;
    }

    public void AllKeyGet()
    {
        ActiveWalls(false);
        this.gameObject.GetComponent<MeshRenderer>().material = activated;
    }

    public void ActiveWalls(bool boolean)
    {
        foreach (var i in walls)
        {
            i.active = boolean;
        }
    }
}
