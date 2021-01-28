using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class VehicleWheel : MonoBehaviour
{
    [SerializeField] VehiclePartsPlacements placement;

    public WheelCollider wheelCollider { get; set; }
    public VehiclePartsPlacements wheelPlacement { get { return placement; } private set { placement = value; } }

    void Awake()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }
}
