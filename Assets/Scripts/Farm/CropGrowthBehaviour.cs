namespace DefaultNamespace {
    public class CropGrowthBehaviour {
        private readonly CropData cropData;

        public CropGrowthBehaviour(CropData cropData) {
            this.cropData = cropData;
        }
        public int CurrentGrowthStage { get; private set; }

        public void AdvanceGrowthStage() {
            if (CurrentGrowthStage < cropData.GrowCyclesToGrow) {
                CurrentGrowthStage++;
            }
        }

        public bool IsFullyGrown() {
            return CurrentGrowthStage == cropData.GrowCyclesToGrow;
        }
    }
}