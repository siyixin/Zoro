using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.Zoro.Exchange
{
    public class SubjectComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            x = new String(x.Skip(9).ToArray());
            DateTime dtx = DateTime.Parse(x);
            y = new String(y.Skip(9).ToArray());
            DateTime dty = DateTime.Parse(y);
            return dtx.CompareTo(dty);
        }
    }
}
