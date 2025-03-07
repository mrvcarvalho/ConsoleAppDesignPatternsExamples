using System;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using System.Globalization;
using System.Text;

namespace RepositoryProduct
{
    class Program
    {
        static string connectionString       = @"Server=(localdb)\MSSQLLocalDB;Database=ProductDB;Integrated Security=True;";
        static string masterConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=True;";
        static CultureInfo italianCulture = new CultureInfo("it-IT");

        static void Main(string[] args)
        {
            try
            {
                // Configurare la cultura italiana per l'intera applicazione
                CultureInfo.DefaultThreadCurrentCulture = italianCulture;
                CultureInfo.DefaultThreadCurrentUICulture = italianCulture;

                DropDatabaseIfExists();
                EnsureDatabaseCreated();
                EnsureTableCreated();

                CultureInfo cultureInfoIta = new CultureInfo("it-IT");
                Console.OutputEncoding = Encoding.UTF8;

                IProductRepository repository = new ProductRepository(connectionString);
                ProductService productService = new ProductService(repository);

                // Aggiunta di una gamma più ampia di prodotti
                Console.WriteLine(">> Aggiunta di prodotti...");
                productService.AddNewProduct("Laptop", 1799.00m, 30);
                productService.AddNewProduct("Mouse", 10.98m, 50);
                productService.AddNewProduct("Tastiera", 46.70m, 30);
                productService.AddNewProduct("Monitor 27", 299.99m, 12);
                productService.AddNewProduct("Stampante", 129.50m, 20);
                productService.AddNewProduct("SSD 1TB", 89.99m, 40);
                productService.AddNewProduct("Webcam HD", 59.90m, 25);
                productService.AddNewProduct("Cuffie Bluetooth", 79.99m, 5);
                productService.AddNewProduct("Router Wi-Fi", 45.50m, 18);
                productService.AddNewProduct("Adattatore USB-C", 15.99m, 60);
                productService.AddNewProduct("Powerbank 20000mAh", 39.99m, 22);
                productService.AddNewProduct("Custodia per Laptop", 24.50m, 45);
                Console.WriteLine(">> Prodotti aggiunti con successo.");

                // Elenco di tutti i prodotti
                Console.WriteLine("\n\tElenco di tutti i prodotti:");
                var allProducts = repository.GetAll();
                PrintProducts(allProducts);

                // Aggiornamento dello stock di un prodotto
                Console.WriteLine("\n>> Aggiornamento dello stock per il Laptop... di 30 per 15");
                var laptopId = allProducts.First(p => p.Name == "Laptop").Id;
                productService.UpdateProductStock(laptopId, 15);
                Console.WriteLine(">> Stock aggiornato con successo.");

                // Controllo dei prodotti a basso stock
                Console.WriteLine("\n\tProdotti con stock basso (meno di 20):");
                var lowStockProducts = productService.GetLowStockProducts(20);
                PrintProducts(lowStockProducts);

                // Rimozione di un prodotto
                Console.WriteLine("\n>> Rimozione del Mouse...");
                var mouseId = allProducts.First(p => p.Name == "Mouse").Id;
                repository.Delete(mouseId);
                Console.WriteLine(">> Mouse rimosso con successo.");

                // Elencare nuovamente i prodotti per confermare le modifiche
                Console.WriteLine("\n\tElenco aggiornato dei prodotti:");
                allProducts = repository.GetAll();
                PrintProducts(allProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Si è verificato un errore: {ex.Message}");
            }
        }


        static void PrintProducts(IEnumerable<Product> products)
        {
            Console.WriteLine("\t"+new string('-', 52));
            Console.WriteLine("\t" + $"{"ID",-4} {"Nome",-20} {"Prezzo",15} {"Stock",10}");
            Console.WriteLine("\t" + new string('-', 52));

            foreach (var product in products)
            {
                Console.WriteLine("\t" + $"{product.Id,-4} {product.Name,-20} {product.Price.ToString("C", italianCulture),15} {product.Stock,10}");
            }

            Console.WriteLine("\t" + new string('-', 52));
        }

        static void DropDatabaseIfExists()
        {
            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                connection.Execute(@"
                IF EXISTS (SELECT name FROM sys.databases WHERE name = 'ProductDB')
                BEGIN
                    ALTER DATABASE ProductDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE ProductDB;
                END");
            }
            Console.WriteLine(">> Database cancellato (se esistente).");
        }

        static void EnsureDatabaseCreated()
        {
            var masterConnection = @"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=True;";
            using (var connection = new SqlConnection(masterConnection))
            {
                connection.Open();
                connection.Execute(@"
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ProductDB')
            BEGIN
                CREATE DATABASE ProductDB;
            END");
            }
            Console.WriteLine(">> Database creato nuovo.");
        }

        static void EnsureTableCreated()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(@"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
            BEGIN
                CREATE TABLE Products (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(100) NOT NULL,
                    Price DECIMAL(18,2) NOT NULL,
                    Stock INT NOT NULL
                );
            END");
            }
            Console.WriteLine(">> Table Products creata nuova.");
        }
    }
}
