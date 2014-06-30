using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Redis.Client
{
    public static class CSharpExtensions
    {
        public static byte[] ToUtf8Bytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static long ToUnixTime(this DateTime datetime)
        {
            TimeSpan span = (datetime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return (long)span.TotalSeconds;
        }

        public static long ToUnixTimeMs(this DateTime datetime)
        {
            TimeSpan span = (datetime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return (long)span.TotalMilliseconds;
        }

        public static DateTime ToDateTime(this long unixTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime().AddSeconds(unixTime);
        }
    }
}
