using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvItem {
    public string title = "";
    public string description = "";
    public Sprite spr;
    public GameObject prefab;
    public int quantity = 1;

    public InvItem(Sprite spr, string title, string description, GameObject prefab) {
        this.spr = spr;
        this.title = title;
        this.description = description;
        this.prefab = prefab;
    }
}

public class Inventory : MonoBehaviour
{

    public Dictionary<string, InvItem> inventoryData = new Dictionary<string, InvItem>();
    [SerializeField]
    public GameObject invRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            if(invRef.activeSelf) { 
                invRef.SetActive(false);
            } else {
                invRef.SetActive(true);
                invRef.GetComponent<InventoryDisplay>().updateInventoryDisplay();
            }
        }
    }
}
