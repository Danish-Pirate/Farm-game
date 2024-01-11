using DefaultNamespace;
using UnityEngine;

public class InteractableObject : Interactable {
    public InteractableObjectData interactableObjectData;
    
    public int maxDropAmount = 5;
    public int minDropAmount = 2;
    private const float MaxDropSpread = 0.7f;  // max dist item can spawn from object when destroyed

    public override void Interact(Item item) {
        if (item != null && item.type  == interactableObjectData.requiredToolType) {
            DestroyObject();
        }
    }

    void DestroyObject() {
        DropLoot();
        Destroy(gameObject);
    }

    void DropLoot() {
        int dropAmount = Random.Range(minDropAmount, maxDropAmount);

        for (int i = 0; i < dropAmount; i++) {
            Vector2 randomCircle = Random.insideUnitCircle * MaxDropSpread;
            Vector3 spawnPos = new Vector3(transform.position.x + randomCircle.x, transform.position.y + randomCircle.y);
            Instantiate(interactableObjectData.prefab, spawnPos, Quaternion.identity);
        }
    }
}
