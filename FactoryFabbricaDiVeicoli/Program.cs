using FactoryFabbricaDiVeicoli.Factory;

namespace FactoryFabbricaDiVeicoli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            VehicleFactory factory = new VehicleFactory();

            // Creazione di un'auto
            IVehicle car = factory.CreateVehicle("car");
            Console.Write("1) car: \t");
            car.Drive();

            // Creazione di una moto
            IVehicle motorcycle = factory.CreateVehicle("motorcycle");
            Console.Write("2) motorcycle: \t");
            motorcycle.Drive();

            // Creazione di un camion
            IVehicle truck = factory.CreateVehicle("truck");
            Console.Write("3) truck: \t");
            truck.Drive();

            Console.Write("4) boat: \t");
            // Tentativo di creare un veicolo non supportato
            try
            {
                IVehicle unknown = factory.CreateVehicle("boat");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Errore: {ex.Message}");
            }
        }
    }
}



