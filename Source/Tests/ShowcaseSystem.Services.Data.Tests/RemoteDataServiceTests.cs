namespace ShowcaseSystem.Services.Data.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    using Showcase.Services.Data;

    [TestFixture]
    public class RemoteDataServiceTests
    {
        [Test]
        public void UsersExistShouldReturnValidResults()
        {
            var service = new RemoteDataService();
            var result = service.UsersExist(new List<string> { "Nikolay.IT", "ivaylo.kenov" }).Result;
            Assert.AreEqual(true, result);
        }
    }
}
