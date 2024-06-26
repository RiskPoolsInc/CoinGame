using App.Data.Sql.Modules;
using App.Services.RestoreUsers;
using App.Services.RestoreUsers.CF;
using App.Services.RestoreUsers.ConnectionStringProviders;
using App.Services.RestoreUsers.DbContextes;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var builder = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .AddJsonFile("appsettings.Development.json", true)
             .AddEnvironmentVariables();

var config = builder.Build();


var dbContextModule = new AppDbContextModule(CFConnectionStringProvider.CONNECTION_KEY);
dbContextModule.CreateDbContext(null);

using var cfDb = new CFContextFactory().Create(config);
await cfDb.MigrateAsync(CancellationToken.None);

var dbOldContextModule = new AppDbContextModule(CFConnectionStringProviderOld.CONNECTION_KEY);
dbOldContextModule.CreateDbContext(null);

using var cfDbOld = new CFOldContextFactory().Create(config);
await cfDbOld.MigrateAsync(CancellationToken.None);

var restore = new RestoreCFUser(cfDb, cfDbOld);

var loggerProvider = new RestoreCFUserLoggerProvider();

string input;
restore.EmailLogger = loggerProvider.CreateLogger("Emails");

do {
    Console.WriteLine("Enter emails separated by ',' for restore :");

    input = Console.ReadLine();

    if (!string.IsNullOrEmpty(input)) {
        var emails = input.Split(",");

        for (var i = 0; i < emails.Length; i++) {
            var email = emails[i];
            var logger = loggerProvider.CreateLogger("User");
            restore.Logger = logger;
            logger.LogInformation($"Restoring user with email {email}\n\r");
            logger.LogInformation("-------------------------------\n\r");
            logger.LogInformation($"[{i + 1}/{emails.Length}] Restore {email}\n\r");
            logger.LogInformation("-------------------------------\n\r");
            logger.LogInformation("-------------------------------\n\r");

            try {
                restore.RestoreUser(email.Trim());
            }
            catch (Exception e) {
                logger.LogInformation("\n\rError:", e);
            }
            finally {
                logger.LogInformation("-------------------------------\n\r");
                logger.LogInformation("-------------------------------\n\r");
                logger.LogInformation($"Restore {email} complete\n\r");
            }
        }
    }
} while (!string.IsNullOrEmpty(input));

Console.WriteLine("Closed");