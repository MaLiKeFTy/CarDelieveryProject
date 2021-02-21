using System.Collections;
using UnityEngine;

public class UiJoystick : UiElement
{
    #region Serialised Fields

    [Header("Refrences")]
    [SerializeField] RectTransform joystickHandle;

    [Space]

    [Header("Stats")]
    [SerializeField] AxesTypes axisType = AxesTypes.Horizontal;
    [SerializeField] float elacticityValue = 8f;
    [SerializeField] bool floatingJoystick;
    [Range(0.1f, 1.0f)]
    [SerializeField] float sensitivity = 1;

    #endregion

    #region Properties
    public RectTransform JoystickHandle => joystickHandle;
    public AxesTypes AxisType => axisType;
    public float ElacticityValue => elacticityValue;
    public float Sensitivity => sensitivity;
    public float FingerID { get; set; } = -99;
    public Vector2 StartPosition { get; set; }
    public bool BackToCentre { get; set; }

    public override RectTransform ThisRect => thisRect;
    public override bool IsLeft => isLeft;
    #endregion

    UiJoystickController uiJoystickController;

    protected override void Awake() => Initialize();

    void Initialize()
    {
        base.Awake();
        uiJoystickController = new UiJoystickController(this);
    }

    void Update() => uiJoystickController.Tick();

    void OnValidate()
    {
        if (floatingJoystick && !GetComponent<FloatingUi>())
            StartCoroutine(ComponentToggle(true));
        else if(!floatingJoystick)
            StartCoroutine(ComponentToggle(false));

    }

    IEnumerator ComponentToggle(bool add)
    {
        yield return new WaitForEndOfFrame();
        if (add)
            gameObject.AddComponent<FloatingUi>();
        else
            DestroyImmediate(GetComponent<FloatingUi>());
    }
}
