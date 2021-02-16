using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class FloatingUi : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] RectTransform thisRect;
    [SerializeField] CanvasGroup thisCanvasGroup;


    [Header("Stats")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float transparency = 1.0f;
    [SerializeField] bool isLeft;


    public RectTransform ThisRect => thisRect;
    public CanvasGroup ThisCanvasGroup => thisCanvasGroup;
    public float Transparency => transparency;
    public bool IsLeft => isLeft;


    FloatingUiController floatingUiController;
    void Awake()
    {
        floatingUiController = new FloatingUiController(this);
    }


    void Update()
    {
        floatingUiController.FloatUi();
    }

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
