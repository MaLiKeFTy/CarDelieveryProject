using System;
using UnityEngine;

public class FloatingUiController
{
    readonly FloatingUiCorotine floatingUiCorotine;
    readonly RectTransform rectToMove;
    readonly CanvasGroup alphaToggle;
    readonly float transparencyAmount;
    readonly bool isLeft;

    TouchesManager touchesManager = new TouchesManager();

    public bool IsLeft => isLeft;
    public RectTransform RectToMove => rectToMove;

    public static Action<string> TouchedEvent;

    public FloatingUiController(FloatingUi floatingUi)
    {
        floatingUiCorotine = new FloatingUiCorotine(floatingUi);
        rectToMove = floatingUi.ThisRect;
        alphaToggle = floatingUi.ThisCanvasGroup;
        transparencyAmount = floatingUi.Transparency;
        isLeft = floatingUi.IsLeft;
    }

    public void FloatUi()
    {
        OnTouchDown();
        OnTouchUp();
    }

    void OnTouchDown()
    {
        FloatingUiProcessor.floatingGameObjs.Add(this);

        foreach (var touch in touchesManager.GetTouches(TouchPhase.Began))
        {

            FloatingUiController closestFloatingUi;

            if (touch.position.x <= Screen.width / 2)
                closestFloatingUi = FloatingUiProcessor.GetClosestFloatingUi(FloatingUiProcessor.FilterList(true), touch.position);
            else
                closestFloatingUi = FloatingUiProcessor.GetClosestFloatingUi(FloatingUiProcessor.FilterList(false), touch.position);


            closestFloatingUi.floatingUiCorotine.ActivateAplhaToggle(closestFloatingUi.alphaToggle, 1, 1);
            closestFloatingUi.floatingUiCorotine.ActivateMoveToTouch(closestFloatingUi.rectToMove, touch.position, 1);

        }
    }


    void OnTouchUp()
    {
        foreach (var touch in touchesManager.GetTouches(TouchPhase.Ended))
        {
            FloatingUiController closestFloatingUi;

            if (touch.position.x <= Screen.width / 2)
                closestFloatingUi = FloatingUiProcessor.GetClosestFloatingUi(FloatingUiProcessor.FilterList(true), touch.position);
            else
                closestFloatingUi = FloatingUiProcessor.GetClosestFloatingUi(FloatingUiProcessor.FilterList(false), touch.position);

            closestFloatingUi.floatingUiCorotine.ActivateAplhaToggle(closestFloatingUi.alphaToggle, transparencyAmount, 1);
        }
    }
}
