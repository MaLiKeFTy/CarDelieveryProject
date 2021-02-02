using System.Collections;
using UnityEngine;

public class Uilearning : MonoBehaviour
{
    [SerializeField] RectTransform testImg;
    CanvasGroup canvasGroup;
    IEnumerator moveToMouseCorotine;
    IEnumerator alphaToggleCorotine;
    [SerializeField] RectTransform joystick;
    void Awake()
    {
        canvasGroup = testImg.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 achoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(testImg.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out achoredPos);
            ActivateCorotine(AlphaToggle(canvasGroup, 0.8f, 1), ref alphaToggleCorotine);
            ActivateCorotine(MoveToMouse(1, achoredPos), ref moveToMouseCorotine);
            
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 newPos = Input.mousePosition - joystick.position;
            // float Xpos = Mathf.Clamp(newPos.x, -testImg.rect.width / 2, testImg.rect.width / 2);
            Vector2 target = Vector2.ClampMagnitude(newPos, testImg.rect.width/2);
          //  Debug.Log();
            joystick.anchoredPosition = Vector2.Lerp(joystick.anchoredPosition, target, 3 * Time.deltaTime);
        }
        if (Input.GetMouseButtonUp(0))
        {
            ActivateCorotine(AlphaToggle(canvasGroup, 0, 1), ref alphaToggleCorotine);
        }
    }

    IEnumerator MoveToMouse(float time, Vector2 target)
    {
        float ElapcedTime = 0;
        while (ElapcedTime < time)
        {
            testImg.anchoredPosition = Vector2.Lerp(testImg.anchoredPosition, target, ElapcedTime / time);
            ElapcedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator AlphaToggle(CanvasGroup canvasGroup, float target, float time)
    {
        float ElapcedTime = 0;
        while (ElapcedTime < time)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, target, ElapcedTime / time);
            ElapcedTime += Time.deltaTime;
            yield return null;
        }
    }

    void ActivateCorotine(IEnumerator original, ref IEnumerator corotineToStop)
    {
        if (corotineToStop != null)
            StopCoroutine(corotineToStop);
        corotineToStop = original;
        StartCoroutine(corotineToStop);
    }

}
