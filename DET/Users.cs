using System.Collections.Generic;

namespace DET;

public class Users
{
    private readonly DomainSearcher _searcher;

    /// <summary>
    /// Initializes a new instance of the DET.Users class.
    /// </summary>
    /// <param name="searcher">An instance of the DET.DomainSearcher class.</param>
    public Users(DomainSearcher searcher)
    {
        _searcher = searcher;
    }

    /// <summary>
    /// Get the specified users and their properties.
    /// </summary>
    /// <param name="userNames">Limit the response to the these usernames.</param>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of users and their properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetUsers(string[] userNames = null, string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        var filter = "(&(sAMAccountType=805306368)";

        if (userNames is not null)
        {
            filter += "(|";

            foreach (var userName in userNames)
                filter += $"(samaccountname=*{userName}*)";

            filter += ")";
        }

        filter += ")";

        return ldap.ExecuteQuery(filter, properties);
    }

    /// <summary>
    /// Get all users where servicePrincipalName is not null.
    /// </summary>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of users and their properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetKerberoastableUsers(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(&(sAMAccountType=805306368)(servicePrincipalName=*))";

        return ldap.ExecuteQuery(filter, properties);
    }

    /// <summary>
    /// Get users who "do not require kerberos preauthentication".
    /// </summary>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of users and their properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetASREPRoastableUsers(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(&(sAMAccountType=805306368)(userAccountControl:1.2.840.113556.1.4.803:=4194304))";

        return ldap.ExecuteQuery(filter, properties);
    }

    /// <summary>
    /// Get users with "Password Never Expires" set.
    /// </summary>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of users and their properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetPasswordNeverExpires(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(&(sAMAccountType=805306368)(userAccountControl:1.2.840.113556.1.4.803:=65536))";

        return ldap.ExecuteQuery(filter, properties);
    }

    /// <summary>
    /// Get users not required to have a password.
    /// </summary>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of users and their properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetPasswordNotRequired(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(&(sAMAccountType=805306368)(userAccountControl:1.2.840.113556.1.4.803:=32))";

        return ldap.ExecuteQuery(filter, properties);
    }

    /// <summary>
    /// Get users who are marked as sensitive and not trusted for delegation.
    /// </summary>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of users and their properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetNotTrustedForDelegation(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(&(sAMAccountType=805306368)(userAccountControl:1.2.840.113556.1.4.803:=1048576))";

        return ldap.ExecuteQuery(filter, properties);
    }

    /// <summary>
    /// Get users who are allowed to delegate.
    /// </summary>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of users and their properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetAllowedToDelegateTo(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(&(sAMAccountType=805306368)(msds-allowedtodelegateto=*))";

        return ldap.ExecuteQuery(filter, properties);
    }

    /// <summary>
    /// Get users with adminCount=1.
    /// </summary>
    /// <param name="properties">An array of properties to return.</param>
    /// <returns>A multi-level dictionary of users and their properties.</returns>
    public Dictionary<string, Dictionary<string, object[]>> GetProtectedByAdminSDHolder(string[] properties = null)
    {
        var ldap = new LDAP(_searcher);
        const string filter = "(&(sAMAccountType=805306368)(adminCount=1))";

        return ldap.ExecuteQuery(filter, properties);
    }
}