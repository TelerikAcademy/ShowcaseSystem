namespace ShowcaseSystem.Services.Data.Tests
{
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

        [TestCase("Nikolay.IT", true)]
        [TestCase("ShowcaseSystem", false)]
        public void UsersInfoShouldReturnCorrectResults(string username, bool isAdmin)
        {
            var service = new RemoteDataService();
            var result = service.UsersInfo(new[] { username }).Result.First();
            Assert.IsNotNull(result, "result != null");
            Assert.AreEqual(username, result.UserName, "Username received is not equal to the one requested!");
            Assert.AreEqual(isAdmin, result.IsAdmin, "Admin status not correct!");
            Assert.IsNotNull(result.AvatarUrl, "result.AvatarUrl != null");
            Assert.IsTrue(RemoteFileExists(result.AvatarUrl));
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
            try
            {
                // Creating the HttpWebRequest
                var request = WebRequest.Create(url) as HttpWebRequest;
                
                // Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                
                // Getting the Web Response.
                var response = request.GetResponse() as HttpWebResponse;
                
                // Returns TRUE if the Status code == 200
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                // Any exception will returns false.
                return false;
            }
        }
    }
}
