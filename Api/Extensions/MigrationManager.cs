using FluentMigrator.Runner;
using Infrastructure.Persistence;

namespace Api.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<IMyShopContext>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                databaseService.CreateDatabase("Poc.MyShopContext");
                migrationService?.ListMigrations();
                migrationService?.MigrateUp();

            }

            return host;
        }
    }
}
