using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewBridge.Zoro.Carousel.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Original_Title { get; set; }
        public decimal Rating { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Casts { get; set; }
        public string Directors { get; set; }
        public string Summary { get; set; }
    }
}