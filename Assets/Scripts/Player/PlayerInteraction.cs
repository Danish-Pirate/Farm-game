using System;
using Inventory;
using Tiledata;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player {
    public class PlayerInteraction : MonoBehaviour {
        [SerializeField] private float interactionRange;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private TileMapManager tileMapManager;
        private InventoryManager inventoryManager;

        public void Start() {
            inventoryManager = InventoryManager.Instance;

            InputManager.LeftClick += (object sender, EventArgs args) => {
                Item item = inventoryManager.GetSelectedItem(false);
                
                Interact(item);
            };
        }

        public void Interact(Item item) {
            Vector3 worldPos = GetWorldPosition(Input.mousePosition);
            
            // if the object is not in range, return
            if (!IsPosInRange(worldPos)) return;
            
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            
            // Check if clicked on sprite object
            if (hit) {
                InteractSprite(item, hit);
            }
            // else tile has been clicked on
            else {
                (TileBase tile, Vector3Int gridPosOfCell) = tileMapManager.GetTileAndGridPos(worldPos);
                tileMapManager.TryInteract(gridPosOfCell, tile, item);
            }
        }

        private static void InteractSprite(Item item, RaycastHit2D hit) {
            GameObject hitObject = hit.collider.gameObject;
            IInteractable interactScript = hitObject.GetComponent<IInteractable>();

            if (interactScript == null) return;

            interactScript.Execute(item);
        }

        // checks if pos is within interaction range
        bool IsPosInRange(Vector3 worldPos) {
            Vector3 playerPos = transform.position;
            float distance = Vector3.Distance(playerPos, worldPos);
            return distance <= interactionRange;
        }

        private Vector3 GetWorldPosition(Vector3 mousePos) {
            // Get the world position of the mouse
            Vector3 screenPos = mousePos;
            screenPos.z = -mainCamera.transform.position.z;
            
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);

            return worldPos;
        }
    }
}