using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/LootTableData")]
public class LootTableData : ScriptableObject {
    [System.Serializable]
    public class LootTableEntry {
        public GameObject itemPrefab;
        public int minAmount;
        public int maxAmount;
        [Range(0, 1)] public float dropChance;
    }
    
    [SerializeField]
    private LootTableEntry[] droppableItems;

    public GameObject[] GetDroppedLoot() {
        List<GameObject> droppedItems = new List<GameObject>();

        foreach (var entry in droppableItems) {
            if (Random.value <= entry.dropChance) {
                
                int itemAmount = Random.Range(entry.minAmount, entry.maxAmount);

                for (int i = 0; i < itemAmount; i++) {
                    droppedItems.Add(entry.itemPrefab);
                }
            }
        }

        return droppedItems.ToArray();
    }
}