using Baseline;
using Marten;
using Marten.Events;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Weasel.Postgresql;

namespace SellerReturnApi.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, 
            IHostEnvironment hostEnvironment)
        {
            services.AddMarten(options =>
            {
                options.Connection(() => new NpgsqlConnection(configuration.GetConnectionString("EventSource")));
                
                if (hostEnvironment.IsDevelopment())
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }
                
                options.Events.StreamIdentity = StreamIdentity.AsString;
            });
            
            return services;
        }

        public static IServiceCollection AddFeatures(this IServiceCollection services)
        {
            var currentAssembly = typeof(Startup).Assembly;
            services.AddMediatR(currentAssembly);
            services.AddFluentValidation(new[] { currentAssembly });

            return services;
        }
    }
}