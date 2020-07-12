using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Rawpotion.Identity.Common.Builders;
using Rawpotion.Identity.Common.Interfaces;
using Rawpotion.Identity.Common.Objects;

namespace Rawpotion.Identity.Common.Extensions
{
    public static class RawpotionServiceCollectionExtensions
    {
        public static IRawpotionServiceBuilder AddRawpotionBuilder(this IServiceCollection services) =>
            new RawpotionServiceServiceBuilder(services);
        
        public static IServiceCollection AddRawpotionService(this IServiceCollection services, Action<IRawpotionServiceBuilder> builderOptions = null)
        {
            var builder = services.AddRawpotionBuilder();

            builder.AddEndpoints();
            
            builderOptions?.Invoke(builder);
            return services;
        }
        
        public static IRawpotionServiceBuilder AddEndpoints(this IRawpotionServiceBuilder builder)
        {
            builder.Services.AddTransient<IEndpointRouter, EndpointRouter>();

            return builder;
        }

        public static IRawpotionServiceBuilder AddEndpoint<T>(this IRawpotionServiceBuilder builder, string name, PathString path)
            where T : class, IEndpointHandler
        {
            builder.Services.AddTransient<T>();
            builder.Services.AddSingleton(new RawpotionEndpoint(name, path, typeof(T)));

            return builder;
        }
    }
}