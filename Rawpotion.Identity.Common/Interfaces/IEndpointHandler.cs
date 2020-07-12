using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Rawpotion.Identity.Common.Interfaces
{
    public interface IEndpointHandler
    {
        Task<IEndpointResult> ProcessAsync(HttpContext context);
    }
}