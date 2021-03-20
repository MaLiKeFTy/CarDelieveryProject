using System;
using System.Collections.Generic;
using System.Linq;
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


#if UNITY_EDITOR
    public void Tick() => OnMouse();
#else
    public void Tick() => OnTouch();
#endif

    void OnTouch()
    {
        UiElementTouchSelector.uiElements.Add(thisJoystick);
        foreach (var touch in TouchesManager.GetTouches(TouchPhase.Began))
        {
            var selectedUiElement = UiElementTouchSelector.SelectedUiElement(touch.position);
            if (selectedUiElement is UiJoystick)
            {
                selectedJoystick = (UiJoystick)selectedUiElement;
                selectedJoystick.StartPosition = touch.position;
                selectedJoystick.FingerID = touch.fingerId;
                selectedJoystick.BackToCentre = false;
                selectedJoysticks.Add(selectedJoystick);
            }
        }

        CurrentSelectedJoysticks(TouchPhase.Moved, JoystickHandleMovement);
        CurrentSelectedJoysticks(TouchPhase.Ended, ActivateRestJoystick);

        if (Input.touchCount == 0)
            selectedJoysticks.Clear();

        ResetJoystickPos();
    }

    void OnMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UiElementTouchSelector.uiElements.Add(thisJoystick);
            var selectedUiElement = UiElementTouchSelector.SelectedUiElement(Input.mousePosition);
            if (selectedUiElement is UiJoystick)
            {
                selectedJoystick = (UiJoystick)selectedUiElement;
                selectedJoystick.StartPosition = Input.mousePosition;
                selectedJoystick.BackToCentre = false;
                selectedJoysticks.Add(selectedJoystick);
            }
        }

        ResetJoystickPos();

        if (!selectedJoystick)
            return;

        if (Input.GetMouseButton(0))
            JoystickHandleMovement(selectedJoystick, Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            ActivateRestJoystick(selectedJoystick, Input.mousePosition);
            selectedJoystick = null;
        }
           
    }


    void CurrentSelectedJoysticks(TouchPhase touchPhase, Action<UiJoystick, Vector2> selectedJoystickEvnt)
    {
        foreach (var touch in TouchesManager.GetTouches(touchPhase))
            foreach (var selectedJoystick in selectedJoysticks.ToList())
                if (selectedJoystick.FingerID == touch.fingerId)
                    selectedJoystickEvnt?.Invoke(selectedJoystick, touch.position);
    }

    void JoystickHandleMovement(UiJoystick selectedJoystick, Vector2 touchPos)
    {
        var offset = touchPos - selectedJoystick.StartPosition;
        Vector2 target = JoystickAxesPeocessor.GetAxisTarget(offset, selectedJoystick);
        selectedJoystick.JoystickHandle.anchoredPosition = target;
    }


    void ActivateRestJoystick(UiJoystick selectedJoystick, Vector2 touchPos)
    {
        selectedJoystick.BackToCentre = true;
        selectedJoysticks.Remove(selectedJoystick);
    }

    void ResetJoystickPos()
    {
        if (!thisJoystick.BackToCentre)
            return;
        thisJoystick.JoystickHandle.anchoredPosition = Vector2.Lerp(thisJoystick.JoystickHandle.anchoredPosition, Vector2.zero, thisJoystick.ElacticityValue * Time.deltaTime);
        thisJoystick.InputValue = Mathf.Lerp(thisJoystick.InputValue, 0, thisJoystick.ElacticityValue * Time.deltaTime);
    }


}
