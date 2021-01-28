public class Truck : Vehicle
{
    IVehicleInput truckInput;
    VehicleController truckController;

    void Awake()
    {
        truckInput = new VehiclePlayerInput();
        truckController = new VehicleController(this, truckInput);
    }

    void Update()
    {
        truckInput.ReadInput();
        truckController.RunMotor();
    }
}
