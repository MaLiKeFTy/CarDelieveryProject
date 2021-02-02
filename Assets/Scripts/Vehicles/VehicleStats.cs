using UnityEngine;

[System.Serializable]
public struct VehicleStats
{
    #region Serialised Fields
    [SerializeField] float motorForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float breakForce;
    [SerializeField] float maxSteeringAngle;
    [SerializeField] Vector3 centreOfMass;
    #endregion

    #region Parameters
    public float MotorForce => motorForce;
    public float MaxSpeed => maxSpeed;
    public float BreakForce => breakForce;
    public float MaxSteeringAngle => maxSteeringAngle;
    public Vector3 CentreOfMass => centreOfMass;
    #endregion
}
