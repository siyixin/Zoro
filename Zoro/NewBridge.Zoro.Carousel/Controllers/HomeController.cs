using NewBridge.Zoro.Carousel.Models;
using NewBridge.Zoro.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewBridge.Zoro.Carousel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Movies(string category)
        {
            DataTable dt = DoubanHelper.GetMovie(category);
            List<Movie> list = new List<Movie>();
            foreach (DataRow dr in dt.Rows)
            {
                Movie m = new Models.Movie()
                {
                    Title = (string)dr["title"],
                    Rating = (decimal)dr["rating"],
                    Category = (string)dr["category"],
                    Image = (string)dr["image"],
                    Original_Title = (string)dr["original_title"],
                    Year = (int)dr["year"],
                    Genre = (string)dr["genre"],
                    Casts = (string)dr["casts"],
                    Directors = (string)dr["directors"],
                    Summary = dr["summary"] is DBNull ? string.Empty: (string)dr["summary"]
                };
                list.Add(m);
            }
            return View(list);
        }

        //public ActionResult Movies()
        //{
        //    DataTable dt = DoubanHelper.GetMovies();
        //    List<Movie> list = new List<Movie>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        Movie m = new Models.Movie()
        //        {
        //            Title = (string)dr["title"],
        //            Rating = (decimal)dr["rating"],
        //            Category = (string)dr["category"],
        //            Image = (string)dr["image"],
        //            Original_Title = (string)dr["original_title"],
        //            Year = (int)dr["year"],
        //            Genre = (string)dr["genre"],
        //            Casts = (string)dr["casts"],
        //            Directors = (string)dr["directors"]
        //        };
        //        list.Add(m);
        //    }
        //    return View(list);
        //}
    }
}