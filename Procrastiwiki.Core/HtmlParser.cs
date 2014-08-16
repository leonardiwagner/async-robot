using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Procrastiwiki.Model.Html;

namespace Procrastiwiki.Core
{
    public class HtmlParser : IHtmlParser
    {
        private readonly string htmlBody;
        private List<Tag> tagList;

        public HtmlParser(string htmlBody)
        {
            this.htmlBody = htmlBody;
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

        private IEnumerable<Model.Html.Tag> readTagsByType(string tagType, int fromIndex = 0)
        {
            var firstTagStart = htmlBody.IndexOf("<" + tagType, fromIndex);
            if (firstTagStart > -1)
            {
                var firstTagEnd = htmlBody.IndexOf(">", firstTagStart) + ">".Length;
                var firstTagContent = htmlBody.Substring(firstTagStart, firstTagEnd - firstTagStart);

                var tagEnd = htmlBody.IndexOf("</" + tagType + ">", fromIndex);

                var tagValue = htmlBody.Substring(firstTagEnd, tagEnd - firstTagEnd);

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

            string propertiesArea = firstTagContent.Substring(firstTagContent.IndexOf(" "));
            string[] properties = propertiesArea.Split(new string[]{"=\""},StringSplitOptions.None);

            for (int i = 0; i < properties.Length; i++)
            {
                var key = properties[i].Trim();
                i++;
                var endOfValue = properties[i].IndexOf("\"");
                var value = properties[i].Substring(0, endOfValue);

                returnProperties.Add(new KeyValuePair<string, string>(key, value));
            }

            return returnProperties;
        }
        
    }
}
