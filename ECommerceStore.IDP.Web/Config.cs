using IdentityServer4;
using IdentityServer4.Models;

namespace ECommerceStore.IDP.Web
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                //new IdentityResource("roles", new[] { "role" })

            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                 new ApiScope("ECommerceStore.API", "Ecommerce API"),
                 new ApiScope("roles",new List<string> { "role" }),
                
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Code,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "ECommerceStore.API" }
                },
                 new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:7095/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:7095/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
                 ,new Client
                {
                    ClientId = "postman",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after login
                    RedirectUris = { "https://oauth.pstmn.io/v1/callback" },

                    // where to redirect to after logout
                    //PostLogoutRedirectUris = { "https://localhost:7095/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        "ECommerceStore.API", "roles", "openid" ,"profile"
                    }
                }
            };
    }
}
 