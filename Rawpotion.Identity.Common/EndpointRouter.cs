using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rawpotion.Identity.Common.Interfaces;
using Rawpotion.Identity.Common.Objects;

namespace Rawpotion.Identity.Common
{
    public class EndpointRouter : IEndpointRouter
    {
        private readonly IEnumerable<RawpotionEndpoint> _endpoints;
        private readonly ILogger<EndpointRouter> _logger;

        public EndpointRouter(IEnumerable<RawpotionEndpoint> endpoints, ILogger<EndpointRouter> logger)
        {
            _endpoints = endpoints;
            _logger = logger;
        }

        public IEndpointHandler Find(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            foreach (var endpoint in _endpoints)
            {
                var path = endpoint.Path;
                if (context.Request.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
                {
                    var endpointName = endpoint.Name;
                    _logger.LogDebug("Request path {path} matched to endpoint type {endpoint}", context.Request.Path, endpointName);

                    return GetEndpointHandler(endpoint, context);
                }
            }
            _logger.LogTrace("No endpoint entry found for request path: {path}", context.Request.Path);
            return null;
        }

        private IEndpointHandler GetEndpointHandler(RawpotionEndpoint endpoint, HttpContext context)
        {
            if (context.RequestServices.GetService(endpoint.Handler) is IEndpointHandler handler)
            {
                return handler;
            }
            
            _logger.LogDebug("Endpoint enabled: {endpoint}, failed to create handler {endpointHandler}", endpoint.Name, endpoint.Handler.FullName);
            return null;
        }
        
    }
}