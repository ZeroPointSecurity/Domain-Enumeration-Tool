using Xunit;

namespace DET.Tests
{
    public class DomainTests
    {
        [Fact]
        public void GetDomain()
        {
            var searcher = new DomainSearcher();
            var detDomain = new Domain(searcher);
            var name = "testlab.local";

            var domain = detDomain.GetDomain(name);

            Assert.NotNull(domain);
            Assert.Equal(name, domain.Name);
        }

        [Fact]
        public void GetDomainControllers()
        {
            var searcher = new DomainSearcher();
            var detDomain = new Domain(searcher);
            var name = "testlab.local";

            var domainControllers = detDomain.GetDomainControllers(name);

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
    }
}
