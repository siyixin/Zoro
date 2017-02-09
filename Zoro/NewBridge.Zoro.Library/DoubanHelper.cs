using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.Zoro.Library
{
    public class DoubanHelper
    {
        const string api = "http://api.douban.com/v2/movie/search?q=";
        const string summary = "http://api.douban.com/v2/movie/subject/";

        public static Subject GetChosen(string movie)
        {
            string url = api + movie;
            Subject subject = new Subject();
            var searchResult = DoubanHelper._download_serialized_json_data<Rootobject>(url);
            if(searchResult.total != 0)
            {
                //int chosen = searchResult.subjects.Max(s => s.collect_count);
                //subject = searchResult.subjects.Single<Subject>(e => e.collect_count >= chosen);
                subject = searchResult.subjects[0];
            }
            return subject;
        }

        public static String GetSummary(string id)
        {
            string url = summary + id;
            string result = string.Empty;
            var summaryResult = DoubanHelper._download_serialized_json_data<Summaryobject>(url);
            if (summaryResult.id == id)
                result = summaryResult.summary;
            return result;
        }

        public static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }

        public static bool _download_image(string url)
        {
            bool done = false;
            using (var w = new WebClient())
            {
                //string file = Directory.GetCurrentDirectory() + "\\images\\" + _get_filename(url);
                string file = ConfigurationManager.AppSettings["Path"] + _get_filename(url);
                try
                {
                    w.DownloadFileAsync(new Uri(url), file);
                    done = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return done;
        }

        public static string _get_filename(string url)
        {
            char seperator = '/';
            int index = url.LastIndexOf(seperator);
            return url.Substring(index + 1, url.Length - index - 1);
        }

        public static void Output(Subject subject)
        {
            Console.WriteLine("{0}:{1}[{2}]", subject.id, subject.title, subject.original_title);
            Console.WriteLine("评分:{0}\t年份:{1}\t分类:{2}", subject.rating.average, subject.year, subject.subtype);
            Console.Write("风格:");
            foreach (string genre in subject.genres)
            {
                Console.Write("{0}\t", genre);
            }
            Console.WriteLine();
            Console.Write("演员:");
            foreach (Cast cast in subject.casts)
            {
                Console.Write("{0}\t", cast.name);
            }
            Console.WriteLine();
            Console.Write("导演:");
            foreach (Director director in subject.directors)
            {
                Console.Write("{0}\t", director.name);
            }
            Console.WriteLine();
            //Console.WriteLine("观影人数:{0}",subject.collect_count);
            Console.WriteLine("URL:{0}", subject.alt);
            Console.WriteLine("海报:{0}", subject.images.large);
        }

        //public static DataTable GetMovies()
        //{
        //    string sql = "SELECT id,title,original_title,rating,image,category,year,genre,directors,casts FROM movie";
        //    return DBHelper.GetDataSet(sql);
        //}

        public static DataTable GetMovie(string category)
        {
            string sql = "SELECT id,title,original_title,rating,image,category,year,genre,directors,casts,summary FROM movie WHERE category = '"+category+"'";
            if(category == "top10")
            sql = "SELECT id,title,original_title,rating,image,category,year,genre,directors,casts,summary FROM movie ORDER BY rating DESC limit 1,10";
            return DBHelper.GetDataSet(sql);
        }

        public static IEnumerable<string> GetCategories()
        {
            string sql = "SELECT category FROM movie GROUP BY category ORDER BY category DESC LIMIT 0,9";
            DataTable dt = DBHelper.GetDataSet(sql);
            int num = dt.Rows.Count;
            string[] categories = new string[num];
            for (int i= 0;i<num;i++)
            {
                categories[i] = (string)dt.Rows[i][0];
            }
            return categories;
        }
    }
}
