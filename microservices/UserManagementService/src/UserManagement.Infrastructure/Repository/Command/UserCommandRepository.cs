using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Contracts.Repositories;
using UserManagement.Domain.Entities;
using Work.Rabbi.Common.Infrastructure.Persistence;
using Work.Rabbi.Common.Infrastructure.Repository.Command;

namespace UserManagement.Infrastructure.Repository.Command
{
    public class UserCommandRepository<TContext> : CommandRepository<ApplicationUser, TContext>, IUserCommandRepository where TContext : DbContext
    {
        public UserCommandRepository(DbFactory<TContext> dbFactory) : base(dbFactory)
        {
        }

        public Task<bool> CheckUserPasswordAsync(ApplicationUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, string userId)> CreateUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, string userId)> CreateUserAsync(ApplicationUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetUsersAsync(string search, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, string userId)> UpdateUserAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
