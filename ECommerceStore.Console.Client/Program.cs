// See https://aka.ms/new-console-template for more information
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

Console.WriteLine("API Testing Starts!");


var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5443");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,

    ClientId = "client",
    ClientSecret = "secret",
    Scope = "ECommerceStore.API"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine(tokenResponse.Json);

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:7251/api/Products?id=3FA85F64-5717-4562-B3FC-2C963F66AFA6");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(content);
}
