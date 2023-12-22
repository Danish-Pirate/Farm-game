using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControl : MonoBehaviour {
    public GameObject inventoryGroup;

    private bool showInventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inventoryGroup.SetActive(showInventory);
        if (Input.GetKeyDown(KeyCode.Tab)) {
            showInventory = !showInventory;
        }
    }
}
