using System;
using DefaultNamespace;
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
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            (TileBase tile,Vector3Int gridPosition) = tilemapReadController.GetTile(worldPos);

            TileData tileData = tilemapReadController.GetTileData(tile);

            if (tileData != null && tileData.isInteractable) {
                TileInteract(gridPosition, tile, tileData, item);
            }

            
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.down);
            if (!hit) return;

            // if the object is not in range, return
            if (!IsObjectInRange(hit.collider.transform.position)) return;

            GameObject hitObject = hit.collider.gameObject;
            Interactable interactScript = hitObject.GetComponent<Interactable>();

            if (interactScript == null) return;
            
            interactScript.Interact(item);
        }

        public void TileInteract(Vector3Int worldPos, TileBase tile, TileData tileData, Item item) {
            if (tileData.requiredTool == item.type) {
                tilemapReadController.tilemap.SetTile(worldPos, tileData.TileToReplaceWith);
            }
        }

        // checks if object is within interaction range
        bool IsObjectInRange(Vector3 objectPos) {
            Vector3 playerPos = transform.position;
            float distance = Vector3.Distance(playerPos, objectPos);
            return distance <= interactionRange;
        }
    }
}