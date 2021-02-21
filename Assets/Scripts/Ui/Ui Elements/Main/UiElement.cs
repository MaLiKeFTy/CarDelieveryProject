using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public abstract class UiElement : MonoBehaviour
{
    [SerializeField] protected bool isLeft; //if the ui element is in the left of the screen.

    protected RectTransform thisRect;

    public abstract RectTransform ThisRect { get; }
    public abstract bool IsLeft { get; }

    protected virtual void Awake() => thisRect = GetComponent<RectTransform>();
}
