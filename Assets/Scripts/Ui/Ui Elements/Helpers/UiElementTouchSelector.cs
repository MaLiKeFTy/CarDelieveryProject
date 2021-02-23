using System.Collections.Generic;
using UnityEngine;

public static class UiElementTouchSelector
{
    public static HashSet<UiElement> uiElements = new HashSet<UiElement>();

    /// <summary>
    /// Gets the closest ui element to the touch position.
    /// </summary>
    static UiElement GetClosestUiElement(HashSet<UiElement> filteredUiElements, Vector2 touchPos)
    {
        UiElement tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPos = touchPos;


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
    static HashSet<UiElement> FilterUiElements(bool isLeft)
    {
        HashSet<UiElement> tempUiElement = new HashSet<UiElement>();
        
        foreach (var uiElement in uiElements)
        {
            var isLeftProp = uiElement.GetType().GetProperty("IsLeft");
            bool isLeftprop = (bool)(isLeftProp.GetValue(uiElement, null));

            if (isLeftprop == isLeft)
                tempUiElement.Add(uiElement);
        }

        /*foreach (var item in tempUiElement)
        {
            Debug.Log(item);
        }*/
        return tempUiElement;
    }

    /// <summary>
    /// Gets the selected ui element by the touch.
    /// </summary>
    public static UiElement SelectedUiElement(Touch touch)
    {
        UiElement selectedUielement;
        
        var touchIsLeftOfscreen = touch.position.x <= Screen.width / 2 ? true : false;
        selectedUielement = GetClosestUiElement(FilterUiElements(touchIsLeftOfscreen), touch.position);
        
        
        return selectedUielement;
    }
}
