using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Infrastructure.Data.Context;

namespace TaskFlow.Infrastructure.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<UserGroup> _roleManager;
        private readonly AppDbContext _context;

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager, RoleManager<UserGroup> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<bool> CheckPasswordApplicationUser(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> Create(ApplicationUser user, string password, string grupo)
        {
            await _userManager.CreateAsync(user, password);
            return await _userManager.AddToRoleAsync(user, grupo);
        }

        public async Task<IdentityResult> Edit(ApplicationUser user, string lastGroup, string group)
        {
            await _userManager.RemoveFromRoleAsync(user, lastGroup);
            await _userManager.AddToRoleAsync(user, group);
            return await _userManager.UpdateAsync(user);
        }

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public IEnumerable<ApplicationUser> Get()
        {
            return _userManager.Users;
        }

        public async Task<ApplicationUser> GetByEmail(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser> GetByUserName(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            return user;
        }

        public List<ApplicationUserDTO> GetUsersWithRoles()
        {
            return _userManager.Users.Select(x => new ApplicationUserDTO
            {
                Id = x.Id,
                Name = x.Name,
                Group = _context.UserRoles
                    .Where(ur => ur.UserId == x.Id)
                    .Join
                    (
                        _context.Roles,
                        ur => ur.RoleId,
                        r => r.Id,
                        (ur, r) => r.Name
                    ).FirstOrDefault(),
                Email = x.Email,
                IsActive = x.LockoutEnabled
            }).ToList();
        }

        public async Task<IdentityResult> ResetPassword(ApplicationUser user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<IdentityResult> SetActiveApplicationUser(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
