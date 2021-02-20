using System.Collections.Generic;
using UnityEngine;

public static class JoystickSelector
{
    public static HashSet<UiJoystickController> joysticks = new HashSet<UiJoystickController>();

    /// <summary>
    /// Gets the closest floating ui element to the touch position 
    /// </summary>
    static UiJoystickController GetClosestFloatingUi(HashSet<UiJoystickController> floatingUiList, Vector2 touchPos)
    {
        UiJoystickController tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = touchPos;
        foreach (UiJoystickController t in floatingUiList)
        {
            float dist = Vector3.Distance(t.JoystickBackgorund.position, currentPos);
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
    static HashSet<UiJoystickController> FilterList(bool isLeft)
    {
        HashSet<UiJoystickController> tempPoints = new HashSet<UiJoystickController>();
        foreach (var item in joysticks)
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
    public static UiJoystickController SelectedJoystick(Touch touch)
    {
        UiJoystickController selectedFloatUi;

        if (touch.position.x <= Screen.width / 2)
            selectedFloatUi = GetClosestFloatingUi(FilterList(true), touch.position);
        else
            selectedFloatUi = GetClosestFloatingUi(FilterList(false), touch.position);

        return selectedFloatUi;
    }
}
