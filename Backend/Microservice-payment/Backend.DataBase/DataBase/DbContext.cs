using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Backend.Domain.Models;

namespace Backend.DataBase.DataBase
{
    public class DbContext
    {
        private readonly IMongoDatabase _database;
        public DbContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }
        public async Task<bool> CheckConnectionAsync()
        {
            try
            {
                // Configuramos un timeout de 2 segundos para no bloquear la app
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
                await _database.RunCommandAsync<MongoDB.Bson.BsonDocument>(
                    new MongoDB.Bson.BsonDocument("ping", 1),
                    cancellationToken: cts.Token
                );
                return true;
            }
            catch (Exception ex)
            {
                // Esto imprimirá en tu consola de Visual Studio el motivo real (Permisos, Red, etc.)
                System.Diagnostics.Debug.WriteLine($"[MongoDB Error]: {ex.Message}");
                return false;
            }
        }

        public IMongoCollection<PaymentModel> Payments =>
            _database.GetCollection<PaymentModel>("tbl_payment");
    }
}
