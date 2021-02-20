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

    float fingerID = -99;
    Vector2 startPosition;
    UiJoystickController selectedJoystick;
    HashSet<UiJoystickController> selectedJoysticks = new HashSet<UiJoystickController>();
    bool backToCentre;
    #endregion

    float currrentJoystickXValue;
    float currrentJoystickYValue;

    #region Constructor
    public UiJoystickController(UiJoystick uiJoystick)
    {
        JoystickBackgorund = uiJoystick.JoystickBackgorund;
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
        JoystickSelector.joysticks.Add(this);

        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Began))
        {
            selectedJoystick = JoystickSelector.SelectedJoystick(touch);
            selectedJoystick.startPosition = touch.position;
            selectedJoystick.fingerID = touch.fingerId;
            selectedJoystick.backToCentre = false;
            selectedJoysticks.Add(selectedJoystick);

        }

        CurrentSelectedJoysticks(TouchPhase.Moved, JoystickHandleMovement);
        CurrentSelectedJoysticks(TouchPhase.Ended, (currentJoystick, touch) => currentJoystick.backToCentre = true);

        if (Input.touchCount == 0)
            selectedJoysticks.Clear();

        if (backToCentre)
            JoystickHandle.anchoredPosition = Vector2.Lerp(JoystickHandle.anchoredPosition, Vector2.zero, elacticityValue * Time.deltaTime);

        if(selectedJoystick != null)
        {
            currrentJoystickXValue = selectedJoystick.JoystickHandle.anchoredPosition.x;
            currrentJoystickYValue = selectedJoystick.JoystickHandle.anchoredPosition.y;
        }
       
       // Debug.Log(currrentJoystickYValue);
    }


    void CurrentSelectedJoysticks(TouchPhase touchPhase, Action<UiJoystickController, Touch> selectedJoystickEvnt)
    {
        foreach (var touch in TouchesManager.GetTouches(touchPhase))
            foreach (var selectedJoystick in selectedJoysticks)
                if (selectedJoystick.fingerID == touch.fingerId)
                {
                    selectedJoystickEvnt?.Invoke(selectedJoystick, touch);
                }
    }

    void JoystickHandleMovement(UiJoystickController selectedJoystick, Touch touch)
    {
        var offset = touch.position - selectedJoystick.startPosition;
        Vector2 target = JoystickAxesPeocessor.GetAxisTarget(offset, selectedJoystick);
        selectedJoystick.JoystickHandle.anchoredPosition = target;
      //  currrentJoystickValue = target.x;
    }


}
