using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using System.Threading.Tasks;


namespace Core
{
    public class Website
    {
        public readonly string Address = "http://en.wikipedia.org/";

        public IEnumerable<String> GetUrlsInWebpage(string url)
        {
            var body = ReadAsync(url);
            return this.GetUrlsInWebpage(body);
        }

        private ICollection<String> linksToRead;

        public IEnumerable<String> SearchValidLinks(string htmlBody, int lastIndex = 0)
        {
            if (lastIndex == 0) linksToRead = new List<String>();

            var linkTagStart = htmlBody.IndexOf("<a", lastIndex);
            if (linkTagStart > -1)
            {
                var linkTagEnd = htmlBody.IndexOf(">", linkTagStart);
                var linkStart = htmlBody.IndexOf("href=\"", linkTagStart);
                var linkEnd = htmlBody.IndexOf("\"", linkStart);
                linksToRead.Add(htmlBody.Substring(linkStart, linkEnd - linkStart));
                return SearchValidLinks(htmlBody, linkTagEnd);
            }
            else
            {
                return linksToRead;
            }
        }

        public String ReadAsync(string url)
        {
            var httpClient = new HttpClient();
            return httpClient.GetStringAsync(url).Result;
        }
    }
}
