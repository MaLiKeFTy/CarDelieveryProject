using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FloatingUi : UiElement
{
    //This class is responsible for moving the ui element to the finger touch position on the screen.

    [Header("Stats")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float transparency = 1.0f;

    public CanvasGroup ThisCanvasGroup { get; private set; }
    public float Transparency => transparency;

    public override RectTransform ThisRect => thisRect;

    public override bool IsLeft => isLeft;

    FloatingUiController floatingUiController;

    protected override void Awake() => Initialize();

    void Update() => floatingUiController.MoveUiElementToTouch();

    void Initialize()
    {
        base.Awake();
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
