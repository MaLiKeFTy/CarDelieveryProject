using System.Collections.Generic;
using UnityEngine;

public static class FloatingUiProcessor
{
    public static HashSet<FloatingUiController> floatingGameObjs = new HashSet<FloatingUiController>();

    /// <summary>
    /// Gets the closest floating ui element to the touch position 
    /// </summary>
    static FloatingUiController GetClosestFloatingUi(HashSet<FloatingUiController> floatingUiList, Vector2 touchPos)
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

    /// <summary>
    /// Only gets the floating ui elements that is on the left or of the right
    /// </summary>
    static HashSet<FloatingUiController> FilterList(bool isLeft)
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

    /// <summary>
    /// Gets the selected floating ui element by the touch.
    /// </summary>
    public static FloatingUiController SelectedFloatUi(Touch touch)
    {
        FloatingUiController selectedFloatUi;

        if (touch.position.x <= Screen.width / 2)
            selectedFloatUi = GetClosestFloatingUi(FilterList(true), touch.position);
        else
            selectedFloatUi = GetClosestFloatingUi(FilterList(false), touch.position);

        return selectedFloatUi;
    }

    /// <summary>
    /// Activates the floating ui element corotine for smooth transitions.
    /// </summary>
    public static void ActivateFloatingUiCorotine(FloatingUiController selectedFloatUi, Touch touch, bool goToTouch)
    {
        if (goToTouch)
        {
            selectedFloatUi.FloatingUiCorotine.ActivateAplhaToggle(selectedFloatUi.AlphaToggle, 1, 1);
            selectedFloatUi.FloatingUiCorotine.ActivateMoveToTouch(selectedFloatUi.RectToMove, touch.position, 1);
        }
        else
        {
            selectedFloatUi.FloatingUiCorotine.ActivateAplhaToggle(selectedFloatUi.AlphaToggle, selectedFloatUi.TransparencyAmount, 1);
        }
    }
}
