using UnityEngine;

public class JoystickVerticalAxis : JoystickAxes
{
    public override AxesTypes AxesTypes => AxesTypes.Vertical;

    public override Vector2 AxisSelection(Vector2 goToTarget, UiJoystick selectedJoystick)
    {
        Vector2 verticalTargetOffset = new Vector2(0, goToTarget.y) * selectedJoystick.Sensitivity;
        var clampedDistance = Vector2.ClampMagnitude(verticalTargetOffset, selectedJoystick.ThisRect.rect.height / 2);
        selectedJoystick.InputValue = clampedDistance.y / (selectedJoystick.ThisRect.rect.height / 2);
        return clampedDistance;
    }
}
