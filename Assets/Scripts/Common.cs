using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common
{
    // Converts a controller id and axis name to the format "Jx Input Name"
    public static float GetControllerInputAxis(int controllerID, string axisName)
    {
        // TODO investigate GetAxisRaw vs GetAxis
        return Input.GetAxisRaw("J" + controllerID + " " + axisName);
    }

    // Return a vector representation of the joystick input
    // Scale defaults to 1.0f
    // Interprets 90+% input treated as full
    public static Vector3 GetScaledVectorInput(int controllerID, string axisNameX, string axisNameY, float scale = 1.0f)
    {
        float x = GetControllerInputAxis(controllerID, axisNameX);
        float y = GetControllerInputAxis(controllerID, axisNameY);
        Vector3 result = new Vector3(x, y, 0.0f);
        float effectiveScale = Mathf.Min(1.0f, Mathf.Sqrt(Mathf.Pow(x * 1.1f, 2) + Mathf.Pow(y * 1.1f, 2))) * scale;
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

    // Rotate a Vector2 by an angle counter-clockwise (in degrees)
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
        angle = ((angle % 360) + 360) % 360;
        int x = angle < 67.5f || angle > 292.5f ? 1 : (angle > 112.5f && angle < 247.5f ? -1 : 0);
        int y = angle > 22.5f && angle < 157.5f ? 1 : (angle > 202.5f && angle < 337.5f ? -1 : 0);
        return new Vector3Int(x, y, 0);
    }

    // Return the unit circle angle of a vector
    public static float VectorAngleDegrees(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}
