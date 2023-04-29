using UnityEngine;

public static class ColorUtils 
{
    public static Color RemapColor(float value, Color oMin, Color oMax, AnimationCurve remapCurve)
    {
        Color color;
        float t = Mathf.InverseLerp(0, 1, value);
        color = Color.Lerp(oMin, oMax, remapCurve.Evaluate(t));
        return color;
    }
}
