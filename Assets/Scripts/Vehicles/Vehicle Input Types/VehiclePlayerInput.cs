using UnityEngine;

public class VehiclePlayerInput : IVehicleInput
{
    public float Turn { get; private set; }

    public float Acceleration { get; private set; }

    public bool isBreaking { get; private set; }

    public void ReadInput()
    {
        Turn = Input.GetAxis("Horizontal");
        Acceleration = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }
}
