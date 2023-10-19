using Application.Services.UseCases.AddOffer;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Persistence.Repositories;
using Npgsql;
using Queries.Interface;
using Queries.Services;
using System.Data.Common;

namespace Api.Extensions
{
    public static class ContextExtension
    {
        public static void ConfigureContextServices(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductReadRepository, ProductReadRepository>();
            services.AddValidatorsFromAssemblyContaining<AddOfferCommandValidator>();
        }

        public static void AddDapperPostgreSql(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<DbConnection>(provider =>
            {
                return new NpgsqlConnection(connectionString);
            });
        }

    }
}
