using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Procrastiwiki.Model.Html;

namespace Procrastiwiki.Core
{
    public class HtmlParser : IHtmlParser
    {
        public string Url { get; private set; }
        private string htmlBody;
        private List<Tag> tagList;

        public HtmlParser(string urlOrHtml)
        {
            if (urlOrHtml.StartsWith("http"))
                Url = urlOrHtml;
            else
                htmlBody = urlOrHtml;
        }

        public Model.Html.Tag ReadTagValue(string tagName)
        {
            tagList = new List<Tag>();
            return readTagsByType(tagName, 0).FirstOrDefault();
        }

        public IEnumerable<Tag> ReadTagsByType(string tagType)
        {
            tagList = new List<Tag>();
            return readTagsByType(tagType, 0);
        }


        private ICollection<String> linksToRead;
        public IEnumerable<String> SearchValidLinks()
        {
            linksToRead = new List<string>();
            return SearchValidLinks(0);
        }

        private IEnumerable<String> SearchValidLinks( int lastIndex = 0)
        {
            if (lastIndex == 0) linksToRead = new List<String>();

            var linkTagStart = GetHtmlBody().IndexOf("<a ", lastIndex);
            if (linkTagStart > -1)
            {
                var linkTagEnd = GetHtmlBody().IndexOf(">", linkTagStart);
                var linkStart = GetHtmlBody().IndexOf("href=\"", linkTagStart) + "href=\"".Length;
                var linkEnd = GetHtmlBody().IndexOf("\"", linkStart);
                linksToRead.Add(GetHtmlBody().Substring(linkStart, linkEnd - linkStart));
                return SearchValidLinks(linkTagEnd);
            }
            else
            {
                return linksToRead;
            }
        }

        private string GetHtmlBody()
        {
            if (String.IsNullOrEmpty(htmlBody))
            {
                using (var httpClient = new HttpClient())
                {
                    htmlBody = httpClient.GetStringAsync(Url).Result;
                }
            }

            return htmlBody;
        }

        private IEnumerable<Model.Html.Tag> readTagsByType(string tagType, int fromIndex = 0)
        {
            var firstTagStart = GetHtmlBody().IndexOf("<" + tagType, fromIndex);
            if (firstTagStart > -1)
            {
                var firstTagEnd = GetHtmlBody().IndexOf(">", firstTagStart) + ">".Length;
                var firstTagContent = GetHtmlBody().Substring(firstTagStart, firstTagEnd - firstTagStart);

                var tagEnd = GetHtmlBody().IndexOf("</" + tagType.Trim() + ">", fromIndex);

                var tagValue = GetHtmlBody().Substring(firstTagEnd, tagEnd - firstTagEnd);

                var tag = new Model.Html.Tag(tagType, tagValue);
                var properties = readTagProperties(firstTagContent);
                foreach (var propertie in properties)
                    tag.AddProperty(propertie.Key, propertie.Value);

                tagList.Add(tag);

                return readTagsByType(tagType, tagEnd + ("</" + tagType + ">").Length);
            }
            else
            {
                return this.tagList;
            }
        }

        private IEnumerable<KeyValuePair<string, string>> readTagProperties(string firstTagContent)
        {
            var returnProperties = new List<KeyValuePair<String, String>>();

            if (firstTagContent.IndexOf(" ") > -1)
            {
                string propertiesArea = firstTagContent.Substring(firstTagContent.IndexOf(" "));
                string[] properties = propertiesArea.Split(new string[] {"\""}, StringSplitOptions.None);

                for (int i = 0; i < properties.Length - 1; i++)
                {
                    var key = properties[i].Trim().Replace("=","");
                    i++;
                    var value = properties[i];

                    returnProperties.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            return returnProperties;
        }
        
    }
}
