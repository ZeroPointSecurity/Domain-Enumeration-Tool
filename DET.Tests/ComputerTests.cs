using System.Linq;

using Xunit;

namespace DET.Tests;

public class ComputerTests
{
    [Fact]
    public void GetAllComputersAndProperties()
    {
        var searcher = new DomainSearcher();
        var computers = new Computers(searcher);

        var results = computers.GetComputers();

        Assert.NotNull(results);
        Assert.True(results.Any());
        Assert.True(results.First().Value.Values.Count > 30);
    }

    [Fact]
    public void GetSingleComputerAllProperties()
    {
        var searcher = new DomainSearcher();
        var computers = new Computers(searcher);
        var computerNames = new[] { "dc-1" };

        var results = computers.GetComputers(computerNames);

        Assert.NotNull(results);
        Assert.True(results.Count == 1);
        Assert.True(results.First().Value.Values.Count > 30);
    }

    [Fact]
    public void GetTwoComputersAllProperties()
    {
        var searcher = new DomainSearcher();
        var computers = new Computers(searcher);
        var computerNames = new[] { "dc-1", "wkstn-1" };

        var results = computers.GetComputers(computerNames);

        Assert.NotNull(results);
        Assert.True(results.Count == 2);
        Assert.True(results.First().Value.Values.Count > 30);
    }

    [Fact]
    public void GetSingleComputerSingleProperty()
    {
        var searcher = new DomainSearcher();
        var computers = new Computers(searcher);
        var computerNames = new[] { "dc-1" };
        var properties = new[] { "serviceprincipalname" };

        var results = computers.GetComputers(computerNames, properties);

        Assert.NotNull(results);
        Assert.True(results.Count == 1);
        Assert.True(results.First().Value.Values.Count == 2);
    }

    [Fact]
    public void GetAllComputersSingleProperty()
    {
        var searcher = new DomainSearcher();
        var computers = new Computers(searcher);
        var properties = new[] { "serviceprincipalname" };

        var results = computers.GetComputers(properties: properties);

        Assert.NotNull(results);
        Assert.True(results.Any());
        Assert.True(results.First().Value.Values.Count == 2);
    }

    [Fact]
    public void GetUnconstrainedDelegation()
    {
        var searcher = new DomainSearcher();
        var computers = new Computers(searcher);

        var results = computers.GetTrustedForUnconstrainedDelegation();

        Assert.NotNull(results);
        Assert.True(results.Any());
    }

    [Fact]
    public void GetAllowedToDelegateTo()
    {
        var searcher = new DomainSearcher();
        var computers = new Computers(searcher);

        var results = computers.GetAllowedToDelegateTo();

        Assert.NotNull(results);
        Assert.True(results.Any());
    }
}