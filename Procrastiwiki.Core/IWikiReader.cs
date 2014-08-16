using System;

namespace Procrastiwiki.Core
{
    public interface IWikiReader
    {
        Model.Wiki.Page ReadFromUrl(String url);
    }
}
