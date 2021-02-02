using UnityEngine;

public class UiJoystick : MonoBehaviour
{
    #region Serialised Fields

    [Header("Refrences")]
    [SerializeField] RectTransform joystickBackgorund;
    [SerializeField] RectTransform joystickHandle;
    [SerializeField] CanvasGroup thisCanvasGroup;

    [Header("Stats")]
    [SerializeField] AxesTypes axisType = AxesTypes.Horizontal;

    [Range(0.0f, 1.0f)]
    [SerializeField] float joystickTransparency = 1.0f;
    #endregion

    #region Properties
    public RectTransform JoystickBackgorund => joystickBackgorund;
    public RectTransform JoystickHandle => joystickHandle;
    public CanvasGroup ThisCanvasGroup => thisCanvasGroup;
    public AxesTypes AxisType => axisType;
    public float JoystickTransparency => joystickTransparency;
    public UiJoystickAnimation JoystickAnimation { get; private set; }
    #endregion

    UiJoystickController joystickController;
    

    void Awake()
    {
        JoystickAnimation = new UiJoystickAnimation(CorotineActivator.instance);
        joystickController = new UiJoystickController(this);
    }

    void Update() => joystickController.Tick();
}
