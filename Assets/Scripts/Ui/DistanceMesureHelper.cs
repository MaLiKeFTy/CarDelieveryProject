using UnityEngine;

public static class DistanceMesureHelper
{
    public static int GetClosestDistance(Vector2 origin, Vector2[] points)
    {
        int closestIndex = 0;

        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < points.Length; i++)
        {
            float distance = Vector3.Distance(points[i], origin);

            if (distance < closestDistance)
            {
                closestIndex = i;
                closestDistance = distance;
            }
        }

        return closestIndex;
    }
}
