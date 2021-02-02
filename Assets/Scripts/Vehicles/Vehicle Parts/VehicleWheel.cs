using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class VehicleWheel : MonoBehaviour
{
    [SerializeField] VehiclePartsPlacements placement;
    [SerializeField] VehicleWheelEffects wheelEffects;

    /// <summary>
    /// If wheel is rolling faster than the car's velocity
    /// </summary>
    public bool IsRolling { get; set; }

    public WheelCollider wheelCollider { get; set; }
    public VehiclePartsPlacements wheelPlacement { get { return placement; } private set { placement = value; } }
    public VehicleWheelEffects WheelEffects { get { return wheelEffects; } private set { wheelEffects = value; } }

    void Awake() => wheelCollider = GetComponent<WheelCollider>();
}
