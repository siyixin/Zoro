using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NewBridge.Zoro.Library;

namespace NewBridge.Zoro.Carousel.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = DoubanHelper.GetCategories();
            return PartialView(categories);
        }
    }
}