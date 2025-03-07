namespace ObserverWeatherStation
{
    public class ForecastDisplay (string Id) : IObserver
    {
        private float _lastPressure;

        public void Update(float temperature, float humidity, float pressure)
        {
            Console.Write($"\t{Id}: ");

            if (pressure > _lastPressure)
            {
                Console.WriteLine("A) Previsione: Il tempo sta migliorando!");
            }
            else if (pressure < _lastPressure)
            {
                Console.WriteLine("B) Previsione: Attenzione, potrebbe piovere!");
            }
            else
            {
                Console.WriteLine("C) Previsione: Condizioni stabili");
            }
            _lastPressure = pressure;
        }
    }

}
