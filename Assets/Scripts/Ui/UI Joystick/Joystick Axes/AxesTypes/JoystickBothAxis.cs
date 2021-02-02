using UnityEngine;

public class JoystickBothAxis : JoystickAxes
{
    public override AxesTypes AxesTypes => AxesTypes.Both;

    public override Vector2 AxisSelection(RectTransform joystickRect, RectTransform backgroundRect)
    {
        Vector2 targetOffset = Input.mousePosition - backgroundRect.position;
        return Vector2.ClampMagnitude(targetOffset, backgroundRect.rect.width / 2);
    }
}
