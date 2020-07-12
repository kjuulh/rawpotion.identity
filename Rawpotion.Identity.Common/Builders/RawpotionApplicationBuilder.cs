using System;
using Microsoft.AspNetCore.Builder;

namespace Rawpotion.Identity.Common.Builders
{
    public interface IRawpotionApplicationBuilder
    {
        IApplicationBuilder App { get; }
    }
    
    public class RawpotionApplicationBuilder : IRawpotionApplicationBuilder
    {
        public IApplicationBuilder App { get; }

        public RawpotionApplicationBuilder(IApplicationBuilder app)
        {
            App = app ?? throw new ArgumentNullException(nameof(app));
        }
    }
}