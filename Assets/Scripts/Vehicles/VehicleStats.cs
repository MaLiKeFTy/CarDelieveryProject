using UnityEngine;

[System.Serializable]
public class VehicleStats
{
    #region Serialised Fields
    [SerializeField] float motorForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float breakForce;
    [Range(0.01f, 1.0f)]
    [SerializeField] float steeringValue = 0.5f;
    [SerializeField] Vector3 centreOfMass;
    #endregion

    #region Properties
    public float MotorForce => motorForce;
    public float MaxSpeed => maxSpeed;
    public float BreakForce => breakForce;
    public float SteeringValue => steeringValue;
    public Vector3 CentreOfMass => centreOfMass;
    #endregion
}
