using System.Linq;
using UnityEngine;
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

    public static string VectorToString(Vector2 test)
    {
        if (test == Vector2.down) return "DOWN";
        if (test == Vector2.up) return "UP";
        if (test == Vector2.right) return "RIGHT";
        if (test == Vector2.left) return "LEFT";
        return null;
    }

}