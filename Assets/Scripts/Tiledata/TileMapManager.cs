using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Tiledata {
    public class TileMapManager : MonoBehaviour {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileData tileData;

        [SerializeField] private Item plantSeed;
        [SerializeField] private GameObject plant;

        public (TileBase, Vector3Int) GetTileAndGridPos(Vector3 worldPosition) {
            Vector3Int tileGridPosition = tilemap.WorldToCell(worldPosition);

            TileBase clickedTile = tilemap.GetTile(tileGridPosition);

            return (clickedTile, tileGridPosition);
        }

        public void TryInteract(Vector3Int worldPosition, TileBase tile, Item heldItem) {
            
            // Plow soil
            if (tile == tileData.tiles[0] && heldItem.type == Item.ItemType.Hoe) {
                tilemap.SetTile(worldPosition, tileData.tilesToReplaceWith[0]);
                AudioManager.Instance.PlaySound(Sound.PLOW, 1, false);
            }
            
            // Plant
            else if (tile == tileData.tilesToReplaceWith[0] && heldItem == plantSeed) {
                Instantiate(plant, new Vector3(worldPosition.x + 0.5f, worldPosition.y + 0.5f), Quaternion.identity); // 0.5f added to center the prefab in the cell
                AudioManager.Instance.PlaySound(Sound.PLOW, 1, false);
            }
        }
    }
}
