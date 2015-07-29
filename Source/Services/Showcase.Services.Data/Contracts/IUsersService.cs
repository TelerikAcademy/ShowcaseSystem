namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface IUsersService : IService
    {
        int UserIdByUsername(string username);

        Task<User> AccountAsync(string username, string password);

        IQueryable<User> ByUsername(string username);

        IEnumerable<string> SearchByUsername(string username);

        ICollection<User> CollaboratorsFromCommaSeparatedValues(string collaborators);
    }
}