using UnityEngine;

public class JoystickHorizontalAxis : JoystickAxes
{
    public override AxesTypes AxesTypes => AxesTypes.Horizontal;

    public override Vector2 AxisSelection(RectTransform joystickRect, RectTransform backgroundRect)
    {
        Vector2 targetOffset = Input.mousePosition - backgroundRect.position;
        Vector2 horizontalTargetOffset = new Vector2(targetOffset.x, 0);
        return Vector2.ClampMagnitude(horizontalTargetOffset, backgroundRect.rect.width / 2);
    }
}
