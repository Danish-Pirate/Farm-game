using System;
using Inventory;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player {
    public class PlayerInteraction : MonoBehaviour {
        [SerializeField] private float interactionRange;
        [SerializeField] private Camera mainCamera;
        private InventoryManager inventoryManager;
        [SerializeField] private TilemapReadController tilemapReadController;

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
                TileBase tile = tilemapReadController.GetTile(worldPos);
                TileInteract(tilemapReadController.GetGridPosition(worldPos), tilemapReadController.GetTileData(tile), item);
            }
        }

        private static void InteractSprite(Item item, RaycastHit2D hit) {
            GameObject hitObject = hit.collider.gameObject;
            IInteractable interactScript = hitObject.GetComponent<IInteractable>();

            if (interactScript == null) return;

            interactScript.Execute(item);
        }

        public void TileInteract(Vector3Int worldPos, TileData tileData, Item item) {
            if (tileData == null || item == null) return;
            
            if (tileData.requiredTool == item.type) {
                tilemapReadController.tilemap.SetTile(worldPos, tileData.TileToReplaceWith);
            }
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