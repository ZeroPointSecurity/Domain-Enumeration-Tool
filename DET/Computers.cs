using System.Collections.Generic;

namespace DET;

public class Computers
{
    private readonly DomainSearcher _searcher;

    /// <summary>
    /// Initializes a new instance of the DET.Computers class.
    /// </summary>
    /// <param name="searcher">An instance of the DET.DomainSearcher class.</param>
    public Computers(DomainSearcher searcher)
    {
        _searcher = searcher;
    }

    /// <summary>
    /// Get the specified computers and their properties.
    /// </summary>
    /// <param name="computerNames">Limit the response to the these computer names.</param>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of computers and their properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetComputers(string[] computerNames = null, string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        var filter = "(&(objectCategory=computer)";

        if (computerNames is not null)
        {
            filter += "(|";

            foreach (var computerName in computerNames)
                filter += $"(dnshostname=*{computerName}*)";

            filter += ")";
        }

        filter += ")";

        return ldap.ExecuteQuery(filter, properties);
    }

    /// <summary>
    /// Get computers permitted to perform unconstrained delegation.
    /// </summary>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of computers and their properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetTrustedForUnconstrainedDelegation(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(&(objectCategory=computer)(userAccountControl:1.2.840.113556.1.4.803:=524288))";

        return ldap.ExecuteQuery(filter, properties);
    }

    public Dictionary<string, Dictionary<string, object[]>> GetAllowedToDelegateTo(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(&(objectCategory=computer)(msds-allowedtodelegateto=*))";

        return ldap.ExecuteQuery(filter, properties);
    }
}