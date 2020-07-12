using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Rawpotion.Identity.Common.Interfaces
{
    public interface IEndpointResult
    {
        Task ExecuteAsync(HttpContext context);
    }
}