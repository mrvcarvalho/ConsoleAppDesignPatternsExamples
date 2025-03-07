namespace ObserverWeatherStation
{
    public class CurrentConditionsDisplay (string Id): IObserver
    {
        public void Update(float temperature, float humidity, float pressure)
        {
            Console.WriteLine($"\t{Id}: Condizioni attuali: {temperature}°C e {humidity}% di umidità");
        }
    }

}
