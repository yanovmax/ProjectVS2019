using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static RSS_Reader.Models.Enums.BaseEnums;

namespace RSS_Reader.Models.RSS
{
    public class NewFeedData
    {
        public string Name { get; set; }
        public string LinkAnchor { get; set; }
        public ResponseType ResponseType { get; set; }

        public NewFeedData()
        {
            this.Name = "";
            this.LinkAnchor = "";
            this.ResponseType = ResponseType.Unknown;
        }
    }
}