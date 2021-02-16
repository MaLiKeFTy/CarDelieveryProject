using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class FloatingUi : MonoBehaviour
{
    //This class is responsible for moving the ui element to the finger touch position on the screen.

    [Header("Refrences")]
    [SerializeField] RectTransform thisRect;
    [SerializeField] CanvasGroup thisCanvasGroup;

    [Space]

    [Header("Stats")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float transparency = 1.0f;
    [SerializeField] bool isLeft;

    public RectTransform ThisRect => thisRect;
    public CanvasGroup ThisCanvasGroup => thisCanvasGroup;
    public float Transparency => transparency;
    public bool IsLeft => isLeft;

    FloatingUiController floatingUiController;

    void Awake() => floatingUiController = new FloatingUiController(this);

    void Update() => floatingUiController.MoveUiElementToTouch();

    /// <summary>
    /// This starts the ui element movement.
    /// </summary>
    /// <param name="original">The current corotine.</param>
    /// <param name="corotineToStop">The corotine that needs to be stopped if its still running.</param>
    public void ActivateCorotine(IEnumerator original, ref IEnumerator corotineToStop)
    {
        if (corotineToStop != null)
        {
            StopCoroutine(corotineToStop);
        }
        corotineToStop = original;
        StartCoroutine(corotineToStop);
    }
}
