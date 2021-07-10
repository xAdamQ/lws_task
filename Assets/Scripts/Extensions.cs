using UnityEngine;

public static class Extensions
{
    public static Vector3 SetZ(this Vector3 vector3, float z)
    {
        vector3.z = z;
        return vector3;
    }

    public static bool IsAbout(this float value, float target, float ignored)
    {
        return Mathf.Abs(value) <= ignored + target;
    }
}