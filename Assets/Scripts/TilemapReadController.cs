using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapReadController : MonoBehaviour {
    public Tilemap tilemap;
    [SerializeField] private List<TileData> tileDatas;
    
    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Start() {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (TileData tileData in tileDatas) {
            foreach (TileBase tile in tileData.tiles) {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    public TileBase GetTile(Vector2 worldPos) {
        var gridPosition = GetGridPosition(worldPos);

        TileBase tile = tilemap.GetTile(gridPosition);

        return tile;
    }

    public Vector3Int GetGridPosition(Vector2 worldPos) {
        Vector3Int gridPosition = tilemap.WorldToCell(worldPos);
        return gridPosition;
    }

    public TileData GetTileData(TileBase tileBase) {
        try {
            return dataFromTiles[tileBase];
        }
        catch {
            return null;
        }
    }
}
