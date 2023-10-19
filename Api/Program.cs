using Api.Extensions;
using Application.Services.UseCases.GelAllOffers;
using FluentMigrator.Runner;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMyShopContext, MyShopContext>();

string connStringMyshopContext = builder.Configuration.GetConnectionString("MyshopContext");

builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddPostgres()
                    .WithGlobalConnectionString(connStringMyshopContext)
                    .ScanIn(typeof(InitialTables_202307140001231).Assembly).For.Migrations());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GelAllOffersQuery).Assembly));
builder.Services.ConfigureContextServices();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
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

app.MigrateDatabase().Run();

app.Run();
