using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GatheredObject : MonoBehaviour
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
    public string obejctName;

    private void Start()
    {
        if (type == "key")
        {
            if (DungeonObject.instance.GetStateOfKey(id))
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Update()
    {
        if (type == "key")
        {
            if (DungeonObject.instance.GetStateOfKey(id))
            {
                Destroy(this.gameObject);
            }
        }
        transform.Rotate(0, .5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(type == "healthy")
        {
            CollectToInventory();
            Player.instance.HealthUp();
        } else if(type == "key")
        {
            DungeonObject.instance.SetStateOfKey(id);
            collectedKey.Play();
        } else if(type == "unhealthy")
        {
            CollectToInventory();
            Player.instance.HealthDown();
        }

        Destroy(this.gameObject);
    }

    private void CollectToInventory()
    {
        Inventory.instance.AddItem(itemType);
        GameObject itemObject = Instantiate(itemInvetory, content);
        var itemName = itemObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        var ItemSprite = itemObject.transform.Find("ItemSprite").GetComponent<Image>();
        itemName.text = obejctName;
        ItemSprite.sprite = icon;
    }
}
