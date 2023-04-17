using Common.Configuration;
using Common.Persistence;
using OrderAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IRabbitMQChannelRegistry>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var apiSettings = configuration.GetSection(RabbitMQOptions.Name).Get<RabbitMQOptions>();

    if (apiSettings.UseStub)
        return new StubRabbitMQChannelRegistry();
    else
        return new RabbitMQChannelRegistry();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
