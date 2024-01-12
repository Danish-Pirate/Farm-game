
using DefaultNamespace;
using Utility;

public class Tree : Harvestable {
    public override void Execute(Item item) {
        if (item && item.type == requiredTool) {
            DropLoot();
            AudioManager.Instance.PlaySound(Sound.CHOP, 1);
            Destroy(gameObject);
        }
    }
}
