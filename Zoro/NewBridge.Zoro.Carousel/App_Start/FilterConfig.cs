﻿using System.Web;
using System.Web.Mvc;

namespace NewBridge.Zoro.Carousel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
