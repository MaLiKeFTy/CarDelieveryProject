
using UnityEngine;
public class VehicleState
{

    readonly Transform vehicleTrans;
    readonly VehicleWheel[] wheelColliders;
    readonly Rigidbody vehicleRb;

    public bool IsReversing { get; private set; }
    public bool IsGrounded { get; private set; }

    public VehicleState(Transform vehicleTrans, VehicleWheel[] wheelColliders)
    {
        this.vehicleTrans = vehicleTrans;
        this.wheelColliders = wheelColliders;
        vehicleRb = vehicleTrans.GetComponent<Rigidbody>();
    }

    public void DeployState()
    {
        ReverseCheck();
        GroundCheck();
    }

    void ReverseCheck()
    {
        var vehicleDirection = Vector3.Dot(vehicleTrans.forward, vehicleRb.velocity);

        if (vehicleDirection < -1)
            IsReversing = true;
        else
            IsReversing = false;
    }

    void GroundCheck()
    {
        foreach (var wheel in wheelColliders)
        {
            if (wheel.wheelCollider.isGrounded)
                IsGrounded = true;
            else
                IsGrounded = false;
        }
    }
}
