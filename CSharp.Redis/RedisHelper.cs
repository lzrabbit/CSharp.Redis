using CSharp.Redis.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Redis
{
    public class RedisHelper
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string Password { get; set; }

        RedisPool Pool;
        const int MAXCOUNT = 3000;
        public RedisHelper(string host, int port, string password)
        {
            this.Host = host;
            this.Port = port;
            this.Password = password;
            Pool = new RedisPool(host, port, password);
            Pool.GetRedisClient().Dispose();
        }

        public List<RedisKeyEntry> QueryKeys(string pattern)
        {
            List<RedisKeyEntry> list = new List<RedisKeyEntry>();
            using (RedisClient redis = Pool.GetRedisClient())
            {
                List<string> keys;
                int dbsize = redis.DbSize();
                if (dbsize > MAXCOUNT && pattern == "*")
                {
                    try
                    {
                        keys = redis.Scan(0, null, MAXCOUNT);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("ERR unknown command"))
                        {
                            throw new RedisException("缓存服务器KEY过多,Redis服务器版本过低,不支持SCAN命令,请升级Redis到2.8版本以上或者使用key进行过滤查询或执行自定义命令");
                        }
                        else
                        {
                            throw new RedisException(ex.Message);
                        }
                    }

                }
                else
                {
                    keys = redis.Keys(pattern);
                }

                foreach (var key in keys)
                {
                    string type = redis.Type(key);
                    string encoding = redis.Object(key, EObjectCommand.ENCODING);
                    int len = -1;
                    switch (type)
                    {
                        case "list":
                            len = redis.LLen(key);
                            break;
                        case "set":
                            len = redis.SCard(key);
                            break;
                        case "zset":
                            len = redis.ZCard(key);
                            break;
                        case "hash":
                            len = redis.HLen(key);
                            break;
                    }
                    list.Add(new RedisKeyEntry
                    {
                        ItemCount = len,
                        Key = key,
                        Type = type,
                        Encoding = encoding,
                    });
                }
            }
            return list;
        }

        public string ExecuteCommand(string[] command)
        {
            string result;
            using (RedisClient client = Pool.GetRedisClient())
            {
                var list = client.ExecuteCommand<string>(command);
                switch (string.Join(" ", command))
                {
                    case "TIME":
                        result = long.Parse(list[0]).ToDateTime().ToString();
                        break;
                    case "LASTSAVE":
                        result = long.Parse(list[0]).ToDateTime().ToString();
                        break;
                    case "CONFIG GET *":
                        result = List2String(list, true);
                        break;
                    default:
                        result = List2String(list);
                        break;
                }
            }
            return result;
        }



        public static string List2String(List<String> list, bool isKeyValuePare = false)
        {
            if (list.Count == 1) return list[0].Replace("\n", "\r\n");
            StringBuilder sb = new StringBuilder();
            if (!isKeyValuePare)
            {
                int max = list.Count.ToString().Length;
                for (int i = 0; i < list.Count; i++)
                {
                    string padding = new string(' ', max - (i + 1).ToString().Length);
                    sb.AppendFormat("{0}) {1}\r\n", padding + (i + 1), list[i].Replace("\n", "\r\n"));
                }
            }
            else
            {
                int max = (list.Count / 2).ToString().Length;
                for (int i = 0; i < list.Count; i += 2)
                {
                    string padding = new string(' ', max - ((i + 1) / 2).ToString().Length);
                    sb.AppendFormat("{0}) {1}:{2}\r\n", padding + (i / 2), list[i].Replace("\n", "\r\n"), list[i + 1].Replace("\n", "\r\n"));
                }
            }
            return sb.ToString();
        }

        public class RedisKeyEntry
        {
            public string Key { get; set; }

            /// <summary>
            /// none (key不存在)
            /// string (字符串)
            /// list (列表)
            /// set (集合)
            /// zset (有序集)
            /// hash (哈希表) 
            /// </summary>
            public string Type { get; set; }

            public string Encoding { get; set; }

            public int ItemCount { get; set; }

            public string GetRedisCommand()
            {
                string command = null;
                switch (Type)
                {
                    case "none":
                        break;
                    case "string":
                        command = string.Format("{0} {1}", RedisCommand.Strings.GET, Key);
                        break;
                    case "list":
                        command = string.Format("{0} {1} 0 100", RedisCommand.Lists.LRANGE, Key);
                        break;
                    case "set":
                        if (ItemCount <= MAXCOUNT) command = string.Format("{0} {1}", RedisCommand.Sets.SMEMBERS, Key);
                        else command = string.Format("{0} {1} 0 COUNT 100", RedisCommand.Sets.SSCAN, Key);
                        break;
                    case "zset":
                        if (ItemCount <= MAXCOUNT) command = string.Format("{0} {1} 0 10 WITHSCORES", RedisCommand.SortedSets.ZRANGE, Key);
                        else command = string.Format("{0} {1} 0 COUNT 100", RedisCommand.SortedSets.ZSCAN, Key);
                        break;
                    case "hash":
                        if (ItemCount <= MAXCOUNT) command = string.Format("{0} {1}", RedisCommand.Hashes.HGETALL, Key);
                        else command = string.Format("{0} {1} 0 COUNT 100", RedisCommand.Hashes.HSCAN, Key);
                        break;
                }
                return command;
            }

            public override string ToString()
            {
                return string.Format("{0} {1} {2}", Key, Type, ItemCount);
            }
        }
    }

}
