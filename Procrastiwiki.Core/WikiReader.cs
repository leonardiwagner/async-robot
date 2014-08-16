using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
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



        public List<string> SearchRelatedLinks(IHtmlParser htmlParser, int howManyTimes,List<string> linksToReturn = null, int i = 0)
        {
            var name = htmlParser.ReadTagValue("title").Value;

            Random rand = new Random();
            var links = htmlParser.SearchValidLinks()
                            .Where(x => x.StartsWith("/wiki/") && !x.Contains(":"))
                            .OrderBy(c => rand.Next()).Select(c => c)
                            .Take(10)
                            .ToList();


            if (linksToReturn == null) linksToReturn = new List<string>();
            linksToReturn.AddRange(links);

            i++;
            if (i > howManyTimes) return linksToReturn;

            foreach(var x in links)
                Console.WriteLine(i.ToString() + "|" + htmlParser.Url + "|" + x);
            
            foreach (var url in links)
            {
                
                return SearchRelatedLinks(new HtmlParser("http://en.wikipedia.org" + url), howManyTimes, linksToReturn, i);
            }

            return linksToReturn;
        }

        public List<string> SearchRelatedLinksT(IHtmlParser htmlParser, int howManyTimes, List<string> linksToReturn = null, int i = 0)
        {
            var name = htmlParser.ReadTagValue("title").Value;

            Random rand = new Random();
            var links = htmlParser.SearchValidLinks()
                            .Where(x => x.StartsWith("/wiki/") && !x.Contains(":"))
                            .OrderBy(c => rand.Next()).Select(c => c)
                            .Take(10)
                            .ToList();


            if (linksToReturn == null) linksToReturn = new List<string>();
            linksToReturn.AddRange(links);




            i++;
            
            if (i > howManyTimes) return linksToReturn;

            var a = 0;
            foreach (var url in links)
            {
                a++;

                lock(new object()){
                    Console.WriteLine(i.ToString() + " de " + a.ToString() + "|" + htmlParser.Url);
                }

                var tempI = i;
                var x = new Thread(() =>
                {
                    SearchRelatedLinksT(new HtmlParser("http://en.wikipedia.org" + url), howManyTimes, linksToReturn,
                        tempI);
                });

                x.Start();
            }
            

            return linksToReturn;
        }
    }
}
