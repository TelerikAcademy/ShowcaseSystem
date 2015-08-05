namespace ShowcaseSystem.Services.Data.Tests
{
    using NUnit.Framework;

    using Showcase.Services.Data;

    [TestFixture]
    public class RemoteDataServiceTests
    {
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
