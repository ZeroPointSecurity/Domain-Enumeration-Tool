using Xunit;

namespace DET.Tests
{
    public class DACLTests
    {
        [Fact]
        public void TestGetDACL()
        {
            var searcher = new DomainSearcher();
            var dacl = new DACL(searcher);

            // Test GPO
            var dn = "CN={31B2F340-016D-11D2-945F-00C04FB984F9},CN=Policies,CN=System,DC=testlab,DC=local";

            var result = dacl.GetDacl(dn);

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }
    }
}
