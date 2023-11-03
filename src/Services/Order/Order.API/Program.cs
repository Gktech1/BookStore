using API.EventBusConsumer;
using API.Extensions;
using EventBus.Messages.Common;
using Infastructure;
using Infrastructure.Persistence;
using MassTransit;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<CartCheckoutConsumer>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehaviour", true);

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {

    config.AddConsumer<CartCheckoutConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        cfg.ReceiveEndpoint(EventBusConstants.CartCheckoutQueue, c =>
        {
            c.ConfigureConsumer<CartCheckoutConsumer>(ctx);
        });
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();
    OrderContextSeed
        .SeedAsync(context, logger)
        .Wait();

});
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
