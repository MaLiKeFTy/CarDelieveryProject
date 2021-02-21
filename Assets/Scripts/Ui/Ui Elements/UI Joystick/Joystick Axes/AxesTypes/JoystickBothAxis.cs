using UnityEngine;

public class JoystickBothAxis : JoystickAxes
{
    public override AxesTypes AxesTypes => AxesTypes.Both;

    public override Vector2 AxisSelection(Vector2 goToTarget, UiJoystick selectedJoystick)
    {
        goToTarget = goToTarget * selectedJoystick.Sensitivity;
        return Vector2.ClampMagnitude(goToTarget, selectedJoystick.ThisRect.rect.width / 2);
    }
}
