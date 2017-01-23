using S22.Imap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace NewBridge.Zoro.Exchange
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ImapClient imap = new ImapClient(ConfigurationManager.AppSettings["ImapServer"],
                    Int32.Parse(ConfigurationManager.AppSettings["ImapPort"]), bool.Parse(ConfigurationManager.AppSettings["EnabledSsl"]));
                imap.Login(ConfigurationManager.AppSettings["ImapUsername"], ConfigurationManager.AppSettings["ImapPwd"], AuthMethod.Login);
                //IEnumerable<uint> uids = imap.Search(SearchCondition.Subject("易泰电影列表"));
                IEnumerable<uint> uids = imap.Search(SearchCondition.All());
                IEnumerable<MailMessage> msgs = imap.GetMessages(uids);

                SubjectComparer comparer = new SubjectComparer();

                IOrderedEnumerable<MailMessage> orderedmsgs = msgs.Where(m => m.Subject.Contains("易泰")).OrderByDescending(m => m.Subject,comparer);
                if (orderedmsgs.Count() > 0)
                {
                    MailMessage msg = orderedmsgs.ElementAt(0);
                    Attachment attach = msg.Attachments[0];
                    Stream stream = attach.ContentStream;
                    // 把 Stream 转换成 byte[]   
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    // 设置当前流的位置为流的开始   
                    stream.Seek(0, SeekOrigin.Begin);
                    // 把 byte[] 写入文件   
                    FileStream fs = new FileStream(ConfigurationManager.AppSettings["Result"], FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(bytes);
                    bw.Close();
                    fs.Close();
                    Console.WriteLine("接收成功！{0}", msg.Subject);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("接收失败！{0}",ex.Message);
            }
            Console.ReadLine();
        }
    }
}
