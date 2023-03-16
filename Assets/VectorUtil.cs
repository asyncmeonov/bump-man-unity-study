using System.Linq;
using UnityEngine;
public static class VectorUtil 
{
    public static bool isWithinRange(Vector3 v1, Vector3 v2, float tolerance = 0.1f) =>
    v2.x - tolerance <= v1.x && v1.x <= v2.x + tolerance ||
    v2.y - tolerance <= v1.y && v1.y <= v2.y + tolerance;
}