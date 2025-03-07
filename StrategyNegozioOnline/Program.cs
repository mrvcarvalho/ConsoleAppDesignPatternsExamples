using System.Globalization;
using System.Text;

namespace StrategyNegozioOnline
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            decimal purchaseAmount = 100m;

            CultureInfo cultureInfoIta = new CultureInfo("it-IT");
            Console.OutputEncoding = Encoding.UTF8;

            // Crea un carrello con nessuno sconto iniziale
            ShoppingCart cart = new ShoppingCart(new NoDiscount());

            // Aggiungi alcuni articoli al carrello
            cart.AddItem(new CartItem(961, "1x Libro di program  ", 29.99m));
            cart.AddItem(new CartItem(432, "3x Mouse wireless    ", 15.50m));
            cart.AddItem(new CartItem(321, "1x Tastiera meccanica", 85.67m));

            // Checkout senza sconto
            Console.WriteLine("\n1) Carrello senza sconto     :\t");
            Console.WriteLine("=============================================\t");
            cart.Checkout();
            Console.WriteLine();

            // Applica uno sconto del 10%

            Console.WriteLine("\n2) Checkout con sconto del 25%:");
            Console.WriteLine("=============================================\t");
            cart.SetDiscountStrategy(new PercentageDiscount(25, "Bonus di affiliazione"));
            cart.Checkout();
            Console.WriteLine();

            // Applica uno sconto fisso di 15
            Console.WriteLine("\n3) Checkout con sconto fisso di 15:");
            Console.WriteLine("=============================================\t");
            cart.SetDiscountStrategy(new FixedDiscount(15, "Sconto promozionale Pasqua"));
            cart.Checkout();
            Console.WriteLine("\n");
        }
    }
}
