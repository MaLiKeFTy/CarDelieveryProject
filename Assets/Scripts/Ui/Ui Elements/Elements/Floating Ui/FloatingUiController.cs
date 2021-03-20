using UnityEngine;

public class FloatingUiController
{
    FloatingUi thisFloatingUi;

    public FloatingUiController(FloatingUi floatingUi) => thisFloatingUi = floatingUi;

    public void MoveUiElementToTouch()
    {
        OnTouchDown();
        OnTouchUp();
    }

    void OnTouchDown()
    {
        UiElementTouchSelector.uiElements.Add(thisFloatingUi);

        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Began))
        {
            MoveSelectedFloatingUi(touch, true);
        }
    }

    void OnTouchUp()
    {
        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Ended))
        {
            MoveSelectedFloatingUi(touch, false);
        }
    }

    /// <summary>
    /// Gets the selected Float UI and activates the corotine.
    /// </summary>
    void MoveSelectedFloatingUi(Touch touch, bool goToTouch)
    {
        var selectedFloatUi = UiElementTouchSelector.SelectedUiElement(touch.position);
       // ActivateFloatingUiCorotine(selectedFloatUi, touch, goToTouch);
    }

    /// <summary>
    /// Activates the floating ui element corotine for smooth transitions.
    /// </summary>
    void ActivateFloatingUiCorotine(FloatingUi selectedFloatUi, Touch touch, bool goToTouch)
    {
        if (goToTouch)
        {
            selectedFloatUi.FloatingUiCorotine.ActivateAplhaToggle(selectedFloatUi.ThisCanvasGroup, 1, 1);
            selectedFloatUi.FloatingUiCorotine.ActivateMoveToTouch(selectedFloatUi.ThisRect, touch.position, 1);
        }
        else
        {
            selectedFloatUi.FloatingUiCorotine.ActivateAplhaToggle(selectedFloatUi.ThisCanvasGroup, selectedFloatUi.Transparency, 1);
        }
    }

}
