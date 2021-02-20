using System.Collections;
using UnityEngine;

public class UiJoystick : MonoBehaviour
{
    #region Serialised Fields

    [Header("Refrences")]
    [SerializeField] RectTransform joystickBackgorund;
    [SerializeField] RectTransform joystickHandle;

    [Space]

    [Header("Stats")]
    [SerializeField] AxesTypes axisType = AxesTypes.Horizontal;
    [SerializeField] bool isLeft;
    [SerializeField] float elacticityValue = 8f;
    [SerializeField] bool floatingJoystick;
    [Range(0.1f, 1.0f)]
    [SerializeField] float sensitivity = 1;

    #endregion

    #region Properties
    public RectTransform JoystickBackgorund => joystickBackgorund;
    public RectTransform JoystickHandle => joystickHandle;
    public AxesTypes AxisType => axisType;
    public bool IsLeft => isLeft;
    public float ElacticityValue => elacticityValue;
    public float Sensitivity => sensitivity;
    #endregion

    UiJoystickController uiJoystickController;

    void Awake() => uiJoystickController = new UiJoystickController(this);

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
