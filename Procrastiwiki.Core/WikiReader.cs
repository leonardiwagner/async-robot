using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        public async Task<List<string>> SearchRelatedLinksA(IHtmlParser htmlParser, int howManyTimes, List<string> linksToReturn = null, int i = 0)
        {
            var name = htmlParser.ReadTagValue("title").Value;

            Random rand = new Random();
            var links = await htmlParser.SearchValidLinksAsync()
                            .Where(x => x.StartsWith("/wiki/") && !x.Contains(":"))
                            .OrderBy(c => rand.Next()).Select(c => c)
                            .Take(10)
                            .ToList();


            if (linksToReturn == null) linksToReturn = new List<string>();
            linksToReturn.AddRange(links);

            i++;
            if (i > howManyTimes) return linksToReturn;

            foreach (var x in links)
                Console.WriteLine(i.ToString() + "|" + htmlParser.Url + "|" + x);

            foreach (var url in links)
            {

                return SearchRelatedLinks(new HtmlParser("http://en.wikipedia.org" + url), howManyTimes, linksToReturn, i);
            }

            return linksToReturn;
        }

        private int v = 0;

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


            v++;

            //if(!name.Contains("-"))
              //  Console.WriteLine(htmlParser.Url);
            Console.WriteLine(name);
            //Console.WriteLine(System.Threading.Thread.CurrentThread.Name);
            //Console.WriteLine(Process.GetCurrentProcess().Threads.Count);

            if (v > howManyTimes) return linksToReturn;

            var a = 0;
            foreach (var url in links)
            {
                a++;
                // todo precisa ignorar "Main_page"
                /*lock(new object()){
                    Console.WriteLine(i.ToString() + " de " + a.ToString() + "|" + htmlParser.Url);
                }*/

                
                var thread = new Thread(() =>
                {
                    SearchRelatedLinksT(new HtmlParser("http://en.wikipedia.org" + url), howManyTimes, linksToReturn);
                });

                thread.Name = "Wiki_" + v.ToString() + "_" + a.ToString();
                thread.Start();
            }
            

            return linksToReturn;
        }

    }
}
