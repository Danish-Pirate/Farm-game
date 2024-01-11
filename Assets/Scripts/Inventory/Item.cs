using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject {
    public ItemType type;

    [Header("Only UI")] public bool stackable = true;

    [Header("Both")] public Sprite image;

    public enum ItemType {
        Pickaxe, Axe, Hoe, Misc
    }
}
