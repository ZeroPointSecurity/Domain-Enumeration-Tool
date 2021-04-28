using System.Collections.Generic;

namespace DET
{
    public class OrganizationalUnits
    {
        readonly DomainSearcher _searcher;

        /// <summary>
        /// Initializes a new instance of the DET.OrganizationalUnits class.
        /// </summary>
        /// <param name="searcher">An instance of the DET.DomainSearcher class.</param>
        public OrganizationalUnits(DomainSearcher searcher)
        {
            _searcher = searcher;
        }

        /// <summary>
        /// Get all Organizational Units and their properties.
        /// </summary>
        /// <returns>A multi-level dictionary of OUs and their properties.</returns>
        public Dictionary<string, Dictionary<string, object[]>> GetOrganizationalUnits()
        {
            var ldap = new LDAP(_searcher);
            var filter = "(&(objectCategory=organizationalUnit))";

            return ldap.ExecuteQuery(filter);
        }

        /// <summary>
        /// Get the specified Organizational Units and their properties.
        /// </summary>
        /// <param name="ouNames">Limit the response to the these OU names.</param>
        /// <param name="properties">An array of properties to return.</param>
        /// <returns>A multi-level dictionary of OUs and their properties.</returns>
        public Dictionary<string, Dictionary<string, object[]>> GetOrganizationalUnits(string[] ouNames, string[] properties = null)
        {
            var ldap = new LDAP(_searcher);
            var filter = "(&(objectCategory=organizationalUnit)";

            filter += "(|";

            foreach (var ouName in ouNames)
            {
                filter += $"(name=*{ouName}*)";
            }

            filter += "))";

            return ldap.ExecuteQuery(filter, properties);
        }
    }
}
