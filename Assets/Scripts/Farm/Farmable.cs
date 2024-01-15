using UnityEngine;

namespace DefaultNamespace {
    public class Farmable : MonoBehaviour, IInteractable {

        [SerializeField] private CropData cropData;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private LootTableData cropLootTable;
        private CropGrowthBehaviour growthBehaviour;
        
        private const float SpawnSpread = 0.7f; // Radius of how far loot can spawn around crop
        
        private void Start() {
            growthBehaviour = new CropGrowthBehaviour(cropData);
            cropLootTable = cropData.CropLootTable;

            TimeManager.OnDateTimeChanged += AdvanceGrowStage;
        }
        
        public void AdvanceGrowStage(DateTime dateTime) {
            growthBehaviour.AdvanceGrowthStage();
            spriteRenderer.sprite = cropData.SpriteGrowStages[growthBehaviour.CurrentGrowthStage - 1];
        }

        public void Execute(Item item) {
            HarvestCrop();
        }

        private void HarvestCrop() {
            if (growthBehaviour.IsFullyGrown()) {
                DropLoot();
            }
            
            // clean up
            TimeManager.OnDateTimeChanged -= AdvanceGrowStage;
            Destroy(gameObject);
        }
        
        private void DropLoot() {
            foreach (GameObject itemPrefab in cropLootTable.GetDroppedLoot()) {
                Vector2 randomCircle = Random.insideUnitCircle * SpawnSpread;
                Vector3 spawnPos = new Vector3(transform.position.x + randomCircle.x, transform.position.y + randomCircle.y);
                Instantiate(itemPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}