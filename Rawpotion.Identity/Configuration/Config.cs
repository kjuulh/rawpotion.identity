using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4;
using IdentityServer4.Models;

namespace Rawpotion.Identity.Configuration
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
        
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("rawpotion.graphql", "Rawpotion GraphQL API"),
            new ApiScope("rawpotion.identity.admin", "Rawpotion Identity Admin"),
            new ApiScope("rawpotion.identity", "Rawpotion Identity"),
        };
        
        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "rawpotion.test",
                ClientSecrets = {new Secret("secret".Sha256())},
                
                AllowedGrantTypes = GrantTypes.Code,
                
                RedirectUris = { "https://localhost:5003/signin-oidc" },
                
                PostLogoutRedirectUris = {"https://localhost:5003/signout-callback-oidc"},

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "rawpotion.identity.admin"
                }
            }
        };
    }
}