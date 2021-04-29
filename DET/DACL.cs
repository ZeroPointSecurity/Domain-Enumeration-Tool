using System.DirectoryServices;
using System.Linq;
using System.Security.AccessControl;

namespace DET
{
    public class DACL
    {
        readonly DomainSearcher _searcher;

        /// <summary>
        /// Initializes a new instance of the DET.DACL class.
        /// </summary>
        /// <param name="searcher">An instance of the DET.DomainSearcher class.</param>
        public DACL(DomainSearcher searcher)
        {
            _searcher = searcher;
        }

        /// <summary>
        /// Get an Discretionary Access Control List.
        /// </summary>
        /// <param name="distinguishedName">The target DN.</param>
        /// <returns>RawAcl.</returns>
        public RawAcl GetDacl(string distinguishedName)
        {
            var ldap = new LDAP(_searcher, SecurityMasks.Dacl);
            var filter = $"(distinguishedname={distinguishedName})";
            var results = ldap.ExecuteQuery(filter, new string[] { "ntsecuritydescriptor" });

            var rawDacl = results.First().Value["ntsecuritydescriptor"][0] as byte[];

            var descriptor = new RawSecurityDescriptor(rawDacl, 0);
            return descriptor.DiscretionaryAcl;
        }
    }
}
