using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp.Redis
{
    public class DbConnection
    {
        static readonly string FILE = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "lzrabbit", "redis", "conf.ini");

        public string Name { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\r\n", this.Name, this.Host, this.Port, this.Password);
        }

        public static List<DbConnection> Load()
        {
            List<DbConnection> list = new List<DbConnection>();
            if (File.Exists(FILE))
            {
                var conns = File.ReadAllLines(FILE);
                foreach (var conn in conns)
                {
                    if (!string.IsNullOrEmpty(conn))
                    {
                        var config = conn.Split('\t');
                        list.Add(new DbConnection
                        {
                            Name = config[0],
                            Host = config[1],
                            Port = int.Parse(config[2]),
                            Password = config[3],
                        });
                    }
                }
            }
            return list;
        }

        public void Save()
        {
            LocalApplicationData.Save(FILE, this.ToString(), true);
        }

        public static void Save(List<DbConnection> conns)
        {
            StringBuilder sb = new StringBuilder();
            conns.ForEach(conn =>
            {
                sb.AppendFormat(conn.ToString() + "\r\n");
            });
            LocalApplicationData.Save(FILE, sb.ToString(), false);
        }
    }
}
