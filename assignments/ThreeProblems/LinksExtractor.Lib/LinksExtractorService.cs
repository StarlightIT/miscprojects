using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinksExtractor.Lib
{
    public class LinksExtractorService
    {
        private readonly ILogger<LinksExtractorService> _logger;

        public LinksExtractorService(ILogger<LinksExtractorService> logger)
        {
            _logger = logger;
        }

        public List<string> ExtractLinks(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            var aNodes = doc.DocumentNode.Descendants("a");

            List<string> links = new List<string>();

            foreach(var a in aNodes)
            {
                var href = a.Attributes["href"].Value;
                _logger.LogInformation(href);
                links.Add(href);
            }

            return links;
        }
    }
}
