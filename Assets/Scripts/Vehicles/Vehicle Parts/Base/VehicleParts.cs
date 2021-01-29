using UnityEngine;

[System.Serializable]
public struct VehicleParts
{
    #region Serialised Fields
    [SerializeField] VehicleWheel[] vehicleWheels;
    [SerializeField] VehicleHeadLight[] vehicleHeadLights;
    [SerializeField] Rigidbody vehicleRb;
    #endregion

    #region Parameters
    public VehicleWheel[] VehicleWheels { get { return vehicleWheels; } set { vehicleWheels = value; } }
    public VehicleHeadLight[] VehicleHeadLights { get { return vehicleHeadLights; } set { vehicleHeadLights = value; } }
    public Rigidbody VehicleRb { get { return vehicleRb; } set { vehicleRb = value; } }
    public Transform VehicleTrans { get { return vehicleRb.GetComponent<Transform>(); }}
    #endregion
}
