using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    [SerializeField]
    private GameObject inventoryGroup;
    [SerializeField]
    private InventorySlot[] InventorySlots;
    [SerializeField]
    private GameObject inventoryItemPrefab;
    [SerializeField]
    private int maxItemStackSize = 32;
    [SerializeField]
    private Item[] startingItems;
    [SerializeField]
    private TextMeshProUGUI itemPickupText;

    private int selectedSlot = -1;
    [HideInInspector]
    public bool showInventory;

    private void Start() {
        ChangeSelectedSlot(0);
        foreach (var startingItem in startingItems) {
            AddItem(startingItem, false);
        }
    }

    void ChangeSelectedSlot(int newValue) {
        if (selectedSlot >= 0)
            InventorySlots[selectedSlot].Deselect();
        
        InventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
    
    /// <summary>
    ///  <para>Displays message above hotbar informing the player what item they picked up.</para>
    /// </summary>
    /// <param name="item">The item that was picked up.</param>
    void DisplayPickupText(Item item) {
        itemPickupText.text = $"{item.name}";
        Animator animator = itemPickupText.GetComponent<Animator>();
        animator.SetTrigger("Fade");
    }
    
    /// <summary>
    ///  <para>Attempts to add an item to the player inventory.</para>
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <param name="displayPickupText">Specifies whether to display the ItemPickup Text. True by default</param>
    /// <returns>Boolean on whether the operation was successful.</returns>
    public bool AddItem(Item item, bool displayPickupText = true) {
        
        // check if any slot is not empty, has the same item, has less items than max,
        // and the item is stackable
        for (int i = 0; i < InventorySlots.Length; i++) {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < maxItemStackSize &&
                itemInSlot.item.stackable == true) {
                
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                if (displayPickupText)
                    DisplayPickupText(item);
                return true;
            }
        }
        
        // Look for an empty inventory slot
        for (int i = 0; i < InventorySlots.Length; i++) {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) {
                SpawnNewItem(item, slot);
                if (displayPickupText)
                    DisplayPickupText(item);
                return true;
            }
        }
        
        return false;
    }

    public bool HasEmptySlot() {
        for (int i = 0; i < InventorySlots.Length; i++) {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) {
                return true;
            }
        }

        return false;
    }
    
    /// <summary>
    ///  <para>Spawns a new item in the player inventory.</para>
    /// </summary>
    /// <param name="item">The type of item to spawn.</param>
    /// <param name="slot">The inventory slot to spawn the item in.</param>
    void SpawnNewItem(Item item, InventorySlot slot) {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitaliseItem(item);
    }
    
    /// <summary>
    ///  <para>Looks through the player inventory and finds the currently selected item</para>
    /// </summary>
    /// <param name="use">Specifies whether the item should be used or not.</param>
    /// <returns>The currently selected item.</returns>
    public Item GetSelectedItem(bool use) {
        InventorySlot slot = InventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null) {
            Item item = itemInSlot.item;
            if (use == true) {
                itemInSlot.count--;
                if (itemInSlot.count <= 0) {
                    Destroy(itemInSlot.gameObject);
                }
                else {
                    itemInSlot.RefreshCount();
                }
            }

            return item;
        }

        return null;
    }

    public void OpenMainInventoryUI() {
        // check if tab has been pressed to show/hide main inventory UI
        showInventory = !showInventory;
        inventoryGroup.SetActive(showInventory);
    }
    
    void ParseAlphaKeyInput() {
        // check if there is an input, it is a number, and it is between 1-8 (both inclusive)
        if (Input.inputString != null) {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 9) {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            OpenMainInventoryUI();
        }
        
        ParseAlphaKeyInput();
    }
}
