public interface IVehicleInput
{
    void ReadInput();
    float Turn { get; }
    float Acceleration { get; }
    bool isBreaking { get; }
}
