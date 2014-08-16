using System;
using System.Collections.Generic;

namespace Procrastiwiki.Core
{
    public interface IWikiReader
    {
        Model.Wiki.Page ReadFromUrl(IHtmlParser htmlParser);

        List<string> SearchRelatedLinks(IHtmlParser htmlParser, int howManyTimes,List<string> linksToReturn = null, int i = 0);
    }
}
