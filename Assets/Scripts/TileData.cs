using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Tile Data")]
public class TileData : ScriptableObject {
    public List<TileBase> tiles;
    public TileBase TileToReplaceWith;

    public Item.ItemType requiredTool;
    public bool isInteractable;
}
