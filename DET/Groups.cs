﻿using System.Collections.Generic;

namespace DET
{
    public class Groups
    {
        readonly DomainSearcher _searcher;

        /// <summary>
        /// Initializes a new instance of the DET.Groups class.
        /// </summary>
        /// <param name="searcher">An instance of the DET.DomainSearcher class.</param>
        public Groups(DomainSearcher searcher)
        {
            _searcher = searcher;
        }

        /// <summary>
        /// Get all groups and their properties.
        /// </summary>
        /// <returns>A multi-level dictionary of groups and their properties.</returns>
        public Dictionary<string, Dictionary<string, object[]>> GetGroups()
        {
            var ldap = new LDAP(_searcher);
            var filter = "(&(objectCategory=group))";

            return ldap.ExecuteQuery(filter);
        }

        /// <summary>
        /// Get the specified groups and their properties.
        /// </summary>
        /// <param name="groupNames">Limit the response to the these group names.</param>
        /// <param name="properties">An array of properties to return.</param>
        /// <returns>A multi-level dictionary of groups and their properties.</returns>
        public Dictionary<string, Dictionary<string, object[]>> GetGroups(string[] groupNames, string[] properties = null)
        {
            var ldap = new LDAP(_searcher);
            var filter = "(&(objectCategory=group)";

            filter += "(|";

            foreach (var groupName in groupNames)
            {
                filter += $"(name=*{groupName}*)";
            }

            filter += "))";

            return ldap.ExecuteQuery(filter, properties);
        }
    }
}
