using MercadoEletronico.API.Applications.Queries;
using MercadoEletronico.API.Configuration;
using MercadoEletronico.API.Core;
using MercadoEletronico.API.Data;
using MercadoEletronico.API.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MercadoEletronico.API.Infra
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<MercadoEletronicoContext>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            //Repositories
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();

            //Queries
            services.AddScoped<IPedidoQueries, PedidoQueries>();

        }
    }
}
