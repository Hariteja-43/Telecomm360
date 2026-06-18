using System.Threading.Tasks;
using Telecomm360.Model;

namespace Telecomm360.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(long empId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}