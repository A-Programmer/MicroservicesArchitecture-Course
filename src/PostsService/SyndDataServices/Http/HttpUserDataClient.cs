using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PostsService.Dtos;

namespace PostsService.SyncDataServices
{
    public class HttpUserDataClient : IUserDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpUserDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPostToUsers(PostReadDto post)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(post),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(_configuration["UsersServiceUrl"], httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(" ==> Sync POST to Users Service was OK!");
            }
            else
            {
                Console.WriteLine(" ==> Sync POST to Users Service was NOT OK!");
            }
        }
    }
}