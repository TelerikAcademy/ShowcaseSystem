namespace Showcase.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface IUsersService : IService
    {
        string GetUserId(string username);

        Task<User> GetAccountAsync(string username, string password);

        IQueryable<User> GetByUsername(string username);
    }
}