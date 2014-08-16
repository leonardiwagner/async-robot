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
        private ICollection<Wiki.Page> pagesInsideThisWiki;

        public Page(String name, String url)
        {
            Name = name;
            Url = url;
            pagesInsideThisWiki = new List<Wiki.Page>();
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
