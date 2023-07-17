using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Dictionary<string, InvItem> invData;
    [SerializeField]
    public GameObject textPrefab;
    public int rows = 5;
    public int cols = 5;
    public GameObject[] invCells;

    // Start is called before the first frame update
    void Start()
    {
        invData = GameObject.Find("Player").GetComponent<Inventory>().inventoryData;

        GameObject gridObject = new GameObject("Grid");
        gridObject.transform.parent = transform;
        gridObject.transform.position = transform.position;
        GridLayoutGroup grid = gridObject.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(100, 100);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = cols;
        grid.spacing = new Vector2(5,5);
        //grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
        invCells = new GameObject[rows*cols];

        int count = 0;
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                // Instantiate Text prefab
                GameObject textObject = Instantiate(textPrefab, gridObject.transform);
                //textObject.transform.position = new Vector2(gridObject.transform.position.x + j, gridObject.transform.position.y + i);
                // Set text
                TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
                invCells[count] = textObject;
                count++;
                //text.text = $"Cell {i}-{j}";
            }
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateInventoryDisplay() {
        int count = 0;
        foreach (KeyValuePair<string, InvItem> kvp in invData) {
            invCells[count].GetComponent<TextMeshProUGUI>().text = kvp.Key + " " + kvp.Value.quantity.ToString();
            invCells[count] .GetComponentInChildren<Image>().sprite = kvp.Value.spr;
            
            count++;
        }
    }
}
