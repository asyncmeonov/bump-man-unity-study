using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class AboveGroundTilemapController : MonoBehaviour
{
    private TilemapCollider2D col;
    private Tilemap tilemap;
    private Color tilemapColor;
    private HashSet<Vector3Int> changedTiles;
    void Start()
    {
        col = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<Tilemap>();
        tilemapColor = tilemap.color;
        changedTiles = new HashSet<Vector3Int>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Vector3Int centerTilePosition = tilemap.WorldToCell(other.transform.position);

        //get neighbors
        HashSet<Vector3Int> obscuringTiles = VectorUtil.GetNeighborsInRange(centerTilePosition,2);
        changedTiles.UnionWith(obscuringTiles);
        foreach (var tilePos in obscuringTiles)
        {
            tilemap.SetTileFlags(tilePos, TileFlags.None); //This is important otherwise the default tiles flags forbid modification.
            tilemap.SetColor(tilePos, new Color(1.0f, 1.0f, 1.0f, 0.5f));
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        //v1 return all color back to normal
        foreach (var changedPos in changedTiles)
        {
            tilemap.SetColor(changedPos, new Color(1.0f, 1.0f, 1.0f, 1f));
        }
        //tilemap.color = tilemapColor;

    }
}
