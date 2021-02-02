
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
        WheelRollingCheck();
    }

    void ReverseCheck()
    {
        var vehicleDirection = Vector3.Dot(vehicleTrans.forward, vehicleRb.velocity);

        IsReversing = vehicleDirection < -1 ? true : false;
    }

    void GroundCheck()
    {
        foreach (var wheel in wheelColliders)
        {
            IsGrounded = wheel.wheelCollider.isGrounded ? true : false;
        }
    }

    void WheelRollingCheck()
    {
        foreach (var wheel in wheelColliders)
        {
            var wheelModifiedRPM = wheel.wheelCollider.rpm / 100;

            //To check if the wheel is rolling faster than the car velocity
            wheel.IsRolling = vehicleRb.velocity.magnitude < wheelModifiedRPM ? true : false;
        }
    }

}
