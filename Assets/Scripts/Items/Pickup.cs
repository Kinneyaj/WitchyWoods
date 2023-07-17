using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    public GameObject prefab;
    public string title;
    public string description;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //title = "Fish";
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player") {
            var invData = other.GetComponent<Inventory>().inventoryData;
            var invItem = new InvItem(spriteRenderer.sprite, title, description, prefab);
            if (invData.TryGetValue(title, out InvItem item)) {
                invData[title].quantity++;
            } else {
                invData.Add(title, new InvItem(spriteRenderer.sprite, title, description, prefab));
            }
            if (GameObject.Find("Inventory") != null) {
                GameObject.Find("Inventory").gameObject.GetComponent<InventoryDisplay>().updateInventoryDisplay();
            }
            Destroy(gameObject);
        }
    }
}
