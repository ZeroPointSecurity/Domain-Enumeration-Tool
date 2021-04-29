using System.Linq;

using Xunit;

namespace DET.Tests
{
    public class DomainTests
    {
        [Fact]
        public void GetDomainControllers()
        {
            var searcher = new DomainSearcher();
            var detDomain = new Domain(searcher);

            var domainControllers = detDomain.GetDomainControllers();

            Assert.NotNull(domainControllers);
            Assert.True(domainControllers.Count > 0);
        }

        [Fact]
        public void GetDomainSid()
        {
            var searcher = new DomainSearcher();
            var domain = new Domain(searcher);

            var result = domain.GetDomainSid();

            Assert.NotNull(result);
            Assert.True(result.Length == 40);
        }

        [Fact]
        public void GetDomainTrusts()
        {
            var searcher = new DomainSearcher();
            var domain = new Domain(searcher);

            var result = domain.GetDomainTrusts();

            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}
