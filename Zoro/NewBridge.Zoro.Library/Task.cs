using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.Zoro.Library
{
    public class Task
    {
        public string Name { get; set; }
        public string Category { get; set; }

        private char _seperator  = '\\';
        private char _extention = '.';
        public Task(string line)
        {
            //"\\10.63.41.50\share\电影\2016.05\哭泣的男人BD韩语中字.rmvb"
            string[] words = line.Split(new char[] { _seperator });
            this.Name = words.Last().Split(new char[] { _extention})[0];
            for (int i = 0; i < words.Length; i++)
                if (words[i] == "电影")
                    this.Category = words[i + 1];
        }
    }
}
