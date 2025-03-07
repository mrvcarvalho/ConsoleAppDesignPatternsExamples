namespace ObserverWeatherStation
{
    public class StatisticsDisplay (string Id) : IObserver
    {
        private List<float> _temperatures = new List<float>();

        public void Update(float temperature, float humidity, float pressure)
        {
            _temperatures.Add(temperature);
            Console.WriteLine($"\t{Id}: Temp media: {_temperatures.Average():F1}°C, Min: {_temperatures.Min():F1}°C, Max: {_temperatures.Max():F1}°C");
        }
    }

}
