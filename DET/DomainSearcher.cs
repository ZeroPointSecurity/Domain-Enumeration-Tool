using System.DirectoryServices;

namespace DET;

public class DomainSearcher
{
    public DirectoryEntry Directory { get; }

    /// <summary>
    /// Initializes a new instance of the DET.DomainSearcher class.
    /// </summary>
    public DomainSearcher()
    {
        Directory = new DirectoryEntry();
    }

    /// <summary>
    /// Initializes a new instance of the DET.DomainSearcher class.
    /// </summary>
    /// <param name="path">The path at which to bind the System.DirectoryServices.DirectoryEntry</param>
    public DomainSearcher(string path)
    {
        Directory = new DirectoryEntry(path);
    }

    /// <summary>
    /// Initializes a new instance of the DET.DomainSearcher class.
    /// </summary>
    /// <param name="path">The path at which to bind the System.DirectoryServices.DirectoryEntry.</param>
    /// <param name="username">The username to use when authenticating.</param>
    /// <param name="password">The password to use when authenticating.</param>
    public DomainSearcher(string path, string username, string password)
    {
        Directory = new DirectoryEntry(path, username, password);
    }

    /// <summary>
    /// Modify the AuthenticateType used on the System.DirectoryServices.DirectoryEntry.
    /// </summary>
    /// <param name="authenticationTypes">One or more of the System.DirectoryServices.AuthenticationTypes values.</param>
    public void SetAuthenticationTypes(AuthenticationTypes authenticationTypes)
    {
        Directory.AuthenticationType = authenticationTypes;
    }
}