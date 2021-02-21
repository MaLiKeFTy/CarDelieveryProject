using UnityEngine;

public class JoystickHorizontalAxis : JoystickAxes
{
    public override AxesTypes AxesTypes => AxesTypes.Horizontal;

    public override Vector2 AxisSelection(Vector2 goToTarget, UiJoystick selectedJoystick)
    {
        Vector2 horizontalTargetOffset = new Vector2(goToTarget.x, 0) * selectedJoystick.Sensitivity;
        return Vector2.ClampMagnitude(horizontalTargetOffset, selectedJoystick.ThisRect.rect.width / 2);
    }
}
