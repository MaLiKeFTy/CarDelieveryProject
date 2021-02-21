using UnityEngine;

public class FloatingUiController
{
    public bool IsLeft { get; } //if the touch position is in the left of the screen.
    public RectTransform RectToMove { get; }
    public float TransparencyAmount { get; }
    public FloatingUiCorotine FloatingUiCorotine { get; } //this is responsible for the floating ui element animation.
    public CanvasGroup AlphaToggle { get; }

    public FloatingUiController(FloatingUi floatingUi)
    {
        FloatingUiCorotine = new FloatingUiCorotine(floatingUi);
        RectToMove = floatingUi.ThisRect;
        AlphaToggle = floatingUi.ThisCanvasGroup;
        TransparencyAmount = floatingUi.Transparency;
        IsLeft = floatingUi.IsLeft;
    }

    public void MoveUiElementToTouch()
    {
        OnTouchDown();
        OnTouchUp();
    }

    void OnTouchDown()
    {
        FloatingUiProcessor.floatingGameObjs.Add(this);

        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Began))
        {
            var selectedFloatUi = FloatingUiProcessor.SelectedFloatUi(touch);
            FloatingUiProcessor.ActivateFloatingUiCorotine(selectedFloatUi, touch, true);
        }
    }

    void OnTouchUp()
    {
        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Ended))
        {
            var selectedFloatUi = FloatingUiProcessor.SelectedFloatUi(touch);
            FloatingUiProcessor.ActivateFloatingUiCorotine(selectedFloatUi, touch, false);
        }
    }
}
