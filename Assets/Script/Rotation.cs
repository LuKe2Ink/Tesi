using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Rotation : MonoBehaviour
{
    public GameObject wall;
    public GameObject pillar;

    public TextMeshProUGUI scoreText;

    public NavMeshSurface navMesh;
    public Material activated;
    public AudioSource collectedKey;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, .5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(wall);
        pillar.GetComponent<MeshRenderer>().material = activated;

        CharacterStats.instance.addKey();
        scoreText.text = ": " + CharacterStats.instance.key;
        
        CharacterStats.instance.hasKeys(1);
        collectedKey.Play();
        Destroy(this.gameObject);
        //navMesh.BuildNavMesh();
    }
}
