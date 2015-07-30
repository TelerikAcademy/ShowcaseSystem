namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    public interface IUsersService : IService
    {
        IQueryable<User> ByUsername(string username);

        Task<User> Account(string username, string password);

        Task<IEnumerable<string>> SearchByUsername(string username);

        Task<ICollection<User>> CollaboratorsFromCommaSeparatedValues(string collaborators);

        Task<RemoteUserProfile> ProfileInfo(string username);

        int UserIdByUsername(string username);
    }
}