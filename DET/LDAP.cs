using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;

namespace DET
{
    public class LDAP
    {
        readonly DomainSearcher _searcher;

        /// <summary>
        /// Initializes a new instance of the DET.LDAP class.
        /// </summary>
        /// <param name="searcher">An instance of the DET.DomainSearcher class.</param>
        public LDAP(DomainSearcher searcher)
        {
            _searcher = searcher;
        }

        /// <summary>
        /// Execute a raw LDAP query.
        /// </summary>
        /// <param name="filter">The LDAP filter.</param>
        /// <param name="properties">Optional parameters.</param>
        /// <returns>A multi-level dictionary of LDAP properties and an array of their values.</returns>
        public Dictionary<string, Dictionary<string, object[]>> ExecuteQuery(string filter, string[] properties = null)
        {
            var searcher = new DirectorySearcher(_searcher.Directory)
            {
                Filter = filter
            };

            if (properties is not null)
            {
                searcher.PropertiesToLoad.AddRange(properties);
            }

            var searchResultCollection = searcher.FindAll();

            var resultDictionary = new Dictionary<string, Dictionary<string, object[]>>();

            foreach (SearchResult searchResult in searchResultCollection)
            {
                resultDictionary.Add(searchResult.Path, null);

                var dictionary = new Dictionary<string, object[]>();

                foreach (DictionaryEntry entry in searchResult.Properties)
                {
                    var values = new List<object>();

                    foreach (var value in entry.Value as ResultPropertyValueCollection)
                    {
                        values.Add(value);
                    }

                    dictionary.Add(entry.Key.ToString(), values.ToArray());
                }

                resultDictionary[searchResult.Path] = dictionary;
            }

            return resultDictionary;
        }
    }
}
