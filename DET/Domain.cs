using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace DET;

public class Domain
{
    private readonly DomainSearcher _searcher;

    /// <summary>
    /// Initializes a new instance of the DET.Domain class.
    /// </summary>
    /// <param name="searcher">An instance of the DET.DomainSearcher class.</param>
    public Domain(DomainSearcher searcher)
    {
        _searcher = searcher;
    }

    /// <summary>
    /// Get a collection of Domain Controllers for the domain.
    /// </summary>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of domain and its properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetDomainControllers(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(&(objectCategory=computer)(userAccountControl:1.2.840.113556.1.4.803:=8192))";

        return ldap.ExecuteQuery(filter, properties);
    }

    /// <summary>
    /// Get the domain SID.
    /// </summary>
    /// <returns>String.</returns>
    public string GetDomainSid()
    {
        var properties = new string[] { "objectsid" };
        var domainControllers = GetDomainControllers(properties);

        var firstDc = domainControllers.Values.First();
        var dcSidBytes = firstDc["objectsid"].First() as byte[];
        var dcSid = new SecurityIdentifier(dcSidBytes, 0);

        return dcSid.Value.Substring(0, dcSid.Value.LastIndexOf('-'));
    }

    /// <summary>
    /// Get domain trusts
    /// </summary>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of domain trust and its properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetDomainTrusts(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(objectClass=trustedDomain)";

        return ldap.ExecuteQuery(filter, properties);
    }
}