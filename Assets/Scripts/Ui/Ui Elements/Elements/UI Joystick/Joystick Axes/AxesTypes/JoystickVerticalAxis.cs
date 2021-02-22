using UnityEngine;

public class JoystickVerticalAxis : JoystickAxes
{
    public override AxesTypes AxesTypes => AxesTypes.Vertical;

    public override Vector2 AxisSelection(Vector2 goToTarget, UiJoystick selectedJoystick)
    {
        Vector2 verticalTargetOffset = new Vector2(0, goToTarget.y) * selectedJoystick.Sensitivity;
        selectedJoystick.InputValue = verticalTargetOffset.y;
        return Vector2.ClampMagnitude(verticalTargetOffset, selectedJoystick.ThisRect.rect.width / 2);
    }
}
