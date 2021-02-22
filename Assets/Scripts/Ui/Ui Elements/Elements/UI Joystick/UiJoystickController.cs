using System;
using System.Collections.Generic;
using UnityEngine;

public class UiJoystickController
{
    #region Read Only fields
    readonly UiJoystick thisJoystick;

    UiJoystick selectedJoystick;

    public static HashSet<UiJoystick> selectedJoysticks = new HashSet<UiJoystick>();
    #endregion


    #region Constructor
    public UiJoystickController(UiJoystick uiJoystick) => thisJoystick = uiJoystick;
    #endregion

    public void Tick() => OnTouch();

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
        {
            thisJoystick.JoystickHandle.anchoredPosition = Vector2.Lerp(thisJoystick.JoystickHandle.anchoredPosition, Vector2.zero, thisJoystick.ElacticityValue * Time.deltaTime);
            thisJoystick.InputValue = Mathf.Lerp(thisJoystick.InputValue, 0, thisJoystick.ElacticityValue * Time.deltaTime);
        }
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
    }

}
