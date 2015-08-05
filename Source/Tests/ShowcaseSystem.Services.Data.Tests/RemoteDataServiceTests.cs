using NUnit.Framework;
using Showcase.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowcaseSystem.Services.Data.Tests
{
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
