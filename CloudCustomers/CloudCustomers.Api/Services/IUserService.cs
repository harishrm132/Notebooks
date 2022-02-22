
using CloudCustomers.Api.Models;

namespace CloudCustomers.Api.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}