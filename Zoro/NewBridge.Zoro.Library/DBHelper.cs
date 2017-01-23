using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.Zoro.Library
{
    public class DBHelper
    {
        //引导数据库连接数据库调用Web.Config文件    
        private static MySqlConnection connection;
        //创建连接
        public static MySqlConnection Connection
        {
            get
            {
                //MySqlConnection myConn = new MySqlConnection("server=localhost;user id=root;password=siyixin;database=moviedatabase;CharSet=gb2312;");
                MySqlConnection myConn = new MySqlConnection("server=localhost;user id=root;password=siyixin;database=doubandatabase;CharSet=utf8;");
                string connectionString = myConn.ConnectionString;
                if (connection == null)
                {
                    connection = new MySqlConnection(connectionString);
                    //打开连接
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }
        //（无参）返回执行的行数(删除修改更新)
        public static int ExecuteCommand(string safeSql)
        {
            MySqlCommand cmd = new MySqlCommand(safeSql, Connection);
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        //（有参）
        public static int ExecuteCommand(string sql, params MySqlParameter[] values)
        {
            MySqlCommand cmd = new MySqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            return cmd.ExecuteNonQuery();
        }
        //（无参）返回第一行第一列(删除修改更新)
        public static int GetScalar(string safeSql)
        {
            MySqlCommand cmd = new MySqlCommand(safeSql, Connection);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }
        //（有参）
        public static int GetScalar(string sql, params MySqlParameter[] values)
        {
            MySqlCommand cmd = new MySqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            return result;
        }
        //返回一个DataReader（查询）
        public static MySqlDataReader GetReader(string safeSql)
        {
            MySqlCommand cmd = new MySqlCommand(safeSql, Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
        public static MySqlDataReader GetReader(string sql, params MySqlParameter[] values)
        {
            MySqlCommand cmd = new MySqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        //返回一个DataTable
        public static DataTable GetDataSet(string safeSql)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(safeSql, Connection);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        public static DataTable GetDataSet(string sql, params MySqlParameter[] values)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }

        public static int InsertMovie(Task t, Subject s,string summary)
        {
            string sql = string.Format("INSERT INTO movie VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')",
                s.id, s.title.Replace("'","''"), s.original_title.Replace("'", "''"), s.rating.average, s.year==""?"0":s.year, s.subtype, parse(s.genres).Replace("'", "''"), parse(s.casts).Replace("'", "''"), parse(s.directors).Replace("'", "''"), s.alt, DoubanHelper._get_filename(s.images.large), t.Category, t.Name.Replace("'", "''"),summary);
            return DBHelper.GetScalar(sql);
        }

        public static int UpdateMovie(Subject s, string summary)
        {
            string sql = string.Format("UPDATE movie SET rating = '{0}',summary = '{1}' WHERE id = '{2}'",s.rating.average,summary.Replace("'", "''"), s.id);
            return DBHelper.GetScalar(sql);
        }

        public static int IsExsited(Subject s)
        {
            string sql = string.Format("SELECT * FROM movie WHERE id = '{0}'",s.id);
            return DBHelper.GetDataSet(sql).Rows.Count;
        }

        private static string parse(string[] str)
        {
            string r = string.Empty;
            foreach (string s in str)
            {
                r += s;
                r += ", ";
            }
            return r.TrimEnd(new char[] { ',',' ' });
        }

        private static string parse(Cast[] str)
        {
            string r = string.Empty;
            foreach (Cast s in str)
            {
                r += s.name;
                r += ", ";
            }
            return r.TrimEnd(new char[] { ',', ' ' });
        }

        private static string parse(Director[] str)
        {
            string r = string.Empty;
            foreach (Director s in str)
            {
                r += s.name;
                r += ", ";
            }
            return r.TrimEnd(new char[] { ',', ' ' });
        }

    }
}
