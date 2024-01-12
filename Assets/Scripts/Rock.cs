using DefaultNamespace;
using Utility;

public class Rock : Harvestable {
    public override void Execute(Item item) {
        if (item && item.type == requiredTool) {
            DropLoot();
            AudioManager.Instance.PlaySound(Sound.MINE, 1, false);
            Destroy(gameObject);
        }
    }
}