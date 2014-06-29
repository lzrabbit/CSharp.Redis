using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;

namespace CSharp.Redis.Client
{
    public class Pipeline 
    {
        public RedisClient Redis { get; set; }
        public List<PipelineCommand> PipelineCommands = new List<PipelineCommand>();
        public Action<object> CurrentPipelineCommandCallBack { get; set; }

        public Pipeline(RedisClient redis)
        {
            this.Redis = redis;
            this.Redis.Pipeline = this;
        }

        public void Add(Action<RedisClient> command)
        {
            command(this.Redis);

        }

        public void Add(Func<RedisClient, object> command, Action<object> callback)
        {
            this.CurrentPipelineCommandCallBack = callback;

            command(this.Redis);
        }
        

        public void Execute()
        {
            //foreach (var item in this.RedisClient.ArgsToArray(this.PipelineCommands.Select(cmd => cmd.Args).ToArray()))
            //{
            //    Console.WriteLine(item);
            //}
            List<byte> bytes = new List<byte>();
            foreach (var command in this.PipelineCommands)
            {
                bytes.AddRange(command.ArgsBuffer);
            }
            System.IO.BufferedStream BStream = null; //this.RedisClient.SendCommand(bytes.ToArray());

            foreach (var command in this.PipelineCommands)
            {
                object result;
                string methodName;
                if (command.IsSingle)
                {
                    methodName = "HandSingle";
                    //result = RedisClient.HandSingle<string>(BStream);
                }
                else
                {
                    //result = RedisClient.HandMulti<string>(BStream);
                    methodName = "HandMulti";
                }
               
                try
                {
                    var method = this.Redis.GetType().GetMethod(methodName);
                    method = method.MakeGenericMethod(command.ReturnType);
                    result = method.Invoke(this.Redis, new object[] { BStream });
                    
                    if (command.CallBack != null) command.CallBack(result);
                }
                catch (RedisException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    
        public void Dispose()
        {
            //this.RedisClient.Pipeline = null;
        }

    }

}
