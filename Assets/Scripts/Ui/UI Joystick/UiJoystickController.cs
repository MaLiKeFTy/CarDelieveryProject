using System;
using UnityEngine;

public class UiJoystickController
{
    #region Read Only fields
    readonly RectTransform joystickBackgorund;
    readonly RectTransform joystickHandle;
    readonly CanvasGroup thisCanvasGroup;
    readonly AxesTypes axisType = AxesTypes.Horizontal;
    readonly float joystickTransparency;
    readonly UiJoystickAnimation joystickAnimation;
    #endregion

    event Action touchesEvent;

    #region Constructor
    public UiJoystickController(UiJoystick uiJoystick)
    {
        joystickBackgorund = uiJoystick.JoystickBackgorund;
        joystickHandle = uiJoystick.JoystickHandle;
        thisCanvasGroup = uiJoystick.ThisCanvasGroup;
        axisType = uiJoystick.AxisType;
        joystickTransparency = uiJoystick.JoystickTransparency;
        joystickAnimation = uiJoystick.JoystickAnimation;

        AddTouches();
    }
    #endregion

    /// <summary>
    /// Subscribing all the touches methods to invoke them all at once  
    /// </summary>
    void AddTouches()
    {
        touchesEvent += OnTouchDown;
        touchesEvent += OnTouch;
        touchesEvent += OnTouchUp;
    }

    public void Tick()
    {
        touchesEvent?.Invoke();
    }

    #region Touches Methods
    void OnTouchDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 achoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackgorund.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out achoredPos);
            if (Input.mousePosition.x < Screen.width / 2)
            {
                joystickAnimation.ActivateAplhaToggle(thisCanvasGroup, joystickTransparency, 1);
                joystickAnimation.ActivateMoveToTouch(joystickBackgorund, achoredPos, 1);
            }
        }
    }

    void OnTouch()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 target = JoystickAxesPeocessor.GetAxisTarget(joystickHandle, joystickBackgorund, axisType);
            joystickHandle.anchoredPosition = Vector2.Lerp(joystickHandle.anchoredPosition, target, 5 * Time.deltaTime);
        }
    }

    void OnTouchUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            joystickAnimation.ActivateAplhaToggle(thisCanvasGroup, 0, 1);
        }
    }
    #endregion
}
