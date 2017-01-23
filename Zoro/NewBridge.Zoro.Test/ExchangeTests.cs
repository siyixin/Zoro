using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net.Mail;
using System.Collections.Generic;
using System.Collections;
using NewBridge.Zoro.Exchange;

namespace NewBridge.Zoro.Test
{
    [TestClass]
    public class ExchangeTests
    {
        [TestMethod]
        public void CompareTest()
        {
            string word1 = "易泰电影列表，日期2016/12/8";
            string word2 = "易泰电影列表，日期2016/12/27";

            MailMessage msg1 = new MailMessage();
            msg1.Subject = word1;

            MailMessage msg2 = new MailMessage();
            msg2.Subject = word2;

            List<MailMessage> msgs = new List<MailMessage>();
            msgs.Add(msg1);
            msgs.Add(msg2);

            SubjectComparer comparer = new SubjectComparer();
            IOrderedEnumerable<MailMessage> orderedmsgs = msgs.Where(m => m.Subject.Contains("易泰")).OrderByDescending(m => m.Subject,comparer);

            Assert.IsTrue(orderedmsgs.First().Subject.Equals("易泰电影列表，日期2016/12/27"));
        }
    }
}
