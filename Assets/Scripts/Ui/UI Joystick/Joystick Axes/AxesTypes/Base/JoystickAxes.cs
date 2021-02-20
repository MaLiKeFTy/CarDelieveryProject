using UnityEngine;

public abstract class JoystickAxes
{
    public abstract AxesTypes AxesTypes { get; }
    public abstract Vector2 AxisSelection(Vector2 goToTarget, UiJoystickController selectedJoystick);
}
