using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.DTOs;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces.Repository
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> Get();
        List<ApplicationUserDTO> GetUsersWithRoles();
        Task<ApplicationUser> GetById(string id);
        Task<ApplicationUser> GetByEmail(string email);
        Task<ApplicationUser> GetByUserName(string userName);
        Task<IdentityResult> Create(ApplicationUser usuario, string password, string grupo);
        Task<IdentityResult> Edit(ApplicationUser usuario, string grupoAnterior, string grupo);
        Task<IdentityResult> SetActiveApplicationUser(ApplicationUser usuario);
        Task<IdentityResult> ResetPassword(ApplicationUser usuario, string token, string password);
        Task<string> GenerateToken(ApplicationUser usuario);
        Task<bool> CheckPasswordApplicationUser(ApplicationUser usuario, string password);
    }
}
