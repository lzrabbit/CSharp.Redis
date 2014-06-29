using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSharp.Redis.Client
{
    /// <summary>
    /// Redis链接池
    /// 使用FIFO队列实现的简单链接池
    /// </summary>
    public class RedisPool
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public RedisPoolConfig PoolConfig { get; set; }

        private static readonly Object obj = new Object();

        private int ActiveClient;
        private int WaitMillis;
        Queue<RedisClient> IdleQueue = new Queue<RedisClient>();

        #region 构造函数

        public RedisPool(string host, int port)
            : this(host, port, null, null)
        {

        }

        public RedisPool(string host, int port, RedisPoolConfig poolConfig)
            : this(host, port, null, poolConfig)
        {

        }

        public RedisPool(string host, int port, string password)
            : this(host, port, password, null)
        {
        }

        public RedisPool(string host, int port, string password, RedisPoolConfig poolConfig)
        {
            if (poolConfig == null) poolConfig = new RedisPoolConfig();
            this.Host = host;
            this.Port = port;
            this.Password = password;
            this.PoolConfig = poolConfig;
        }

        #endregion

        public RedisClient GetRedisClient()
        {
            lock (obj)
            {
                while (true)
                {
                    if (ActiveClient < PoolConfig.MaxActive)
                    {
                        RedisClient client;
                        if (IdleQueue.Count > 0)
                        {
                            client = IdleQueue.Dequeue();
                            if (PoolConfig.TestOnBorrow)
                            {
                                try
                                {
                                    client.Ping();
                                }
                                catch
                                {
                                    client = new RedisClient(this.Host, this.Port, this.Password, this);
                                }
                            }
                        }
                        else
                        {
                            client = new RedisClient(this.Host, this.Port, this.Password, this);
                        }
                        ActiveClient++;
                        WaitMillis = 0;
                        return client;
                    }
                    else if (WaitMillis < PoolConfig.MaxWaitMillis)
                    {
                        Thread.Sleep(PoolConfig.WaitingIntervalMillis);
                        WaitMillis += PoolConfig.WaitingIntervalMillis;
                    }
                    else
                    {
                        throw new RedisException(string.Format("活动客户端已达上限:{0},在等待{1}毫秒后仍无法获得任何可用客户端", PoolConfig.MaxActive, WaitMillis));
                    }
                }
            }
        }

        /// <summary>
        /// 归还RedisClient到IdleQueue链接池,若成功归还到IdleQueue返回true,否则返回false,可直接抛弃client实例
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        internal bool ReturnClient(RedisClient client)
        {
            lock (obj)
            {
                ActiveClient--;
                if (IdleQueue.Count < PoolConfig.MaxIdle)
                {
                    IdleQueue.Enqueue(client);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 链接池配置
        /// </summary>
        public class RedisPoolConfig
        {
            /// <summary>
            /// 获取RedisClient实例时,检测实例是否可用
            /// </summary>
            public bool TestOnBorrow { get; set; }

            /// <summary>
            /// 最大活动RedisClient实例数
            /// </summary>
            public int MaxActive { get; set; }

            /// <summary>
            /// 最大空闲连接数
            /// </summary>
            public int MaxIdle { get; set; }

            /// <summary>
            /// 获取RedisClient实例时,最大等待时间(毫秒)
            /// </summary>
            public int MaxWaitMillis { get; set; }

            /// <summary>
            /// 获取RedisClient实例时,检测时间间隔(毫秒)
            /// </summary>
            public int WaitingIntervalMillis { get; set; }
            
            /// <summary>
            /// 初始化连接池配置
            /// </summary>
            public RedisPoolConfig()
            {
                TestOnBorrow = true;
                MaxActive = 20;
                MaxIdle = 10;
                MaxWaitMillis = 10 * 1000;
                WaitingIntervalMillis = 100;
            }
        }
    }
}
