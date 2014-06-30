using System;

namespace CSharp.Redis.Client
{
    public class RedisException : Exception
    {
        public RedisException(string message)
            : base(message)
        {
        }
    }
}