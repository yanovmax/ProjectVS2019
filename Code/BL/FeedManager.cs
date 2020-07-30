using RSS_Reader.Code.DL.RSS;
using RSS_Reader.Helpers;
using RSS_Reader.Models.Enums;
using RSS_Reader.Models.RSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace RSS_Reader.Code.BL
{
    public class FeedManager
    {
        public FeedManager()
        { }

        public string AddNewFeed(string textUrl)
        {
            //textUrl = "https://www.nasa.gov/rss/dyn/lg_image_of_the_day.rss";
            var cacheKey = textUrl;
            var jsonResult = string.Empty;
            RssFeed rssFeed = null;
            NewFeedData newFeedData = new NewFeedData();

            var regex = "^(http:\\/\\/www\\.|https:\\/\\/www\\.|http:\\/\\/|https:\\/\\/)[a-z0-9]+([\\-\\.]{1}[a-z0-9]+)*\\.[a-z]{2,5}(:[0-9]{1,5})?(\\/.*)?$";
            try
            {
                var match = Regex.Match(textUrl, regex, RegexOptions.IgnoreCase);

                if (!match.Success)
                {
                    newFeedData.ResponseType = BaseEnums.ResponseType.BadUrl;
                    jsonResult = new JavaScriptSerializer().Serialize(newFeedData);

                    return jsonResult;
                }

                rssFeed = Caching.GetCache(cacheKey) as RssFeed;

                if (rssFeed == null)
                {
                    RssReader rssReader = new RssReader();
                    rssFeed = rssReader.OnGetFeed(textUrl);
                

                    //if (rssFeed != null)
                    //{
                    //    Caching.SetCache(cacheKey, rssFeed, BaseEnums.CacheDuration.Small);
                    //}
                }
                if(rssFeed != null)
                {
                    Caching.SetCache(cacheKey, rssFeed, BaseEnums.CacheDuration.Small);

                    //newFeedData = new NewFeedData()
                    //{
                    //    Name = rssFeed.Title,
                    //    LinkAnchor = rssFeed.Links[1],
                    //    ResponseType = BaseEnums.ResponseType.Success;
                    //};
                    newFeedData.Name = rssFeed.Title;
                    newFeedData.LinkAnchor = rssFeed.Links[1];
                    newFeedData.ResponseType = BaseEnums.ResponseType.Success;

                    jsonResult = new JavaScriptSerializer().Serialize(newFeedData);
                }
                else
                {
                    newFeedData.ResponseType = BaseEnums.ResponseType.NoResult;
                }

                return jsonResult;
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, "BL", "FeedManager", "AddNewFeed");
                return string.Empty;
            }
        }

        public RssFeed ShowFeedNews(string LinkAnchor)
        {
            var cacheKey = LinkAnchor;
            RssReader rssReader = null;
            
            try
            {
                RssFeed rssFeed = Caching.GetCache(LinkAnchor) as RssFeed;

                if (rssFeed != null && rssFeed.RssItems != null && rssFeed.RssItems.Any())
                {
                    return rssFeed;
                }
                else
                {
                    rssReader = new RssReader();
                    rssFeed = rssReader.OnGetFeed(LinkAnchor);
                    if (rssFeed != null)
                    {
                        Caching.SetCache(cacheKey, rssFeed, BaseEnums.CacheDuration.Small);
                    }

                    return rssFeed;
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, "BL", "FeedManager", "ShowFeedNews");
                return null;
            }   
        }
    }
}