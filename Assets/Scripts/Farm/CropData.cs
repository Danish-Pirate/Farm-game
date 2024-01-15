using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/CropData")]
public class CropData : ScriptableObject {
    public string CropName;
    public CropType CropType;
    public int GrowCyclesToGrow; // How many "grow cycles" have to complete before the crop is fully grown
    public Sprite[] SpriteGrowStages; // Sprites for all the different grow stages
    public LootTableData CropLootTable; // Table for what loot the crop drops when harvested
}

public enum CropType {
    Potato, Wheat, Carrot
}
