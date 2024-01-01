using DefaultNamespace;
using UnityEngine;

public class InteractableRock : Interactable {
    [SerializeField] private GameObject prefab;
    [SerializeField] private Item.ItemType requiredToolType;
    public override void Interact(Item item) {
        if (item.type == requiredToolType) {
            DestroyObject();
        }
    }

    void DestroyObject() {
        DropLoot();
        Destroy(gameObject);
    }

    void DropLoot() {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
