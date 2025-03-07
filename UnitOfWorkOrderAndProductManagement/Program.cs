using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWorkOrderAndProductManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<OrderService>();

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                SeedData(dbContext);

                var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();

                try
                {
                    // Verifica dello stock iniziale
                    var stockIni = dbContext.Products.Find(1);   // Laptop
                    Console.WriteLine($"Stock iniziale del prodotto:          id:1, descr:'Laptop', qtt:{stockIni?.Stock}");

                    Console.WriteLine( "Ordine creato con successo! : venduto id:1, descr:'Laptop', qtt:8");
                    orderService.CreateOrder(1, 8);                     
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore durante la creazione dell'ordine: {ex.Message}");
                }

                // Verifica dello stock finale
                var stockFin = dbContext.Products.Find(1);   // Laptop
                Console.WriteLine($"Stock finale del prodotto  :          id:1, descr:'Laptop', qtt:{stockFin?.Stock}");

            }
        }

        static void SeedData(AppDbContext context)
        {
            context.Products.Add(new Product { Id = 1, Name = "Laptop", Price = 1000m, Stock = 100 });
            context.SaveChanges();
        }
    }
}
