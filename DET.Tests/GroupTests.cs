using System.Linq;

using Xunit;

namespace DET.Tests
{
    public class GroupTests
    {
        [Fact]
        public void GetAllGroupsAndProperties()
        {
            var searcher = new DomainSearcher();
            var groups = new Groups(searcher);

            var results = groups.GetGroups();

            Assert.NotNull(results);
            Assert.True(results.Any());
            Assert.True(results.First().Value.Values.Count > 20);
        }

        [Fact]
        public void GetSingleGroupAllProperties()
        {
            var searcher = new DomainSearcher();
            var groups = new Groups(searcher);
            var names = new string[] { "Domain Admins" };

            var results = groups.GetGroups(names);

            Assert.NotNull(results);
            Assert.True(results.Count == 1);
        }

        [Fact]
        public void GetSingleGroupSingleProperty()
        {
            var searcher = new DomainSearcher();
            var groups = new Groups(searcher);
            var names = new string[] { "Domain Admins" };
            var properties = new string[] { "member" };

            var results = groups.GetGroups(names, properties);

            Assert.NotNull(results);
            Assert.True(results.Count == 1);
            Assert.True(results.First().Value.Values.Count == 2);
        }

        [Fact]
        public void GetAllGroupsSingleProperty()
        {
            var searcher = new DomainSearcher();
            var groups = new Groups(searcher);
            var properties = new string[] { "member" };

            var results = groups.GetGroups(properties: properties);

            Assert.NotNull(results);
            Assert.True(results.Any());
            Assert.True(results.First().Value.Values.Count == 2);
        }
    }
}
