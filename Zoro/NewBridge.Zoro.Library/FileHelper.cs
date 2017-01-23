using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.Zoro.Library
{
    public class FileHelper
    {
        static string PATH = Directory.GetCurrentDirectory() + "\\images";
        public static List<Task> GetTasks(string path)
        {
            List<Task> tasks = new List<Task>();
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    if(s != string.Empty)
                    { 
                    Task t = new Task(s);
                    tasks.Add(t);
                    }
                }
            }
            return tasks;
        }

        /// <summary>
        /*
        var files = from file in
            Directory.EnumerateFiles(@"\\archives1\library\")
                    where file.ToLower().Contains("europe")
                    select file;*/
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static bool isExisted(string image)
        {
            bool result = false;
            if (Directory.EnumerateFiles(PATH).Where(f => f.Contains(image)).Count()!=0)
                result = true;
            return result;
        }
    }
}
