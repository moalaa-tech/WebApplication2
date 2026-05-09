using CRM.WebApp.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
    }
}