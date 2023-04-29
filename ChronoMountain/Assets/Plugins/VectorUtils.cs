using UnityEngine;

public static class VectorUtils
{
    public static float InverseLerp(Vector2 a, Vector2 b, Vector2 value)
    {
        Vector2 AB = b - a;
        Vector2 AV = value - a;
        return Vector2.Dot(AV, AB) / Vector2.Dot(AB, AB);
    }

    public static Vector2 GetDirection(Vector2 a, Vector2 b)
    {
        Vector2 v2 = a - b;
        return v2.normalized;
    }
}
