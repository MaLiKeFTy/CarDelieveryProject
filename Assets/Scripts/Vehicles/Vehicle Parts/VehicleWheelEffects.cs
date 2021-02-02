using UnityEngine;

[System.Serializable]
public struct VehicleWheelEffects
{
    [SerializeField] TrailRenderer typeTrail;

    public TrailRenderer TypeTrail { get { return typeTrail; } set { typeTrail = value; } }
}
