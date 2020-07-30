using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSS_Reader.Models.Enums
{
    public class BaseEnums
    {
        public enum CacheDuration
        {
            /// <summary>
            /// 4 minutes cache
            /// </summary>
            Small,

            /// <summary>
            /// 30 minutes cache
            /// </summary>
            Medium,

            /// <summary>
            /// 2 hours cache
            /// </summary>
            Large,

            /// <summary>
            /// 4 hours cache
            /// </summary>
            Huge,
        }

        public enum ResponseType
        {
            Unknown = 0,
            Success = 1,
            BadUrl = 2,
            NoResult = 3
        }
    }
}