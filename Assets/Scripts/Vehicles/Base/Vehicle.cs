using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    #region Serialised Fields
    [SerializeField] VehicleParts vehicleParts;
    [SerializeField] VehicleStats vehicleStats;
    #endregion

    #region Parameters
    public VehicleParts VehicleParts => vehicleParts;
    public VehicleStats VehicleStats => vehicleStats;
    public VehicleState VehicleState { get; protected set; }
    #endregion

}
