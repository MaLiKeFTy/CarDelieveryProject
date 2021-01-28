using UnityEngine;

public static class Vector3Extension
{
    /// <summary>
    /// Can only modify one or more Vector 3 axis without having to refrence the other axis 
    /// </summary>
    /// <param name="origin">The original vector3 class</param>
    public static Vector3 With(this Vector3 origin, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? origin.x, y ?? origin.y, z ?? origin.z);
    }
}
