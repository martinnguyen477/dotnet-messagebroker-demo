using PaymentService;
using PaymentService.Consumers;
using PaymentService.Data;
using PaymentService.Repositories;
using MassTransit;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<PaymentWorker>();

        services.AddScoped<IPaymentContext, PaymentContext>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<TakePaymentConsumer>();

            x.UsingRabbitMq((context, cfg) => { cfg.ConfigureEndpoints(context); });
        });

        services.AddHealthChecks()
                .AddMongoDb(hostContext.Configuration["DatabaseSettings:ConnectionString"], "MongoDb Health", HealthStatus.Degraded);

        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
    })
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
        config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false);
        config.AddJsonFile("appsettings.k8s.json", optional: true, reloadOnChange: false);
    })
    .Build();

host.Run();