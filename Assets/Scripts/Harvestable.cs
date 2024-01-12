
using UnityEngine;

public abstract class Harvestable : MonoBehaviour, IInteractable {
    [SerializeField] protected Item.ItemType requiredTool;
    [SerializeField] protected LootTableData  lootTableData;
    protected const float SpawnSpread = 0.7f;

    public virtual void Execute(Item item) {
        if (item && item.type == requiredTool) {
            DropLoot();
            Destroy(gameObject);
        }
    }

    protected void DropLoot() {
        foreach (GameObject itemPrefab in lootTableData.GetDroppedLoot()) {
            Vector2 randomCircle = Random.insideUnitCircle * SpawnSpread;
            Vector3 spawnPos = new Vector3(transform.position.x + randomCircle.x, transform.position.y + randomCircle.y);
            Instantiate(itemPrefab, spawnPos, Quaternion.identity);
        }
    }
}
