using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicationProcess.December2020.Domain.Helper
{
    public class RestCountriesService : ICountryService
    {
        private readonly ILogger<RestCountriesService> _logger;
        private readonly string _serviceBaseUrl;
        private readonly HttpClient _httpClient; 
        public RestCountriesService(HttpClient httpClient, IConfiguration configuration, ILogger<RestCountriesService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _serviceBaseUrl = configuration.GetSection("CountryServiceSetting")["BaseUrl"];
        }

        public async Task<string> QueryWebService(string name)
        {
            var result = ""; 
            var url = $"{_serviceBaseUrl}/name/{name}?field=name";
            try
            {
                var serviceResult = await _httpClient.GetFromJsonAsync<List<Country>>(url);
                result = serviceResult?.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    ?.Name;
            }
            catch (HttpRequestException ex)
            {
                result = "";
                if (ex.StatusCode != HttpStatusCode.NotFound)
                {
                    _logger.LogError(ex , "Error on fetch country name url {url}", url);
                    result = "";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception on sending request to {url}", url);
                result = ""; 
            }
            return result; 
        }
    }
}