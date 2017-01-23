using MySql.Data.MySqlClient;
using NewBridge.Zoro.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.Zoro.MySQLClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //string connStr = "server=localhost;user=root;database=moviedatabase;port=3306;password=siyixin;";
            //MySqlConnection conn = new MySqlConnection(connStr);
            //try
            //{
            //    Console.WriteLine("Connecting to MySQL...");
            //    conn.Open();
            //    string sql = "SELECT * FROM movie";
            //    MySqlCommand cmd = new MySqlCommand(sql, conn);
            //    MySqlDataReader rdr = cmd.ExecuteReader();

            //    while (rdr.Read())
            //    {
            //        Console.WriteLine(rdr[0] + " -- " + rdr[1]);
            //    }
            //    rdr.Close();

            //    Console.WriteLine(DBHelper.GetDataSet(sql).Rows.Count);

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            //conn.Close();
            //Console.WriteLine("Done.");
            GetData();
            Console.ReadLine();
        }

        private static void GetData()
        {
            string sql = @"SELECT * FROM movie";
            DataTable table = DBHelper.GetDataSet(sql);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}",table.Rows[i]["category"],table.Rows[i]["title"], table.Rows[i]["rating"],table.Rows[i]["image"]);
            }
        }
    }
}
