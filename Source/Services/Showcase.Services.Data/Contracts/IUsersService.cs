namespace Showcase.Services.Data.Contracts
{
    using Showcase.Data.Models;
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using System.Threading.Tasks;

    public interface IUsersService : IService
    {
        string GetUserId(string username);

        Task<User> GetAccountAsync(string username, string password);
        IQueryable<User> GetByUsername(string username);
        string GetUserId(string username);

        Task<User> GetAccountAsync(string username, string password);
    }
}