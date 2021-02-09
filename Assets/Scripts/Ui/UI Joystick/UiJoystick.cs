using UnityEngine;

public class UiJoystick : MonoBehaviour
{
    #region Serialised Fields

    [Header("Refrences")]
    [SerializeField] RectTransform joystickBackgorund;
    [SerializeField] RectTransform joystickHandle;
    [SerializeField] CanvasGroup thisCanvasGroup;
    
    [Space]

    [Header("Stats")]
    [SerializeField] AxesTypes axisType = AxesTypes.Horizontal;

    [Range(0.0f, 1.0f)]
    [SerializeField] float joystickTransparency = 1.0f;

    [SerializeField] bool isLeft;
    #endregion

    #region Properties
    public RectTransform JoystickBackgorund => joystickBackgorund;
    public RectTransform JoystickHandle => joystickHandle;
    public CanvasGroup ThisCanvasGroup => thisCanvasGroup;
    public AxesTypes AxisType => axisType;
    public float JoystickTransparency => joystickTransparency;
    #endregion

    FloatingUiController floatingJoystick;


    UiJoystickController uiJoystickController;

    void Awake()
    {
        floatingJoystick = new FloatingUiController(joystickBackgorund, thisCanvasGroup, joystickTransparency, isLeft);
        uiJoystickController = new UiJoystickController(this);
    }

    void Update()
    {
        floatingJoystick.FloatUi();
        uiJoystickController.Tick();
    }
        
}
