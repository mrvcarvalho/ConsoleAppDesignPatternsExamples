namespace FactoryFabbricaDiVeicoli.Factory
{
    public class VehicleFactory
    {
        public IVehicle CreateVehicle(string vehicleType)
        {
            switch (vehicleType.ToLower())
            {
                case "car":
                    return new Car();
                case "motorcycle":
                    return new Motorcycle();
                case "truck":
                    return new Truck();
                default:
                    throw new ArgumentException("Tipo di veicolo non supportato.");
            }
        }
    }
}



