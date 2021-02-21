using System;
using System.Collections.Generic;
using UnityEngine;

public class UiJoystickController
{
    #region Read Only fields
    readonly float elacticityValue;


    public bool IsLeft { get; }
    public RectTransform JoystickBackgorund { get; }
    public RectTransform JoystickHandle { get; }
    public AxesTypes AxisType { get; } = AxesTypes.Horizontal;
    public float Sensitivity { get; }


    UiJoystick thisJoystick;

    UiJoystick selectedJoystick;
    HashSet<UiJoystick> selectedJoysticks = new HashSet<UiJoystick>();
    #endregion

    float currrentJoystickXValue;
    float currrentJoystickYValue;

    #region Constructor
    public UiJoystickController(UiJoystick uiJoystick)
    {
        thisJoystick = uiJoystick;
        JoystickBackgorund = uiJoystick.ThisRect;
        JoystickHandle = uiJoystick.JoystickHandle;
        AxisType = uiJoystick.AxisType;
        IsLeft = uiJoystick.IsLeft;
        elacticityValue = uiJoystick.ElacticityValue;
        Sensitivity = uiJoystick.Sensitivity;
        VehicleController.SteeringCallBack += () => { return currrentJoystickXValue / 4; };
        VehicleController.AccelerationCallBack += () => { return currrentJoystickYValue / 200; };
    }
    #endregion

    public void Tick()
    {
        OnTouch();
    }

    void OnTouch()
    {
        UiElementTouchSelector<UiJoystick>.uiElements.Add(thisJoystick);

        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Began))
        {
            selectedJoystick = UiElementTouchSelector<UiJoystick>.SelectedUiElement(touch);
            selectedJoystick.StartPosition = touch.position;
            selectedJoystick.FingerID = touch.fingerId;
            selectedJoystick.BackToCentre = false;
            selectedJoysticks.Add(selectedJoystick);

        }

        CurrentSelectedJoysticks(TouchPhase.Moved, JoystickHandleMovement);
        CurrentSelectedJoysticks(TouchPhase.Ended, (currentJoystick, touch) => currentJoystick.BackToCentre = true);

        if (Input.touchCount == 0)
            selectedJoysticks.Clear();

        if (thisJoystick.BackToCentre)
            JoystickHandle.anchoredPosition = Vector2.Lerp(JoystickHandle.anchoredPosition, Vector2.zero, elacticityValue * Time.deltaTime);

        if(selectedJoystick != null)
        {
            currrentJoystickXValue = selectedJoystick.JoystickHandle.anchoredPosition.x;
            currrentJoystickYValue = selectedJoystick.JoystickHandle.anchoredPosition.y;
        }
       
       // Debug.Log(currrentJoystickYValue);
    }


    void CurrentSelectedJoysticks(TouchPhase touchPhase, Action<UiJoystick, Touch> selectedJoystickEvnt)
    {
        foreach (var touch in TouchesManager.GetTouches(touchPhase))
            foreach (var selectedJoystick in selectedJoysticks)
                if (selectedJoystick.FingerID == touch.fingerId)
                {
                    selectedJoystickEvnt?.Invoke(selectedJoystick, touch);
                }
    }

    void JoystickHandleMovement(UiJoystick selectedJoystick, Touch touch)
    {
        var offset = touch.position - selectedJoystick.StartPosition;
        Vector2 target = JoystickAxesPeocessor.GetAxisTarget(offset, selectedJoystick);
        selectedJoystick.JoystickHandle.anchoredPosition = target;
      //  currrentJoystickValue = target.x;
    }


}
