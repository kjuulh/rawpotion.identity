using System;
using Microsoft.Extensions.DependencyInjection;
using Rawpotion.Identity.Account.Endpoints;
using Rawpotion.Identity.Common;
using Rawpotion.Identity.Common.Builders;
using Rawpotion.Identity.Common.Extensions;
using static Rawpotion.Identity.Account.AccountConstants;

namespace Rawpotion.Identity.Account.DependencyInjection
{
    public static class RawpotionAccountBuilderExtensions
    {
        public static IRawpotionServiceBuilder AddRawpotionAccount(this IRawpotionServiceBuilder builder) =>
            builder
                .AddAccountEndpoints()
                .AddAccountValidators()
                .AddAccountResponseGenerators();

        private static IRawpotionServiceBuilder AddAccountEndpoints(this IRawpotionServiceBuilder builder)
        {
            return builder
                .AddEndpoint<AccountEndpoint>(RouteNames.Account, Routes.Account.EnsureLeadingSlash());
        }

        private static IRawpotionServiceBuilder AddAccountValidators(this IRawpotionServiceBuilder builder)
        {
            return builder;
        }

        private static IRawpotionServiceBuilder AddAccountResponseGenerators(this IRawpotionServiceBuilder builder)
        {
            return builder;
        }
    }
}