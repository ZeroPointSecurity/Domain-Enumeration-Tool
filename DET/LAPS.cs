using System.Collections.Generic;

namespace DET
{
    public class LAPS
    {
        readonly DomainSearcher _searcher;

        /// <summary>
        /// Initializes a new instance of the DET.LAPS class.
        /// </summary>
        /// <param name="searcher">An instance of the DET.DomainSearcher class.</param>
        public LAPS(DomainSearcher searcher)
        {
            _searcher = searcher;
        }

        /// <summary>
        /// Get computers were the ms-Mcs-AdmPwdExpirationTime attribute is not null
        /// </summary>
        /// <param name="properties">An array of properties to return.</param>
        /// <returns>A multi-level dictionary of domain and its properties.</returns>
        public Dictionary<string, Dictionary<string, object[]>> GetComputersWithLAPS(string[] properties = null)
        {
            var filter = "(&(objectCategory=computer)(ms-Mcs-AdmPwdExpirationTime=*))";
            var ldap = new LDAP(_searcher);

            return ldap.ExecuteQuery(filter, properties);
        }
    }
}
