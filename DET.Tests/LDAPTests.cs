using System.Linq;

using Xunit;

namespace DET.Tests
{
    public class LDAPTests
    {
        [Fact]
        public void RawQueryTest()
        {
            var filter = "(&(objectCategory=computer))";
            var searcher = new DomainSearcher();
            var ldap = new LDAP(searcher);

            var result = ldap.ExecuteQuery(filter);

            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}
