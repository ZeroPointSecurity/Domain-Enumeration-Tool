using Xunit;

namespace DET.Tests;

public class DomainSearcherTests
{
    [Fact]
    public void CreateDefaultSearcher()
    {
        var searcher = new DomainSearcher();

        Assert.NotNull(searcher);
        Assert.NotNull(searcher.Directory);
    }

    [Fact]
    public void CreateSearcherWithPath()
    {
        var path = "LDAP://DC=testlab,DC=local";

        var searcher = new DomainSearcher(path);

        Assert.NotNull(searcher);
        Assert.NotNull(searcher.Directory);
        Assert.Equal(searcher.Directory.Path, path);
    }

    [Fact]
    public void CreateSearcherWithCredentials()
    {
        var path = "LDAP://DC=testlab,DC=local";
        var username = "LAB\\testuser";
        var password = "Passw0rd!";

        var searcher = new DomainSearcher(path, username, password);

        Assert.NotNull(searcher);
        Assert.NotNull(searcher.Directory);
        Assert.Equal(searcher.Directory.Path, path);
        Assert.Equal(searcher.Directory.Username, username);
    }

    [Fact]
    public void ModifyAuthenticationTypes()
    {
        var searcher = new DomainSearcher();
        var types = System.DirectoryServices.AuthenticationTypes.Secure | System.DirectoryServices.AuthenticationTypes.Delegation;

        searcher.SetAuthenticationTypes(types);

        Assert.NotNull(searcher);
        Assert.NotNull(searcher.Directory);
        Assert.Equal(searcher.Directory.AuthenticationType, types);
    }
}