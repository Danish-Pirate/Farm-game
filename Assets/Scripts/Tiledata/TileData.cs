using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiledata {
    [CreateAssetMenu(menuName = "Scriptable object/TileData")]
    public class TileData : ScriptableObject {

        public TileBase[] tiles; // Tiles that can be plowed
        public TileBase[] tilesToReplaceWith; // Tiles they are replaced with
    }
}