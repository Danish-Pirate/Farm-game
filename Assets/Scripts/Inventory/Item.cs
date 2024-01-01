using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject {
    [Header("Only gameplay")] public TileBase tile;
    public ItemType type;

    [Header("Only UI")] public bool stackable = true;

    [Header("Both")] public Sprite image;

    public enum ItemType {
        Pickaxe, Axe, Hoe, Misc
    }

    public enum ActionType {
        Dig,
        Mine
    }
}
