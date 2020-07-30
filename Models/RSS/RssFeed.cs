using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSS_Reader.Models.RSS
{
    public class RssFeed
    {
        public string Title { get; set; } 
        public string Description { get; set; } 
        public List<string> Links{ get; set; } 
        public List<RssItem> RssItems { get; set; } 
    }
}