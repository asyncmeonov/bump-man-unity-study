using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class AboveGroundTilemapController : MonoBehaviour
{
    private TilemapCollider2D col;
    private Tilemap tilemap;
    private Color tilemap_color;
    void Start()
    {
        Debug.Log("AboveGroundTilemapController Script is attached");
        col = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<Tilemap>();
        tilemap_color = tilemap.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("AboveGroundTilemapController: Triggered by "+other.name);
        // v1 if we are behind a sprite, all aboveground sprites for the entire map are faded
        tilemap.color = tilemap.color * 0.5f;
        //TODO a smarter method that fades only adjacent blocks
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        Debug.Log("Exited, reverting color...");
        //v1 return all color back to normal
        tilemap.color = tilemap_color;

    }
}
