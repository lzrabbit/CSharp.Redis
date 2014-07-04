using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp.Redis.Client;
using System.IO;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {

          
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

            Console.WriteLine("over...");
            Console.Read();
        }

        static void abc(TextWriter tw)
        {
            tw.Write(DateTime.Now + " qqq");
        }
    }
}
