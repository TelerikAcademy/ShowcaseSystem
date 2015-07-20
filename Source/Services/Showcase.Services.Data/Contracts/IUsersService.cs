namespace Showcase.Services.Data.Contracts
{
    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using System.Threading.Tasks;

    public interface IUsersService : IService
    {
        string GetUserId(string username);

        Task<User> GetAccountAsync(string username, string password);
    }
}