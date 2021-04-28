using System;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Principal;

using ADDomain = System.DirectoryServices.ActiveDirectory.Domain;

namespace DET
{
    public class Domain
    {
        readonly DomainSearcher _searcher;

        /// <summary>
        /// Initializes a new instance of the DET.Domain class.
        /// </summary>
        /// <param name="searcher">An instance of the DET.DomainSearcher class.</param>
        public Domain(DomainSearcher searcher)
        {
            _searcher = searcher;
        }

        /// <summary>
        /// Get information about the domain.
        /// </summary>
        /// <param name="domainName">The name of the domain.</param>
        /// <returns>System.DirectoryServices.ActiveDirectory.Domain.</returns>
        public ADDomain GetDomain(string domainName)
        {
            var context = new DirectoryContext(DirectoryContextType.Domain, domainName);
            return ADDomain.GetDomain(context);
        }

        /// <summary>
        /// Get a collection of Domain Controllers for the domain.
        /// </summary>
        /// <param name="domainName">The name of the domain.</param>
        /// <returns>System.DirectoryServices.ActiveDirectory.</returns>
        public DomainControllerCollection GetDomainControllers(string domainName)
        {
            var domain = GetDomain(domainName);
            return domain.DomainControllers;
        }

        /// <summary>
        /// Get the domain SID.
        /// </summary>
        /// <returns>String.</returns>
        public string GetDomainSid()
        {
            var filter = "(userAccountControl:1.2.840.113556.1.4.803:=8192)";
            var ldap = new LDAP(_searcher);

            var properties = new string[] { "objectsid" };
            var domainControllers = ldap.ExecuteQuery(filter, properties);

            if (domainControllers is null || domainControllers.Count == 0)
                throw new DomainException("Could not locate Domain Controllers for the domain");

            var firstDc = domainControllers.Values.First();
            var dcSidBytes = firstDc["objectsid"].First() as byte[];
            var dcSid = new SecurityIdentifier(dcSidBytes, 0);

            return dcSid.Value.Substring(0, dcSid.Value.LastIndexOf('-'));
        }
    }

    public class DomainException : Exception
    {
        public DomainException() { }

        public DomainException(string message) : base(message) { }

        public DomainException(string message, Exception inner) : base(message, inner) { }
    }
}
