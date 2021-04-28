using System.Collections.Generic;

namespace DET
{
    public class Users
    {
        readonly DomainSearcher _searcher;

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
                {
                    filter += $"(samaccountname=*{userName}*)";
                }

                filter += ")";
            }

            filter += ")";

            return ldap.ExecuteQuery(filter, properties);
        }
    }
}
