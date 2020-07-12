using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rawpotion.Identity.Common;
using Rawpotion.Identity.Common.Extensions;
using Rawpotion.Identity.Common.Interfaces;

namespace Rawpotion.Identity.Account.Endpoints
{
    public class AccountEndpoint : IEndpointHandler
    {
        public async Task<IEndpointResult> ProcessAsync(HttpContext context)
        {
            return new AccountResult();
        }
    }

    public class AccountResult : IEndpointResult
    {
        public async Task ExecuteAsync(HttpContext context)
        {
            var dto = new AccountResultDto
            {
                status = "hello_world"
            };

            await context.Response.WriteJsonAsync(dto);
        }
    }

    public class AccountResultDto
    {
        public string status { get; set; }
    }
}