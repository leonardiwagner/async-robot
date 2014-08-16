using System;
using System.Net.Http;
using System.Threading.Tasks;
using Procrastiwiki.Model.Wiki;

namespace Procrastiwiki.Core
{
    public class WikiReader : IWikiReader
    {
        public Model.Wiki.Page ReadFromUrl(string url)
        {
            string urlBody;
            using (var httpClient = new HttpClient())
            {
               // urlBody = httpClient.GetStringAsync(url);
            }

            return new Page("","");
        }

        
    }
}
