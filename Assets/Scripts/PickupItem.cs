using Inventory;
using UnityEngine;

public class PickupItem : MonoBehaviour {
    private Transform playerTransform;
    private InventoryManager inventoryManager;
    [SerializeField] private Item item;
    private const float Speed = 5;
    private const float PickUpDistance = 0.5f;
    private const float MagnetDistance = 1.5f;
    private float despawnTimerInSeconds = 120.0f;

    private void Awake() {
        playerTransform = GameObject.Find("Player").transform;
        inventoryManager = InventoryManager.Instance;
    }

    private void Update() {
        despawnTimerInSeconds -= Time.deltaTime;
        if (despawnTimerInSeconds < 0) {
            Destroy(gameObject);
        }
        
        if (!IsInMagnetDistance()) return;
        
        if (!inventoryManager.HasEmptySlot()) return;
        
        MoveTowardsPlayer();

        if (!IsInPickUpDistance()) return;

        PickUp();
    }

    private void PickUp() {
        bool hasBeenPickedUp = inventoryManager.AddItem(item);
        if (hasBeenPickedUp) {
            Destroy(gameObject);
        }
    }

    private bool IsInPickUpDistance() {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        return distance <= PickUpDistance;
    }

    private void MoveTowardsPlayer() {
        transform.position = Vector3.MoveTowards(
            transform.position,
            playerTransform.position,
            Speed * Time.deltaTime
        );
    }

    private bool IsInMagnetDistance() {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        return distance <= MagnetDistance;
    }
}
