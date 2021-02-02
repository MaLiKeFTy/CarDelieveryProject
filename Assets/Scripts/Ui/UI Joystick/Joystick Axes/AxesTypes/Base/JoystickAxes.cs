using UnityEngine;

public abstract class JoystickAxes
{
    public abstract AxesTypes AxesTypes { get; }
    public abstract Vector2 AxisSelection(RectTransform joystickRect, RectTransform backgroundRect);
}
