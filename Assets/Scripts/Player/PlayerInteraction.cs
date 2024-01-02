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

        public void Start() {
            inventoryManager = InventoryManager.Instance;
            
            
            InputManager.LeftClick += (object sender, EventArgs args) => {
                Item item = inventoryManager.GetSelectedItem(false);
                
                Interact(item);
            };
        }

        public void Interact(Item item) {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.down);
            
            if (!hit) return;
            
            // if the object is not in range, return
            if (!IsObjectInRange(hit.collider.transform.position)) return;
            
            GameObject hitObject = hit.collider.gameObject;
            Interactable interactScript = hitObject.GetComponent<Interactable>();

            if (interactScript == null) return;
            
            interactScript.Interact(item);
        }
        
        // checks if object is within interaction range
        bool IsObjectInRange(Vector3 objectPos) {
            Vector3 playerPos = transform.position;
            float distance = Vector3.Distance(playerPos, objectPos);
            return distance <= interactionRange;
        }
    }
}