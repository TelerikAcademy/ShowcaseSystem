namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;

    public interface IUsersService : IService
    {
        IQueryable<User> ByUsername(string username);

        Task<User> AccountAsync(string username, string password);

        IEnumerable<string> SearchByUsername(string username);

        ICollection<User> CollaboratorsFromCommaSeparatedValues(string collaborators);

        int UserIdByUsername(string username);
    }
}