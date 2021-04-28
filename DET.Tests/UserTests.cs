using System.Linq;

using Xunit;

namespace DET.Tests
{
    public class UserTests
    {
        [Fact]
        public void GetAllUsersAllProperties()
        {
            var searcher = new DomainSearcher();
            var users = new Users(searcher);

            var results = users.GetUsers();

            Assert.NotNull(results);
            Assert.True(results.Any());
            Assert.True(results.First().Value.Values.Count > 30);
        }

        [Fact]
        public void GetSingleUserAllProperties()
        {
            var searcher = new DomainSearcher();
            var users = new Users(searcher);
            var userNames = new string[] { "user1" };

            var results = users.GetUsers(userNames);

            Assert.NotNull(results);
            Assert.True(results.Count == 1);
        }

        [Fact]
        public void GetSingleUserSingleProperty()
        {
            var searcher = new DomainSearcher();
            var users = new Users(searcher);
            var userNames = new string[] { "user1" };
            var properties = new string[] { "pwdlastset" };

            var results = users.GetUsers(userNames, properties);

            Assert.NotNull(results);
            Assert.True(results.Count == 1);
            Assert.True(results.First().Value.Values.Count == 2);
        }
    }
}
