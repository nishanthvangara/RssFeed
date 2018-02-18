using ApplicationForRSS.Models;
using System;
using System.Text;
using System.Xml;

namespace ApplicationForRSS.Helpers
{
    public static class XmlToRSS
    {
        public static RssFeedDto GetDataAndParseToDto(string url)
        {
            XmlDocument rssXmlDoc = new XmlDocument();

            // Load the RSS file from the RSS URL
            rssXmlDoc.Load(url);

            // Parse the Items in the RSS file
            XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

            StringBuilder rssContent = new StringBuilder();
            RssFeedDto rss = new RssFeedDto();
            // Iterate through the items in the RSS file
            foreach (XmlNode rssNode in rssNodes)
            {
                XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                string title = rssSubNode != null ? rssSubNode.InnerText : "";
                rss.Header = title;

                rssSubNode = rssNode.SelectSingleNode("link");
                string link = rssSubNode != null ? rssSubNode.InnerText : "";
                rss.Source = link;

                rssSubNode = rssNode.SelectSingleNode("description");
                string description = rssSubNode != null ? rssSubNode.InnerText : "";
                rss.Description = description;

                rssSubNode = rssNode.SelectSingleNode("pubDate");
                string date = rssSubNode != null ? rssSubNode.InnerText : "";
                rss.DateTime = Convert.ToDateTime(date);

                rss.CreateDateTime = DateTime.Now;
                rssContent.Append("<a href='" + link + "'>" + title + "</a><br>" + description);
            }         
            return rss;
        }
    }
}
