using System.Linq;

using Xunit;

namespace DET.Tests
{
    public class GPOTests
    {
        [Fact]
        public void GetAllGPOsAllProperties()
        {
            var searcher = new DomainSearcher();
            var ous = new GroupPolicyObjects(searcher);

            var results = ous.GetGPOs();

            Assert.NotNull(results);
            Assert.True(results.Any());
            Assert.True(results.First().Value.Values.Count > 20);
        }

        [Fact]
        public void GetSingleGPOAllProperties()
        {
            var searcher = new DomainSearcher();
            var ous = new GroupPolicyObjects(searcher);
            var names = new string[] { "Default Domain Policy" };

            var results = ous.GetGPOs(names);

            Assert.NotNull(results);
            Assert.True(results.Count == 1);
        }

        [Fact]
        public void GetSingleGPOSingleProperty()
        {
            var searcher = new DomainSearcher();
            var ous = new GroupPolicyObjects(searcher);
            var names = new string[] { "Default Domain Policy" };
            var properties = new string[] { "gpcfilesyspath" };

            var results = ous.GetGPOs(names, properties);

            Assert.NotNull(results);
            Assert.True(results.Count == 1);
            Assert.True(results.First().Value.Values.Count == 2);
        }
    }
}
