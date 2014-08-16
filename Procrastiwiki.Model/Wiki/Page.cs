using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procrastiwiki.Model.Wiki
{
    public class Page
    {
        public String Name { get; private set; }
        public String Url { get; private set; }
        public IEnumerable<KeyValuePair<string, string>> Links { get { return links; } }

        private ICollection<Wiki.Page> pagesInsideThisWiki;
        private ICollection<KeyValuePair<string, string>> links;


        public Page(String name, String url)
        {
            Name = name;
            Url = url;
            pagesInsideThisWiki = new List<Wiki.Page>();
            links = new List<KeyValuePair<string, string>>();
        }

        public void AddLink(string name, string url)
        {
            links.Add(new KeyValuePair<string, string>(name, url));
        }

        public void AddPageToThisWiki(Wiki.Page page)
        {
            pagesInsideThisWiki.Add(page);
        }

        public IEnumerable<Wiki.Page> GetPagesFromThisWiki()
        {
            return pagesInsideThisWiki;
        } 
    }
}
