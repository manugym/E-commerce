using TuringClothes.Database;

namespace TuringClothes.Services
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public MyBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var myDatabase = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<MyDatabase>();
                    var expiredOrders = myDatabase.TemporaryOrders.Where(o => o.ExpirationTime < DateTime.UtcNow).ToList();

                    if (expiredOrders.Any())
                    {
                        Console.WriteLine("Borrando órdenes temporales expiradas...");
                        myDatabase.RemoveRange(expiredOrders);
                        await myDatabase.SaveChangesAsync(stoppingToken);
                    }

                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Error al borrar órdenes temporales expiradas.");
                }
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }


        }
    }
}
