using Player;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerManager>();
            }
            return _instance;
        }
    }
    
    public Camera mainCamera;
    
    [HideInInspector]
    public PlayerMovement _playerMovement;
    
    public InventoryManager _inventoryManager;
    [HideInInspector]
    public PlayerInteraction _playerInteraction;

    private void Start() {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInteraction = GetComponent<PlayerInteraction>();
    }

    void Update() {
        // disable player movement if inventory gui is active.
        _playerMovement.movementEnabled = !_inventoryManager.showInventory;

        if (Input.GetMouseButtonDown(0)) {
            _playerInteraction.Interact(_inventoryManager.GetSelectedItem(false));
        }
    }
}
