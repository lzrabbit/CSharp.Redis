using CSharp.Redis.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Redis
{
    public class RedisHelper
    {
        RedisPool Pool;
        const int MAXCOUNT = 3000;
        public RedisHelper(string host, int port, string password)
        {
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
                    });
                }
            }
            return list;
        }

        public string ExecuteCommand(string[] command)
        {
            using (RedisClient client = Pool.GetRedisClient())
            {
                return List2String(client.ExecuteCommand<string>(command));
            }
        }


        public static string List2String(List<String> list)
        {
            int max = (int)Math.Floor(Math.Log10(list.Count));
            if (list.Count == 1) return list[0];
            string str = null;
            for (int i = 1; i <= list.Count; i++)
            {
                string padding = new string(' ', max - (int)Math.Floor(Math.Log10(i)));
                str += string.Format("{0}) {1}\r\n", padding + i, list[i - 1]);
            }
            return str;
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
