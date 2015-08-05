namespace ShowcaseSystem.Services.Data.Tests
{
    using NUnit.Framework;

    using Showcase.Data.Models;
    using Showcase.Services.Data;

    [TestFixture]
    public class RemoteDataServiceTests
    {
        [TestCase(null, "ShowcaseSystem", "invalidpassword")]
        [TestCase(null, "invalid*user", "invalidpassword")]
        public void LoginShouldReturnValidResults(User expected, string username, string password)
        {
            var service = new RemoteDataService();
            var result = service.Login(username, password).Result;
            Assert.AreEqual(expected, result);
        }

        [TestCase(true, "ShowcaseSystem")]
        [TestCase(true, "Nikolay.IT", "ivaylo.kenov", "ShowcaseSystem")]
        [TestCase(false, "ShowcaseSystem", "but*this*user*does*not*exist")]
        [TestCase(false, "this*user*does*not*exist")]
        public void UsersExistShouldReturnValidResults(bool expected, params string[] usernames)
        {
            var service = new RemoteDataService();
            var result = service.UsersExist(usernames).Result;
            Assert.AreEqual(expected, result);
        }
    }
}
