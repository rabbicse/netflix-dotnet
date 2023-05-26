using UserManagement.Domain.Entities;

namespace UserManagement.Application.Contracts.Repositories
{
    public interface IUserCommandRepository
    {
        Task<bool> ExistsUserAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetUsersAsync(string search, int page = 1, int pageSize = 10);
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<bool> CheckUserPasswordAsync(ApplicationUser user, string password);
        Task<(bool success, string userId)> CreateUserAsync(ApplicationUser user);
        Task<(bool success, string userId)> CreateUserAsync(ApplicationUser user, string password);

        Task<(bool success, string userId)> UpdateUserAsync(ApplicationUser user);
        Task<bool> DeleteUserAsync(string userId);
    }
}
