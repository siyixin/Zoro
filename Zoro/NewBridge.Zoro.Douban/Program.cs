using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using Newtonsoft.Json;
using System.IO;
using NewBridge.Zoro.Library;
using System.Threading;
using System.Configuration;
using System.Timers;

namespace NewBridge.Zoro.Douban
{
    /// <summary>
    /// 公开（对应Required Scope URI ：movie_basic_r, movie_basic_w）- 所有开发者均可申请，审核通过。限定40次请求/分钟。开放基本数据接口，一般的个人爱好者自建网站或应用都会满足。
    /// </summary>
    class Program
    {
        private static System.Timers.Timer aTimer;
        private static List<Library.Task> tasks;
        private static string path;
        private static int i;

        static void Main(string[] args)
        {
            GetTasks();
            SetTimer();

            Console.WriteLine("豆瓣电影数据采集任务开始于：{0}", DateTime.Now.ToLongTimeString());

            i = 0;
            Console.ReadLine();
            while(i == tasks.Count)
            {
                aTimer.Stop();
                aTimer.Dispose();
                Console.WriteLine("豆瓣电影数据采集任务结束于：{0}", DateTime.Now.ToLongTimeString());
                break;
            }
            
        }

        private static void SetTimer()
        {
            double interval = double.Parse(ConfigurationManager.AppSettings["Interval"]);
            aTimer = new System.Timers.Timer(interval);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void GetTasks()
        {
            path = ConfigurationManager.AppSettings["Result"];
            //获得待处理的电影条目
            tasks = FileHelper.GetTasks(path);
            Console.WriteLine("待处理的电影条目『{0}』条",tasks.Count);
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("{0}:{1}:{2}", i+1, tasks[i].Name, e.SignalTime.ToLongTimeString());
            //第一次调用豆瓣API
            Subject subject = DoubanHelper.GetChosen(tasks[i].Name);
            if (subject.id != null)
            {
                //DoubanHelper.Output(subject);
                Console.WriteLine("{0}豆瓣返回信息", tasks[i].Name);
                if (!FileHelper.isExisted(DoubanHelper._get_filename(subject.images.large)))
                {
                    if (DoubanHelper._download_image(subject.images.large))
                        Console.WriteLine(string.Format("{0}海报下载完成...", subject.title));
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(string.Format("{0}海报下载失败...", subject.title));
                        Console.ResetColor();
                    }
                }
                string summary = DoubanHelper.GetSummary(subject.id);
                if (DBHelper.IsExsited(subject) == 0)
                {
                    DBHelper.InsertMovie(tasks[i], subject, summary);
                    Console.WriteLine("{0}入库完成...", subject.title);
                }
                else
                {
                    DBHelper.UpdateMovie(subject, summary);
                    Console.WriteLine("{0}更新完成...", subject.title);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0}豆瓣返回空值", tasks[i].Name);
                Console.ResetColor();
            }
            i++;
        }
    }
}
