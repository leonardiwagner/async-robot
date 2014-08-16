using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Procrastiwiki.Model.Wiki;

namespace Procrastiwiki.Core
{
    public class WikiReader : IWikiReader
    {
        public Page ReadFromUrl(IHtmlParser htmlParser)
        {
            var name = htmlParser.ReadTagValue("title").Value;
            var links = htmlParser.SearchValidLinks()
                            .Where(x => x.StartsWith("/wiki/") && !x.Contains(":"))
                            .Take(10)
                            .ToList();

            var page = new Page(name, htmlParser.Url);

            foreach (var link in links)
            {
                page.AddLink("", link);
            }

            return page;
        }
    }
}
