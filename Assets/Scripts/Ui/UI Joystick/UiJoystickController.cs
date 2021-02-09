using UnityEngine;

public class UiJoystickController
{
    #region Read Only fields
    readonly RectTransform joystickBackgorund;
    readonly RectTransform joystickHandle;
    readonly AxesTypes axisType = AxesTypes.Horizontal;

    #endregion

    float currrentJoystickValue;

    #region Constructor
    public UiJoystickController(UiJoystick uiJoystick)
    {
        joystickBackgorund = uiJoystick.JoystickBackgorund;
        joystickHandle = uiJoystick.JoystickHandle;
        axisType = uiJoystick.AxisType;

        VehicleController.SteeringCallBack += () => { return currrentJoystickValue / 4; };
    }
    #endregion

    public void Tick()
    {
        OnTouch();
    }

    void OnTouch()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 target = JoystickAxesPeocessor.GetAxisTarget(joystickHandle, joystickBackgorund, axisType);
            joystickHandle.anchoredPosition = target;

            var direction = Vector2.Dot(joystickBackgorund.anchoredPosition, joystickHandle.anchoredPosition) * -1;
            currrentJoystickValue = direction >= 0 ? target.magnitude : -target.magnitude;
            
        }
    }
}
