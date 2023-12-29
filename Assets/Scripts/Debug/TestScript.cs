using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    public InventoryManager InventoryManager;
    public Item[] itemsToPickup;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            PickupItem(0);
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            PickupItem(1);
        }

        if (Input.GetKeyDown(KeyCode.V)) {
            GetSelectedItem();
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            UseSelectedItem();
        }
    }

    public void UseSelectedItem() {
        Item receivedItem = InventoryManager.GetSelectedItem(true);
        if (receivedItem != null) {
            print($"Used {receivedItem}");
        }
        else {
            print("No item used");
        }
    }

    public void GetSelectedItem() {
        Item receivedItem = InventoryManager.GetSelectedItem(false);
        if (receivedItem != null) {
            print($"Received {receivedItem}");
        }
        else {
            print("No item received");
        }
    }

    public void PickupItem(int id) {
        bool result = InventoryManager.AddItem(itemsToPickup[id]);
        if (result) {
            print("Item added");
        }
        else {
            print("Item not added");
        }
    }
}
