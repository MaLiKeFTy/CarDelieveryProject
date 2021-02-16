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

    #endregion

    #region Properties
    public RectTransform JoystickBackgorund => joystickBackgorund;
    public RectTransform JoystickHandle => joystickHandle;
    public AxesTypes AxisType => axisType;
    #endregion

    UiJoystickController uiJoystickController;

    void Awake() => uiJoystickController = new UiJoystickController(this);

    void Update() => uiJoystickController.Tick();

}
