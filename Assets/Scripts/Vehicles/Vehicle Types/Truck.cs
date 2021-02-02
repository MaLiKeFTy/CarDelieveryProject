public class Truck : Vehicle
{
    IVehicleInput truckInput;
    VehicleController truckController;

    void Awake()
    {
        VehicleState = new VehicleState(transform, VehicleParts.VehicleWheels);
        truckInput = new VehiclePlayerInput();
        truckController = new VehicleController(this, truckInput);
    }

    void FixedUpdate()
    {
        VehicleState.DeployState();
        truckController.RunMotor();
    }

    void Update() => truckInput.ReadInput();
}
