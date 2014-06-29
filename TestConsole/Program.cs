using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.Redis.Client;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RedisPool pool = new RedisPool("127.0.0.1", 6379, "foobared", new RedisPool.RedisPoolConfig { });
                for (int i = 0; i < 30; i++)
                {
                    using (RedisClient redis = pool.GetRedisClient())
                    {
                        Console.WriteLine(DateTime.Now + " " + i);
                    }
                }

                using (RedisClient redis = new RedisClient("127.0.0.1", 6379, "foobared"))
                {
                    redis.Ping();
                    //Pipeline pipeline = redis.CreatePipeline();
                    //pipeline.Add(client => client.Set("key1", "1:" + DateTime.Now.ToString()));
                    //pipeline.Add(client => client.Set("key2", "2:" + DateTime.Now.ToString()));
                    //pipeline.Add(client => client.Set("key3", "3:" + DateTime.Now.ToString()));

                    //pipeline.Execute();
                    ////Console.WriteLine(redis.String.Get<string>("expedia.incr.rate"));
                    //List<string> list = redis.Scan(0, null, 10);
                    //foreach (var item in list)
                    //{
                    //    string type = redis.Type(item);

                    //    Console.WriteLine(item + " " + type + " " + redis.HLen(item));

                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + " " + ex.Message);
            }


            ////Console.WriteLine("start..");


            Console.WriteLine("over...");
            Console.Read();
        }
    }
}
