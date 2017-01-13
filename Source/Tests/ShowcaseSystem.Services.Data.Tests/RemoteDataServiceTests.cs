namespace ShowcaseSystem.Services.Data.Tests
{
    using System.Diagnostics;
    using System.Linq;
    using System.Net;

    using NUnit.Framework;

    using Showcase.Data.Models;
    using Showcase.Services.Data;

    [TestFixture]
    public class RemoteDataServiceTests
    {
        [TestCase("ShowcaseSystem", "wrongpassword", null)]
        [TestCase("invalid*user", "wrongpassword", null)]
        public void LoginShouldReturnCorrectResults(string username, string password, User expected)
        {
            var service = new RemoteDataService();
            var result = service.Login(username, password).Result;
            Assert.AreEqual(expected, result);
        }

        [TestCase(2, "ShowcaseSystem", "Nikolay.IT")]
        [TestCase(1, "ShowcaseSystem", "invalid*user", "another*invalid*user")]
        [TestCase(0, new[] { "invalid*user" })]
        public void UsersInfoShouldReturnInformationForEveryUserGiven(int validCount, params string[] usernames)
        {
            var service = new RemoteDataService();
            var result = service.UsersInfo(usernames).Result.ToList();

            Assert.AreEqual(validCount, result.Count());

            foreach (var user in result)
            {
                Assert.IsNotNull(user);
            }
        }

        [TestCase("DonchoMinkov", true)]
        [TestCase("ShowcaseSystem", false)]
        public void UsersInfoShouldReturnCorrectResults(string username, bool isAdmin)
        {
            var service = new RemoteDataService();
            var result = service.UsersInfo(new[] { username }).Result.First();
            Assert.IsNotNull(result, "result != null");
            Assert.AreEqual(username, result.UserName, "Username received is not equal to the one requested!");
            Assert.AreEqual(isAdmin, result.IsAdmin, "Admin status not correct!");
            Assert.IsNotNull(result.AvatarUrl, "result.AvatarUrl != null");
            Assert.IsTrue(RemoteFileExists(result.AvatarUrl), "Avatar does not exist!");
        }

        [TestCase("Niko", 20, 20)]
        [TestCase("Ivay", 10, 10)]
        [TestCase("-", 20, 0)]
        public void SearchByUsernameShouldReturnCorrectResults(string textToSearch, int requestedResults, int expectedResults)
        {
            var service = new RemoteDataService();
            var result = service.SearchByUsername(textToSearch, requestedResults).Result.ToList();
            Assert.AreEqual(expectedResults, result.Count());
            foreach (var username in result)
            {
                Assert.IsTrue(
                    username.ToLower().StartsWith(textToSearch.ToLower()),
                    string.Format("{0} expected to starts with {1}", username, textToSearch));
            }
        }

        [TestCase("invalid*user")]
        public void ProfileInfoShouldReturnNonNullResultWithNullPropertiesWhenGivenInvalidUsername(string username)
        {
            var service = new RemoteDataService();
            var result = service.ProfileInfo(username).Result;
            Assert.IsNotNull(result);
            Assert.IsNull(result.Sex);
            Assert.IsNull(result.FirstName);
            Assert.IsNull(result.LastName);
            Assert.IsNull(result.Occupation);
            Assert.IsNull(result.ProfileAvatarUrl);
        }

        [TestCase("Nikolay.IT", "Male", "Nikolay", "Kostov")]
        [TestCase("ShowcaseSystem", null, "Showcase", "System")]
        public void ProfileInfoShouldReturnCorrectResults(string username, string sex, string firstName, string lastName)
        {
            var service = new RemoteDataService();
            var result = service.ProfileInfo(username).Result;
            Assert.IsNotNull(result);
            Assert.AreEqual(sex, result.Sex);
            Assert.AreEqual(firstName, result.FirstName);
            Assert.AreEqual(lastName, result.LastName);
            Assert.IsTrue(RemoteFileExists(result.ProfileAvatarUrl), "Avatar does not exist!");
        }

        [TestCase(true, new[] { "ShowcaseSystem" })]
        [TestCase(true, "Nikolay.IT", "ivaylo.kenov", "ShowcaseSystem")]
        [TestCase(false, "ShowcaseSystem", "but*this*user*does*not*exist")]
        [TestCase(false, new[] { "this*user*does*not*exist" })]
        public void UsersExistShouldReturnCorrectResults(bool expected, params string[] usernames)
        {
            var service = new RemoteDataService();
            var result = service.UsersExist(usernames).Result;
            Assert.AreEqual(expected, result);
        }

        private static bool RemoteFileExists(string url)
        {
            ServicePointManager.DefaultConnectionLimit = 20;
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 60000;

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                return response.StatusCode == HttpStatusCode.OK;
            }
        }
    }
}
