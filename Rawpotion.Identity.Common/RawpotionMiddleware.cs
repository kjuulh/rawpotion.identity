using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rawpotion.Identity.Common.Interfaces;

namespace Rawpotion.Identity.Common
{
    public class RawpotionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RawpotionMiddleware> _logger;
        
        public RawpotionMiddleware(RequestDelegate next, ILogger<RawpotionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IEndpointRouter router)
        {
            try
            {
                var endpoint = router.Find(context);
                if (endpoint != null)
                {
                    _logger.LogInformation("Invoking Rawpotion endpoint: {endpointType} for {url}", endpoint.GetType().FullName, context.Request.Path.ToString());

                    var result = await endpoint.ProcessAsync(context);

                    if (result != null)
                    {
                        _logger.LogTrace("Invoking result: {type}", result.GetType().FullName);
                        await result.ExecuteAsync(context);
                    }

                    return;
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unhandled exception: {exception}", e.Message);
                throw;
            }

            await _next(context);
        }
    }
}