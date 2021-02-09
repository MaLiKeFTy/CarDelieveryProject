﻿using System;
using UnityEngine;

public class VehicleController
{
    readonly IVehicleInput vehicleInput;
    readonly VehicleParts vehicleParts;
    readonly VehicleStats vehicleStats;
    readonly VehicleState vehicleState;
    readonly Vehicle vehicle;


    event Action RunMotorCallback;
    public static Func<float> SteeringCallBack;

    public VehicleController(Vehicle vehicle, IVehicleInput vehicleInput)
    {
        this.vehicle = vehicle;
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
        RunMotorCallback += ApplyTrailEffect;
    }

    public void RunMotor()
    {
        RunMotorCallback?.Invoke();
    }

    void ApplyAcceleration()
    {
      //  Debug.Log(vehicleParts.VehicleRb.velocity.magnitude);
        foreach (var wheel in vehicleParts.VehicleWheels)
        {
            if (wheel.wheelPlacement == VehiclePartsPlacements.Back)
                wheel.wheelCollider.motorTorque = vehicleInput.Acceleration * vehicleStats.MotorForce;
        }
    }

    void ApplySteering()
    {
        var steeringValue = (SteeringCallBack?.Invoke() ?? 0) * vehicleStats.SteeringMultiplier;

        foreach (var wheel in vehicleParts.VehicleWheels)
        {
            if (wheel.wheelPlacement == VehiclePartsPlacements.Front)
                wheel.wheelCollider.steerAngle = steeringValue;
        }
    }

    void ApplyBreaking()
    {
        foreach (var wheel in vehicleParts.VehicleWheels)
        {
            if (wheel.wheelPlacement == VehiclePartsPlacements.Back)
                wheel.wheelCollider.brakeTorque = vehicleInput.isBreaking ? vehicleStats.BreakForce : 0;
        }

        foreach (var breakHeadLights in vehicleParts.VehicleHeadLights)
        {
            if (breakHeadLights.lightPlacement == VehiclePartsPlacements.Back && !breakHeadLights.IsReverseHeadLight)
                breakHeadLights.gameObject.SetActive(vehicleInput.isBreaking || vehicleInput.Acceleration < 0);
        }

    }

    void ApplyReversing()
    {
        foreach (var reverseHeadLights in vehicleParts.VehicleHeadLights)
        {
            if (reverseHeadLights.lightPlacement == VehiclePartsPlacements.Back && reverseHeadLights.IsReverseHeadLight)
                reverseHeadLights.gameObject.SetActive(vehicleState.IsReversing && vehicleInput.Acceleration < 0);
        }
    }


    void ApplyTrailEffect()
    {
        foreach (var wheel in vehicleParts.VehicleWheels)
        {
            var wheelTrailEffect = wheel.WheelEffects.TypeTrail;
            wheelTrailEffect.emitting = vehicleState.IsGrounded && (wheel.IsRolling || vehicleInput.isBreaking) ? true : false;
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
