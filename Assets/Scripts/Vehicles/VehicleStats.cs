using UnityEngine;

[System.Serializable]
public struct VehicleStats
{
    #region Serialised Fields
    [SerializeField] float motorForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float breakForce;
    [SerializeField] float steeringMultiplier;
    [SerializeField] Vector3 centreOfMass;
    #endregion

    #region Properties
    public float MotorForce => motorForce;
    public float MaxSpeed => maxSpeed;
    public float BreakForce => breakForce;
    public float SteeringMultiplier => steeringMultiplier;
    public Vector3 CentreOfMass => centreOfMass;
    #endregion
}
