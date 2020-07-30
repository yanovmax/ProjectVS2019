using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ServiceModel.Syndication;
using System.Xml;
using RSS_Reader.Code.DL.RSS;
using RSS_Reader.Code.BL;

namespace RSS_Reader.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   
            return View();
        }

        public JsonResult AddNewFeed(string name, string link)
        {
            var jsonResult = new FeedManager().AddNewFeed(link);
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFavorites()
        {
            return View();
        }

        public ActionResult ShowFeedNews(string link)
        {
            var RssFeed = new FeedManager().ShowFeedNews(link);
            return View(RssFeed);
        }
    }
}