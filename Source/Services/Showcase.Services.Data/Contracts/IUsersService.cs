namespace Showcase.Services.Data.Contracts
{
    using System.Linq;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface IUsersService : IService
    {
        string GetUserId(string username);

        IQueryable<User> GetByUsername(string username);
    }
}