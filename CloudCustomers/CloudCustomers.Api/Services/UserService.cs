using CloudCustomers.Api.Config;
using CloudCustomers.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CloudCustomers.Api.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        private readonly UserApiOptions apiConfig;

        public UserService(HttpClient httpClient, IOptions<UserApiOptions> apiConfig)
        {
            this.httpClient = httpClient;
            this.apiConfig = apiConfig.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userResponse = await httpClient.GetAsync(apiConfig.EndPoint);
            if (userResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<User>();
            }
            var responseContent = userResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}
