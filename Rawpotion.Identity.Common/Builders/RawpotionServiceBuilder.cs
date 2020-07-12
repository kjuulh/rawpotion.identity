using System;
using Microsoft.Extensions.DependencyInjection;

namespace Rawpotion.Identity.Common.Builders
{
    public interface IRawpotionServiceBuilder
    {
        public IServiceCollection Services { get; }
    }
    
    public class RawpotionServiceServiceBuilder : IRawpotionServiceBuilder
    {
        public RawpotionServiceServiceBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IServiceCollection Services { get; }
    }
    
    
}