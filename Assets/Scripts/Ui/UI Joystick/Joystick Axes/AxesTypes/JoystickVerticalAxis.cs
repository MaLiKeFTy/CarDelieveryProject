using UnityEngine;

public class JoystickVerticalAxis : JoystickAxes
{
    public override AxesTypes AxesTypes => AxesTypes.Vertical;

    public override Vector2 AxisSelection(RectTransform joystickRect, RectTransform backgroundRect)
    {
        Vector2 targetOffset = Input.mousePosition - backgroundRect.position;
        Vector2 verticalTargetOffset = new Vector2(0, targetOffset.y);
        return Vector2.ClampMagnitude(verticalTargetOffset, backgroundRect.rect.width / 2);
    }
}
