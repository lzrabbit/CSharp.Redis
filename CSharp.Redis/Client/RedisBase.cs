using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.Runtime.Serialization.Json;

namespace CSharp.Redis.Client
{
    public abstract class RedisBase : IDisposable
    {
        private RedisPool Pool;

        public Pipeline Pipeline { get; set; }

        Socket SocketClient;

        protected RedisBase(string host, int port, string password, RedisPool pool)
        {
            this.SocketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SocketClient.Connect(host, port);
            if (!string.IsNullOrEmpty(password))
            {
                ExecuteCommand<string>(RedisCommand.Connection.AUTH, password);
            }
            ExecuteCommand<string>(RedisCommand.Connection.PING);
            this.Pool = pool;
        }

        //In a Status Reply the first byte of the reply is "+"
        //In an Error Reply the first byte of the reply is "-"
        //In an Integer Reply the first byte of the reply is ":"
        //In a Bulk Reply the first byte of the reply is "$"
        //In a Multi Bulk Reply the first byte of the reply s "*"
        //用单行回复，回复的第一个字节将是“+”
        //错误消息，回复的第一个字节将是“-”
        //整型数字，回复的第一个字节将是“:”
        //批量回复，回复的第一个字节将是“$”
        //多个批量回复，回复的第一个字节将是“*”

        /*
            *<number of arguments> CR LF
            $<number of bytes of argument 1> CR LF
            <argument data> CR LF
            ...
            $<number of bytes of argument N> CR LF
            <argument data> CR LF
        */

        private byte[] BuildCommand(object[] args)
        {
            byte[] nl = new byte[2] { 13, 10 };
            List<byte> buffer = new List<byte>();
            buffer.AddRange(("*" + args.Count(item => item != null)).ToUtf8Bytes());
            buffer.AddRange(nl);
            foreach (var arg in args)
            {
                if (arg != null)
                {
                    byte[] bytes;
                    if (arg is string) bytes = ((string)arg).ToUtf8Bytes();
                    else if (arg is byte[]) bytes = (byte[])arg;
                    else bytes = JsonSerializer(arg).ToUtf8Bytes();

                    buffer.AddRange(("$" + bytes.Length).ToUtf8Bytes());
                    buffer.AddRange(nl);
                    buffer.AddRange(bytes);
                    buffer.AddRange(nl);
                }
            }
            return buffer.ToArray();
        }

        public List<T> ExecuteCommand<T>(params object[] args)
        {
            if (this.Pipeline != null)
            {
                QueuePipelineCommand<T>(args, false);
                return null;
            }
            return ParseResult<T>(ExecuteCommand(args));
        }

        public BufferedStream ExecuteCommand(params object[] args)
        {
            if (!this.SocketClient.Connected)
            {
                throw new RedisException("Socket连接已被关闭");
            }

            byte[] buffer = BuildCommand(args);
            BufferedStream BStream = new BufferedStream(new NetworkStream(SocketClient), 16 * 1024);
            SocketClient.Send(buffer);
            return BStream;
        }

        internal List<T> ParseResult<T>(BufferedStream BStream)
        {
            //用单行回复，回复的第一个字节将是“+”
            //错误消息，回复的第一个字节将是“-”
            //整型数字，回复的第一个字节将是“:”
            //批量回复，回复的第一个字节将是“$”
            //多个批量回复，回复的第一个字节将是“*”
            List<T> result = new List<T>();
            int lines = 1;
            while (lines-- > 0)
            {
                string line = ReadLine(BStream);
                if (line.Length > 0)
                {
                    switch (line[0])
                    {
                        case '+':
                        case ':':
                            result.Add(JsonDeserialize<T>(line.Substring(1)));
                            break;
                        case '-':
                            throw new RedisException(line.Substring(1));
                        case '$':
                            int len = Convert.ToInt32(line.Substring(1));
                            result.Add(JsonDeserialize<T>(ReadLine(BStream, len)));
                            break;
                        case '*':
                            lines = int.Parse(line.Substring(1));
                            break;
                        default:
                            throw new RedisException("UnKnowResponse " + line);
                    }
                }
            }
            return result;
        }

        private string ReadLine(BufferedStream BStream)
        {
            StringBuilder sb = new StringBuilder();
            // List<byte> bytes = new List<byte>();
            if (!SocketClient.Connected)
            {
                Console.WriteLine("xxx");
            }
            int c;
            while ((c = BStream.ReadByte()) != -1)
            {
                if (c == '\r')
                    continue;
                if (c == '\n')
                    break;
                //bytes.Add((byte)c);
                sb.Append((char)c);
            }
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        private string ReadLine(BufferedStream BStream, int count)
        {
            if (count == -1) return null;
            byte[] retbuf = new byte[count];
            int offset = 0;
            while (count > 0)
            {
                int readCount = BStream.Read(retbuf, offset, count);
                offset += readCount;
                count -= readCount;
            }
            if (BStream.ReadByte() != '\r' || BStream.ReadByte() != '\n')
                throw new Exception("非法终止符");

            return Encoding.UTF8.GetString(retbuf);
        }

        public void Dispose()
        {
            if (Pool == null || !Pool.ReturnClient((RedisClient)this))
            {
                if (SocketClient.Connected)
                {
                    ExecuteCommand<string>(RedisCommand.Connection.QUIT);
                    SocketClient.Dispose();
                }
            }
        }

        protected bool IsSocketClosed()
        {
            return SocketClient == null || !SocketClient.Connected;
        }

        private void QueuePipelineCommand<T>(object[] args, bool isSingle)
        {
            this.Pipeline.PipelineCommands.Add(new PipelineCommand
            {
                Args = args,
                CallBack = this.Pipeline.CurrentPipelineCommandCallBack,
                ArgsBuffer = BuildCommand(args),
                IsSingle = isSingle,
                ReturnType = typeof(T),
            });
            this.Pipeline.CurrentPipelineCommandCallBack = null;
        }

        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, t);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public static T JsonDeserialize<T>(string str)
        {
            if (typeof(T) == typeof(string)) return (T)Convert.ChangeType(str, typeof(T));
            else if (string.IsNullOrEmpty(str)) return default(T);
            else
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
                {
                    T obj = (T)serializer.ReadObject(ms);
                    return obj;
                }
            }
        }

        public static object[] ArgsToArray(params object[] parms)
        {
            List<object> args = new List<object>();
            foreach (var param in parms)
            {
                if (param is ICollection)
                {
                    if (param is IDictionary)
                    {
                        foreach (DictionaryEntry kv in (IDictionary)param)
                        {
                            args.Add(kv.Key);
                            args.Add(kv.Value);
                        }
                    }
                    else
                    {
                        foreach (var arg in (ICollection)param)
                        {
                            args.Add(arg);
                        }
                    }
                }
                else
                {
                    args.Add(param);
                }
            }

            return args.ToArray();
        }
    }
}
