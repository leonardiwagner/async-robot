using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Procrastiwiki.Core;

namespace Procrastiwiki.Test.Unit
{
    [TestFixture]
    public class HtmlParserTest
    {
        public IHtmlParser htmlReader;

        [SetUp, Test]
        public void LoadHtmlToTest()
        {
            htmlReader = new HtmlParser(
                new StringBuilder("<html>")
                          .Append("  <head>")
                          .Append("    <title>This is the page title</title>")
                          .Append("  </head>")
                          .Append("  <body>")
                          .Append("    <a href=\"/Wiki/TestPage\">Test Page Title</a>")
                          .Append("    <a href=\"/Wiki/TestPage2\">Test Page Title2</a>")
                          .Append("  </body>")
                          .Append("</html>")
                .ToString()
            );
        }

        [Test]
        public void HtmlParseShouldReadTagValue()
        {
            var titleTag = htmlReader.ReadTagValue("title");
            
            titleTag.Name.Should().Be("title");
            titleTag.Value.Should().Be("This is the page title");
        }

        [Test]
        public void HtmlParseShouldReadLinkTags()
        {
            var linkTags = htmlReader.ReadTagsByType("a");
            
            linkTags.Count().Should().Be(2);
        }

        [Test]
        public void HtmlParseShouldReadCorrectLinkTagValues()
        {
            var linkTags = htmlReader.ReadTagsByType("a");
            var linkTag = linkTags.Where(x => x.Value == "Test Page Title2").First();

            linkTag.GetProperties().Where(x => x.Key == "href").First().Value
                .Should().Be("/Wiki/TestPage2");
        }
    }
}
