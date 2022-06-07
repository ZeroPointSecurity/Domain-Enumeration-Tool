using System.Linq;

using Xunit;

namespace DET.Tests;

public class OUTests
{
    [Fact]
    public void GetAllOUsAndProperties()
    {
        var searcher = new DomainSearcher();
        var ous = new OrganizationalUnits(searcher);

        var results = ous.GetOrganizationalUnits();

        Assert.NotNull(results);
        Assert.True(results.Any());
        Assert.True(results.First().Value.Values.Count > 15);
    }

    [Fact]
    public void GetSingleOUAllProperties()
    {
        var searcher = new DomainSearcher();
        var ous = new OrganizationalUnits(searcher);
        var names = new[] { "TestOU" };

        var results = ous.GetOrganizationalUnits(names);

        Assert.NotNull(results);
        Assert.True(results.Count == 1);
    }

    [Fact]
    public void GetSingleOUSingleProperty()
    {
        var searcher = new DomainSearcher();
        var ous = new OrganizationalUnits(searcher);
        var names = new[] { "TestOU" };
        var properties = new[] { "distinguishedname" };

        var results = ous.GetOrganizationalUnits(names, properties);

        Assert.NotNull(results);
        Assert.True(results.Count == 1);
        Assert.True(results.First().Value.Values.Count == 2);
    }

    [Fact]
    public void GetAllOUsSingleProperty()
    {
        var searcher = new DomainSearcher();
        var ous = new OrganizationalUnits(searcher);
        var properties = new[] { "distinguishedname" };

        var results = ous.GetOrganizationalUnits(properties: properties);

        Assert.NotNull(results);
        Assert.True(results.Any());
        Assert.True(results.First().Value.Values.Count == 2);
    }
}