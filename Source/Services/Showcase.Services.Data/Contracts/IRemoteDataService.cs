namespace Showcase.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Showcase.Data.Models;
    using Showcase.Services.Common;
    using Showcase.Services.Data.Models;

    public interface IRemoteDataService : IService
    {
        Task<User> Login(string username, string password);

        Task<IEnumerable<User>> UsersInfo(IEnumerable<string> usernames);

        Task<IEnumerable<string>> SearchByUsername(string username);

        Task<RemoteUserProfile> ProfileInfo(string username);

        Task<bool> UsersExist(IEnumerable<string> usernames);
    }
}
