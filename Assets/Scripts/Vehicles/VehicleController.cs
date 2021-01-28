using System;
using UnityEngine;

public class VehicleController
{
    readonly IVehicleInput vehicleInput;
    readonly Vehicle vehicle;
    readonly VehicleWheel[] vehicleWheels;

    float currentSteerAngle;
    event Action RunMotorCallback;

    public VehicleController(Vehicle vehicle, IVehicleInput vehicleInput)
    {
        this.vehicle = vehicle;
        this.vehicleInput = vehicleInput;
        vehicleWheels = vehicle.VehicleParts.VehicleWheels;
        vehicle.VehicleParts.VehicleRb.centerOfMass = vehicle.VehicleStats.CentreOfMass;
        RunMotorCall();
    }

    /// <summary>
    /// Subscrabing all RunMotor method to invoke them all at once.
    /// </summary>
    void RunMotorCall()
    {
        RunMotorCallback += ApplyAcceleration;
        RunMotorCallback += ApplySteering;
        RunMotorCallback += UpdateWheels;
    }

    public void RunMotor()
    {
        RunMotorCallback?.Invoke();
    }

    void ApplyAcceleration()
    {
        foreach (var wheel in vehicleWheels)
        {
            if (wheel.wheelPlacement == VehiclePartsPlacements.Back)
                wheel.wheelCollider.motorTorque = vehicleInput.Acceleration * vehicle.VehicleStats.MotorForce;
        }
    }

    void ApplySteering()
    {
        currentSteerAngle = vehicle.VehicleStats.MaxSteeringAngle * vehicleInput.Turn;
        foreach (var wheel in vehicleWheels)
        {
            if (wheel.wheelPlacement == VehiclePartsPlacements.Front)
                wheel.wheelCollider.steerAngle = currentSteerAngle;
        }
    }

    void UpdateWheels()
    {
        foreach (var wheel in vehicleWheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.transform.GetChild(0).rotation = rot;
            wheel.transform.GetChild(0).position = pos;
        }
    }
}
