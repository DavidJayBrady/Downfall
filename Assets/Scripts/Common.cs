using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common
{
    // Return a vector representation of the joystick input
    // Scale defaults to 1.0f
    // Interprets 90+% input treated as full
    public static Vector3 GetScaledVectorInput(string axisNameX, string axisNameY, float scale = 1.0f)
    {
        Vector3 result = new Vector3(Input.GetAxisRaw(axisNameX), Input.GetAxisRaw(axisNameY), 0.0f);
        float effectiveScale = Mathf.Min(1.0f, Mathf.Sqrt(Mathf.Pow(Input.GetAxisRaw(axisNameX) * 1.1f, 2) + Mathf.Pow(Input.GetAxisRaw(axisNameY) * 1.1f, 2))) * scale;
        VectorNormalize(ref result, effectiveScale);
        return result;
    }

    // Sets the magnitude of a non-zero vector to 1 or to a specific value
    public static void VectorNormalize(ref Vector3 vector, float scale = 1.0f)
    {
        if (vector.magnitude > 0)
            vector *= scale / vector.magnitude;
    }

    // Sets the magnitude of a non-zero vector to 1 or to a specific value
    public static void VectorNormalize(ref Vector2 vector, float scale = 1.0f)
    {
        if (vector.magnitude > 0)
            vector *= scale / vector.magnitude;
    }

    // Rotate a Vector2 by an angle clockwise (in degrees)
    public static void RotateVector2(ref Vector2 vector, float degrees)
    {
        float rad = -degrees * Mathf.Deg2Rad;
        float s = Mathf.Sin(rad);
        float c = Mathf.Cos(rad);
        vector = new Vector2(vector.x * c - vector.y * s, vector.y * c + vector.x * s);
    }

    // Returns a Vector3Int representing an angle from the unit circle (in degrees)
    public static Vector3Int AngleToVector3Int(float angle)
    {
        angle %= 360;
        int x = angle < 60 || angle > 300 ? 1 : (angle > 120 && angle < 240 ? -1 : 0);
        int y = angle > 30 && angle < 150 ? 1 : (angle > 210 && angle < 330 ? -1 : 0);
        return new Vector3Int(x, y, 0);
    }

    public static float VectorAngleDegrees(ref Vector2 vector)
    {
        vector = RotateVector2(ref vector, 45);
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        return angle;
    }
}
