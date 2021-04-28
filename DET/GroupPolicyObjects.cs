using System.Collections.Generic;

namespace DET
{
    public class GroupPolicyObjects
    {
        readonly DomainSearcher _searcher;

        /// <summary>
        /// Initializes a new instance of the DET.GroupPolicyObjects class.
        /// </summary>
        /// <param name="searcher">An instance of the DET.DomainSearcher class.</param>
        public GroupPolicyObjects(DomainSearcher searcher)
        {
            _searcher = searcher;
        }

        /// <summary>
        /// Get all Group Policy Objects and their properties.
        /// </summary>
        /// <returns>A multi-level dictionary of GPOs and their properties.</returns>
        public Dictionary<string, Dictionary<string, object[]>> GetGPOs()
        {
            var ldap = new LDAP(_searcher);
            var filter = "(&(objectCategory=groupPolicyContainer))";

            return ldap.ExecuteQuery(filter);
        }

        /// <summary>
        /// Get the specified GPOs and their properties.
        /// </summary>
        /// <param name="gpoNames">Limit the response to the these GPO display names.</param>
        /// <param name="properties">An array of properties to return.</param>
        /// <returns>A multi-level dictionary of GPOs and their properties.</returns>
        public Dictionary<string, Dictionary<string, object[]>> GetGPOs(string[] gpoNames, string[] properties = null)
        {
            var ldap = new LDAP(_searcher);
            var filter = "(&(objectCategory=groupPolicyContainer)";

            filter += "(|";

            foreach (var gpoName in gpoNames)
            {
                filter += $"(displayname=*{gpoName}*)";
            }

            filter += "))";

            return ldap.ExecuteQuery(filter, properties);
        }
    }
}
