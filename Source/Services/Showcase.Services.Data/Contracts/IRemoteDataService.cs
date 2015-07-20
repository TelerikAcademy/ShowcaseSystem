namespace Showcase.Services.Data.Contracts
{
    using Showcase.Data.Models;

    public interface IRemoteDataService
    {
        User RemoteLogin(string username, string password);
    }
}
