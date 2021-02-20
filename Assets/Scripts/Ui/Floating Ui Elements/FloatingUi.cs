using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class FloatingUi : MonoBehaviour
{
    //This class is responsible for moving the ui element to the finger touch position on the screen.

    [Header("Stats")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float transparency = 1.0f;
    [SerializeField] bool isLeft;

    public RectTransform ThisRect { get; private set; }
    public CanvasGroup ThisCanvasGroup { get; private set; }
    public float Transparency => transparency;
    public bool IsLeft => isLeft;

    FloatingUiController floatingUiController;

    void Awake() => Initialize();

    void Update() => floatingUiController.MoveUiElementToTouch();

    void Initialize()
    {
        ThisRect = GetComponent<RectTransform>();
        ThisCanvasGroup = GetComponent<CanvasGroup>();
        floatingUiController = new FloatingUiController(this);
    }

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
