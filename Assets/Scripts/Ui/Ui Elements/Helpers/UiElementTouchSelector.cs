using System.Collections.Generic;
using UnityEngine;

public static class UiElementTouchSelector<T> where T : UiElement
{
    public static HashSet<T> uiElements = new HashSet<T>();

    /// <summary>
    /// Gets the closest ui element to the touch position.
    /// </summary>
    static T GetClosestUiElement(HashSet<T> filteredUiElements, Vector2 touchPos)
    {
        T tMin = default(T);
        float minDist = Mathf.Infinity;
        Vector3 currentPos = touchPos;

        foreach (var uiElement in filteredUiElements)
        {
            //Get the Rect Transform of the uiElement.
            var rectTransformProp = uiElement.GetType().GetProperty("ThisRect");
            RectTransform thisRect = (RectTransform)(rectTransformProp.GetValue(uiElement, null));

            float dist = Vector3.Distance(thisRect.position, currentPos);

            if (dist < minDist)
            {
                tMin = uiElement;
                minDist = dist;
            }
        }

        return tMin;
    }

    /// <summary>
    /// Only gets the ui elements that is on the left or of the right.
    /// </summary>
    static HashSet<T> FilterUiElements(bool isLeft)
    {
        HashSet<T> tempUiElement = new HashSet<T>();

        foreach (var uiElement in uiElements)
        {
            var isLeftProp = uiElement.GetType().GetProperty("IsLeft");
            bool isLeftprop = (bool)(isLeftProp.GetValue(uiElement, null));

            if (isLeftprop == isLeft)
                tempUiElement.Add(uiElement);
        }

        return tempUiElement;
    }

    /// <summary>
    /// Gets the selected ui element by the touch.
    /// </summary>
    public static T SelectedUiElement(Touch touch)
    {
        T selectedUielement;

        var touchIsLeftOfscreen = touch.position.x <= Screen.width / 2 ? true : false;
        selectedUielement = GetClosestUiElement(FilterUiElements(touchIsLeftOfscreen), touch.position);

        return selectedUielement;
    }
}
