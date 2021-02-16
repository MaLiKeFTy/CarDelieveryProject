using System.Collections.Generic;
using UnityEngine;

public static class FloatingUiProcessor
{
    public static HashSet<FloatingUiController> floatingGameObjs = new HashSet<FloatingUiController>();


    public static FloatingUiController GetClosestFloatingUi(HashSet<FloatingUiController> floatingUiList, Vector2 touchPos)
    {
        FloatingUiController tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = touchPos;
        foreach (FloatingUiController t in floatingUiList)
        {
            float dist = Vector3.Distance(t.RectToMove.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public static HashSet<FloatingUiController> FilterList(bool isLeft)
    {
        HashSet<FloatingUiController> tempPoints = new HashSet<FloatingUiController>();
        foreach (var item in floatingGameObjs)
        {
            if (item.IsLeft == isLeft)
            {
                tempPoints.Add(item);
            }
        }
        return tempPoints;
    }

}
