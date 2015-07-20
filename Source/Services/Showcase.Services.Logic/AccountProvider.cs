namespace Showcase.Services.Logic
{
    using Showcase.Data.Models;
    using Showcase.Services.Logic.Contracts;

    public class AccountProvider : IAccountProvider
    {
        public User GetAccount(string username, string password)
        {
            return new User
            {
                UserName = username,
                Email = "test@test.com"
            };
        }
    }
}
