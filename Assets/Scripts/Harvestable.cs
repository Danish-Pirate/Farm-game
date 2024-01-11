
using UnityEngine;

public abstract class Harvestable : MonoBehaviour, IInteractable {
    [SerializeField] private Item.ItemType requiredTool;
    [SerializeField] private LootTableData lootTableData;
    private const float SpawnSpread = 0.7f;

    public void Execute(Item item) {
        if (item && item.type == requiredTool) {
            DropLoot();
            Destroy(gameObject);
        }
    }

    void DropLoot() {
        foreach (GameObject itemPrefab in lootTableData.GetDroppedLoot()) {
            Vector2 randomCircle = Random.insideUnitCircle * SpawnSpread;
            Vector3 spawnPos = new Vector3(transform.position.x + randomCircle.x, transform.position.y + randomCircle.y);
            Instantiate(itemPrefab, spawnPos, Quaternion.identity);
        }
    }
}
