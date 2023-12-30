using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private PlayerMovement _playerMovement;
    private InventoryManager _inventoryManager;
    
    void Start() {
        _playerMovement = GetComponent<PlayerMovement>();
        _inventoryManager = InventoryManager.instance;
    }
    
    void Update() {
        // disable player movement if inventory gui is active.
        _playerMovement.movementEnabled = !_inventoryManager.showInventory;
    }
}
