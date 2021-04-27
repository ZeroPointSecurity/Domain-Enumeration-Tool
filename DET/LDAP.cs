using System.DirectoryServices;

namespace DET
{
    public class LDAP
    {
        readonly DomainSearcher _searcher;

        public LDAP(DomainSearcher searcher)
        {
            _searcher = searcher;
        }

        public SearchResultCollection ExecuteQuery(string filter, string[] parameters = null)
        {
            var searcher = new DirectorySearcher(_searcher.Directory)
            {
                Filter = filter
            };

            if (parameters is not null)
            {
                searcher.PropertiesToLoad.AddRange(parameters);
            }

            return searcher.FindAll();
        }
    }
}
