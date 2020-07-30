using RSS_Reader.Helpers;
using RSS_Reader.Models.RSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;

namespace RSS_Reader.Code.DL.RSS
{
    public class RssReader
    {
        public RssFeed OnGetFeed(string url)
        {
            try
            {
                RssFeed rssFeed = null;
                using (XmlReader reader = XmlReader.Create(url))
                {
                    var formatter = new Rss20FeedFormatter();
                    formatter.ReadFrom(reader);

                    var DataFeed = formatter.Feed as SyndicationFeed;
                    var DataFeedItems = formatter.Feed.Items; //{System.ServiceModel.Syndication.SyndicationItem}

                    if (DataFeed != null)
                    {
                        rssFeed = MappingDataFeed(DataFeed);
                    }

                    return rssFeed;
                }
                
            }
            catch (System.IO.FileNotFoundException)
            {
                //throw new Exception("File " + url + "not found");
                Log.Write("File " + url + "not found", "DL", "RSS_Reader", "OnGetFeed");
                return null;
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, "DL", "RSS_Reader", "OnGetFeed");
                return null;
            }
        }


        public RssFeed MappingDataFeed(SyndicationFeed data)
        {
            RssFeed dataFeed = null;
            try
            {
                dataFeed = new RssFeed();
                if (data != null)
                {
                    dataFeed.Title = data.Title.Text;
                    dataFeed.Description = data.Description.Text;
                    var urls = data.Links.Select(l => l.Uri.AbsoluteUri).ToList();
                    if (urls != null && urls.Any())
                        dataFeed.Links = urls;

                    if (data.Items != null && data.Items.Any())
                    {
                        dataFeed.RssItems = new List<RssItem>();
                        foreach (var item in data.Items)
                        {
                            dataFeed.RssItems.Add(new RssItem()
                            {
                                Title = item.Title.Text,
                                Link = item.Links.Any() ? item.Links[0].Uri.ToString() : "",
                                Description = item.Summary.Text,
                                Image = item.Links.Count > 1 ? item.Links[1].Uri.ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, "DL", "RSS_Reader", "MappingDataFeed");
                return null;
            }

            return dataFeed;
        }
    }
}