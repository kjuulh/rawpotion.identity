using System;
using Microsoft.AspNetCore.Builder;
using Rawpotion.Identity.Common.Builders;

namespace Rawpotion.Identity.Common.Extensions
{
    public static class RawpotionApplicationBuilderExtensions
    {
        public static IRawpotionApplicationBuilder UseRawpotionBuilder(this IApplicationBuilder app) =>
            new RawpotionApplicationBuilder(app);

        public static IApplicationBuilder UseRawpotionService(this IApplicationBuilder app,
            Action<IRawpotionApplicationBuilder> builderOptions = null)
        {
            var builder = app.UseRawpotionBuilder();

            app.UseMiddleware<RawpotionMiddleware>();
            
            builderOptions?.Invoke(builder);
            return app;
        }

        
    }
}