namespace Showcase.Services.Data.Contracts
{
    using Showcase.Services.Common;

    public interface IUsersService : IService
    {
        string GetUserId(string username);
    }
}