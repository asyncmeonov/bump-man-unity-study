using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//Contrived util classes only for necessary vector operations I encounter as I develop
//Not a comprehensive utility class!
public static class VectorUtil
{
    public static bool IsWithinRange(Vector3 v1, Vector3 v2, float tolerance = 0.1f)
    {
        float min_x = v2.x - tolerance;
        float max_x = v2.x + tolerance;
        float min_y = v2.y - tolerance;
        float max_y = v2.y + tolerance;
        return (v1.x >= min_x && v1.x <= max_x) && (v1.y >= min_y && v1.y <= max_y);
    }

    public static HashSet<Vector3Int> GetNeighborsInRange(Vector3Int center, int range)
    {
        //implements a simple, range-limited flood-fill
        //https://www.redblobgames.com/pathfinding/a-star/introduction.html
        HashSet<Vector3Int> reached = new HashSet<Vector3Int>();
        Queue<Vector3Int> frontier = new Queue<Vector3Int>();
        reached.Add(center);
        frontier.Enqueue(center);
        
        while (frontier.Count > 0)
        {
            if (reached.Count >= 4*range) break;

            Vector3Int current = frontier.Dequeue();
            foreach (var next in GetAdjacentNeighbors(current, true))
            {
                if(!reached.Contains(next))
                {
                    frontier.Enqueue(next);
                    reached.Add(next);
                }
            }
        }

        return reached;
    }

    public static List<Vector3Int> GetAdjacentNeighbors(Vector3Int center, bool includeDiagonals = false)
    {
        if(includeDiagonals)
        {
            return new List<Vector3Int>()
            {
                center,
                center + Vector3Int.up,
                center + Vector3Int.down,
                center + Vector3Int.left,
                center + Vector3Int.right,
                center + Vector3Int.up + Vector3Int.left,
                center + Vector3Int.up + Vector3Int.right,
                center + Vector3Int.down + Vector3Int.left,
                center + Vector3Int.down + Vector3Int.right
            };
        }
        else 
        {
            return new List<Vector3Int>()
            {
                center,
                center + Vector3Int.up,
                center + Vector3Int.down,
                center + Vector3Int.left,
                center + Vector3Int.right
            }; 
        }
    }

    public static string VectorToString(Vector2 test)
    {
        if (test == Vector2.down) return "DOWN";
        if (test == Vector2.up) return "UP";
        if (test == Vector2.right) return "RIGHT";
        if (test == Vector2.left) return "LEFT";
        return null;
    }

}