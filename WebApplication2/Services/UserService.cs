using CRM.Domain.Entities;
using CRM.Domain.IdentityEntity;
using CRM.WebApp.DTOs.User;
using CRM.WebApp.Repositories;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<ApplicationUser> UserUepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IRepository<ApplicationUser> _UserUepository
            )
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            UserUepository = _UserUepository;
        }

        public string? UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid"); }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await UserUepository.GetAll().ToListAsync();
            var result = users.Select(a =>
              new UserDto
              {
                  Id = a.Id,
                  Name = a.UserName ?? string.Empty,
                  Email = a.Email ?? string.Empty
              }
              );

            return result;
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await UserUepository.GetAsync(a => a.Id == id);
            if (user == null)
                throw new ArgumentException();

            return new UserDto { Id = user.Id, Name = user.UserName ?? string.Empty, Email = user.Email ?? string.Empty };

        }
    }
}
