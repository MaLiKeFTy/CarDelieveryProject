using System;
using UnityEngine;

public class VehicleController
{
    readonly IVehicleInput vehicleInput;
    readonly VehicleParts vehicleParts;
    readonly VehicleStats vehicleStats;
    readonly VehicleState vehicleState;

    float currentSteerAngle;
    event Action RunMotorCallback;

   
    public VehicleController(Vehicle vehicle, IVehicleInput vehicleInput)
    {
        this.vehicleInput = vehicleInput;
        vehicleParts = vehicle.VehicleParts;
        vehicleStats = vehicle.VehicleStats;
        vehicleState = vehicle.VehicleState;
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
        RunMotorCallback += ApplyBreaking;
        RunMotorCallback += ApplyReversing;
    }

    public void RunMotor()
    {
        RunMotorCallback?.Invoke();
    }

    void ApplyAcceleration()
    {
        foreach (var wheel in vehicleParts.VehicleWheels)
        {
            if (wheel.wheelPlacement == VehiclePartsPlacements.Back)
            {
                wheel.wheelCollider.motorTorque = vehicleInput.Acceleration * vehicleStats.MotorForce;
                Debug.Log(Vector3.Dot(vehicleParts.VehicleTrans.forward, vehicleParts.VehicleRb.velocity));
            }
        }
    }

    void ApplySteering()
    {
        currentSteerAngle = vehicleStats.MaxSteeringAngle * vehicleInput.Turn;
        foreach (var wheel in vehicleParts.VehicleWheels)
        {
            if (wheel.wheelPlacement == VehiclePartsPlacements.Front)
                wheel.wheelCollider.steerAngle = currentSteerAngle;
        }
    }

    void ApplyBreaking()
    {
        foreach (var wheel in vehicleParts.VehicleWheels)
        {
            wheel.wheelCollider.brakeTorque = vehicleInput.isBreaking ? vehicleStats.BreakForce : 0;
        }

        foreach (var breakHeadLights in vehicleParts.VehicleHeadLights)
        {
            if (breakHeadLights.lightPlacement == VehiclePartsPlacements.Back && !breakHeadLights.IsReverseHeadLight)
                breakHeadLights.gameObject.SetActive(vehicleInput.isBreaking);
        }

    }

    void ApplyReversing()
    {
        foreach (var reverseHeadLights in vehicleParts.VehicleHeadLights)
        {
            if (reverseHeadLights.lightPlacement == VehiclePartsPlacements.Back && reverseHeadLights.IsReverseHeadLight)
                reverseHeadLights.gameObject.SetActive(vehicleState.IsReversing);
        }
    }

    void UpdateWheels()
    {
        foreach (var wheel in vehicleParts.VehicleWheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.transform.GetChild(0).rotation = rot;
            wheel.transform.GetChild(0).position = pos;
        }
    }
}
