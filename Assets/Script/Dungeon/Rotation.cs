using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour
{
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]


    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public AudioSource collectedKey;
    public string type;
    public string itemType;
    public Transform content;
    public GameObject itemInvetory;
    public Sprite icon;

    private void Update()
    {
        if(type == "key")
        {
            if (DungeonObject.instance.getStateOfKey(id))
            {
                Destroy(this.gameObject);
            }
        }
        transform.Rotate(0, .5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(type == "item")
        {
            Inventory.instance.addItem(itemType);
            GameObject itemObject = Instantiate(itemInvetory, content);
            var itemName = itemObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemSprite = itemObject.transform.Find("ItemSprite").GetComponent<Image>();
            itemName.text = "Obsidian";
            ItemSprite.sprite = icon;

            Destroy(this.gameObject);
        } else if(type == "key")
        {
            DungeonObject.instance.setStateOfKey(id);
            collectedKey.Play();
            Destroy(this.gameObject);
        }
        /*
        Destroy(wall);
        pillar.GetComponent<MeshRenderer>().material = activated;

        CharacterStats.instance.addKey();
        scoreText.text = ": " + CharacterStats.instance.key;
        
        CharacterStats.instance.hasKeys(1);
        */
        //navMesh.BuildNavMesh();
    }
}
