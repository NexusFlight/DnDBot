using System.Threading.Tasks;

namespace DnDBot
{
    public interface IDbCon
    {
        Task CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<User> GetUserAsync(ulong id);
    }
}