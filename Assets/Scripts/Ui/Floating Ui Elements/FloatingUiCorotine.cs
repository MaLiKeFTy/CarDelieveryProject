using System.Collections;
using UnityEngine;

public class FloatingUiCorotine
{
    readonly FloatingUi corotineActivator;
    IEnumerator moveToTouchCorotine;
    IEnumerator alphaToggleCorotine;

    public FloatingUiCorotine(FloatingUi corotineActivator) => this.corotineActivator = corotineActivator;

    public void ActivateAplhaToggle(CanvasGroup canvasGroup, float to, float time)
    {
        corotineActivator.ActivateCorotine(AplhaToggle(canvasGroup, to, time), ref alphaToggleCorotine);
    }

    public void ActivateMoveToTouch(RectTransform joystickRect, Vector2 to, float time)
    {
        corotineActivator.ActivateCorotine(MoveToTouch(joystickRect, to, time), ref moveToTouchCorotine);
    }

    IEnumerator AplhaToggle(CanvasGroup canvasGroup, float to, float time)
    {
        float ElapcedTime = 0;
        while (ElapcedTime < time)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, to, ElapcedTime / time);
            ElapcedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator MoveToTouch(RectTransform joystickRect, Vector2 to, float time)
    {
        float ElapcedTime = 0;
        while (ElapcedTime < time)
        {
            joystickRect.position = Vector2.Lerp(joystickRect.position, to, ElapcedTime / time);
            ElapcedTime += Time.deltaTime;
            yield return null;
        }
    }
}
