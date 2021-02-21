using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public abstract class UiElement : MonoBehaviour
{
    [SerializeField] protected bool isLeft;

    protected RectTransform thisRect;

    public abstract RectTransform ThisRect { get; }
    public abstract bool IsLeft { get; }

    protected virtual void Awake() => thisRect = GetComponent<RectTransform>();
}
