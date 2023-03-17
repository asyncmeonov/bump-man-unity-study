using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{

    [SerializeField] private Tilemap mapGround;
    [SerializeField] private GameObject point;

    private BoundsInt bounds;
    // Start is called before the first frame update
    void Start()
    {
        // mapGround.CompressBounds();
        // bounds = mapGround.cellBounds;
        // Vector3Int[,] coordinateGrid = GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }


    // private Vector3Int[,] GenerateGrid()
    // {
    //     Vector3Int[,] spots = new Vector3Int[bounds.size.x, bounds.size.y];
    //     for (int x = bounds.xMin, i = 0; i < spots.GetLength(0); x++, i++)
    //     {
    //         for (int y = bounds.yMin, j = 0; j < spots.GetLength(1); y++, j++)
    //         {
    //             if (mapGround.HasTile(new Vector3Int(x, y, 0)))
    //             {
    //                 spots[i, j] = new Vector3Int(x, y, 0);
    //                 Instantiate(point, spots[i,j], Quaternion.identity);
    //             }
    //             else
    //             {
    //                 spots[i, j] = new Vector3Int(x, y, 1);
    //             }

    //         }
    //     }
    //     return spots;
    // }
}
