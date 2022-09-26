using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Rotation : MonoBehaviour
{
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public AudioSource collectedKey;
    private void Update()
    {
        if (DungeonObject.instance.getStateOfKey(id))
        {
            Destroy(this.gameObject);
        }
        transform.Rotate(0, .5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        Destroy(wall);
        pillar.GetComponent<MeshRenderer>().material = activated;

        CharacterStats.instance.addKey();
        scoreText.text = ": " + CharacterStats.instance.key;
        
        CharacterStats.instance.hasKeys(1);
        */
        DungeonObject.instance.setStateOfKey(id);
        collectedKey.Play();
        Destroy(this.gameObject);
        //navMesh.BuildNavMesh();
    }
}
