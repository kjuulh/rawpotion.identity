using Microsoft.AspNetCore.Http;

namespace Rawpotion.Identity.Common.Interfaces
{
    public interface IEndpointRouter
    {
        IEndpointHandler Find(HttpContext context);
    }
}