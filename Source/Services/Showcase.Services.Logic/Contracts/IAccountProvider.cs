namespace Showcase.Services.Logic.Contracts
{
    using Showcase.Data.Models;

    public interface IAccountProvider
    {
        User GetAccount(string username, string password);
    }
}
