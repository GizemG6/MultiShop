using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Services.Concrete
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly HttpClient _httpClient;
        private readonly ClientSettings _clientSettings;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;

        public ClientCredentialTokenService(ServiceApiSettings serviceApiSettings, HttpClient httpClient, ClientSettings clientSettings, IClientAccessTokenCache clientAccessTokenCache)
        {
            _serviceApiSettings = serviceApiSettings;
            _httpClient = httpClient;
            _clientSettings = clientSettings;
            _clientAccessTokenCache = clientAccessTokenCache;
        }

        public async Task<string> GetToken()
        {
            var token1 = await _clientAccessTokenCache.GetAsync("multishoptoken");
            
            if (token1 != null)
            {
                return token1.AccessToken;
            }

            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false,
                }
            });

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.MultiShopVisitorClient.ClientId,
                ClientSecret = _clientSettings.MultiShopVisitorClient.ClientSecret,
                Address = discoveryEndPoint.TokenEndpoint
            };

            var token2 = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            await _clientAccessTokenCache.SetAsync("multishoptoken", token2.AccessToken, token2.ExpiresIn);
            return token2.AccessToken;
        }
    }
}
