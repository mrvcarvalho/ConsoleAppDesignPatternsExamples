namespace ObserverWeatherStation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WeatherStation weatherStation = new WeatherStation();

            CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(Id:"CA");
            StatisticsDisplay statisticsDisplay = new StatisticsDisplay(Id:"TM");
            ForecastDisplay forecastDisplay = new ForecastDisplay(Id:"FC");

            weatherStation.RegisterObserver(currentDisplay);
            weatherStation.RegisterObserver(statisticsDisplay);
            weatherStation.RegisterObserver(forecastDisplay);

            Console.WriteLine("Simulazione della stazione meteorologica con intervalli di 3 secondi tra le letture.");
            Console.WriteLine();

            SimulateReading(weatherStation, 1, 25, 65, 1013.1f);
            SimulateReading(weatherStation, 2, 26, 70, 1014.2f);
            SimulateReading(weatherStation, 3, 24, 75, 1012.5f);

            Console.WriteLine(">> Rimozione del display delle (CA) condizioni attuali <<\n");
            weatherStation.RemoveObserver(currentDisplay);

            SimulateReading(weatherStation, 4, 23, 80, 1011.8f);

            Console.WriteLine(">> Rimozione del display delle (TM) statistiche <<\n");
            weatherStation.RemoveObserver(statisticsDisplay);

            SimulateReading(weatherStation, 6, 23, 80, 1011.8f);

            Console.WriteLine(">> Rimozione del display delle (FC) statistiche <<\n");
            weatherStation.RemoveObserver(forecastDisplay);

            SimulateReading(weatherStation, 7, 25, 65, 1013.1f);
        }

        private static void SimulateReading(WeatherStation station, int readingNumber, float temp, float humidity, float pressure)
        {
            Console.WriteLine($"Lettura #{readingNumber} dei dati meteo:");
            station.SetMeasurements(temp, humidity, pressure);
            Console.WriteLine();

            // Delay di 3 secondi
            Thread.Sleep(1000);
        }
    }

}
