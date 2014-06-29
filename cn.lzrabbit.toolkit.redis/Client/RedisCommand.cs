namespace CSharp.Redis.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisCommand
    {
        public class Keys
        {
            /// <summary>
            /// DEL key [key ...]
            /// </summary>
            public const string DEL = "DEL";

            /// <summary>
            /// DUMP key(Available since 2.6.0.)
            /// </summary>
            public const string DUMP = "DUMP";

            /// <summary>
            /// EXISTS key
            /// </summary>
            public const string EXISTS = "EXISTS";

            /// <summary>
            /// EXPIRE key seconds
            /// </summary>
            public const string EXPIRE = "EXPIRE";

            /// <summary>
            /// EXPIREAT key timestamp
            /// </summary>
            public const string EXPIREAT = "EXPIREAT";

            /// <summary>
            /// KEYS pattern
            /// </summary>
            public const string KEYS = "KEYS";

            /// <summary>
            /// MIGRATE host port key destination-db timeout [COPY] [REPLACE](Available since 2.6.0.)
            /// </summary>
            public const string MIGRATE = "MIGRATE";

            /// <summary>
            /// MOVE key db
            /// </summary>
            public const string MOVE = "MOVE";

            /// <summary>
            /// OBJECT subcommand [arguments [arguments ...]]
            /// </summary>
            public const string OBJECT = "OBJECT";

            /// <summary>
            /// PERSIST key
            /// </summary>
            public const string PERSIST = "PERSIST";

            /// <summary>
            /// PEXPIRE key milliseconds
            /// </summary>
            public const string PEXPIRE = "PEXPIRE";

            /// <summary>
            /// PEXPIREAT key milliseconds-timestamp
            /// </summary>
            public const string PEXPIREAT = "PEXPIREAT";

            /// <summary>
            /// PTTL key
            /// </summary>
            public const string PTTL = "PTTL";

            /// <summary>
            /// RANDOMKEY
            /// </summary>
            public const string RANDOMKEY = "RANDOMKEY";

            /// <summary>
            /// RENAME key newkey
            /// </summary>
            public const string RENAME = "RENAME";

            /// <summary>
            /// RENAMENX key newkey
            /// </summary>
            public const string RENAMENX = "RENAMENX";

            /// <summary>
            /// RESTORE key ttl serialized-value
            /// </summary>
            public const string RESTORE = "RESTORE";

            /// <summary>
            /// SORT key [BY pattern] [LIMIT offset count] [GET pattern [GET pattern ...]] [ASC|DESC] [ALPHA] [STORE destination]
            /// </summary>
            public const string SORT = "SORT";

            /// <summary>
            /// TTL key
            /// </summary>
            public const string TTL = "TTL";

            /// <summary>
            /// TYPE key
            /// none (key不存在)
            ///   string (字符串)
            ///   list (列表)
            ///   set (集合)
            ///   zset (有序集)
            ///   hash (哈希表)
            /// </summary>
            public const string TYPE = "TYPE";

            /// <summary>
            /// SCAN cursor [MATCH pattern] [COUNT count]
            /// </summary>
            public const string SCAN = "SCAN";
        }

        public class Strings
        {
            /// <summary>
            /// APPEND key value
            /// </summary>
            public const string APPEND = "APPEND";

            /// <summary>
            /// BITCOUNT key [start] [end]
            /// </summary>
            public const string BITCOUNT = "BITCOUNT";

            /// <summary>
            /// BITOP operation destkey key [key ...]
            /// </summary>
            public const string BITOP = "BITOP";

            /// <summary>
            /// DECR key
            /// </summary>
            public const string DECR = "DECR";

            /// <summary>
            /// DECRBY key decrement
            /// </summary>
            public const string DECRBY = "DECRBY";

            /// <summary>
            /// GET key
            /// </summary>
            public const string GET = "GET";

            /// <summary>
            /// GETBIT key offset
            /// </summary>
            public const string GETBIT = "GETBIT";

            /// <summary>
            /// GETRANGE key start end
            /// </summary>
            public const string GETRANGE = "GETRANGE";

            /// <summary>
            /// GETSET key value
            /// </summary>
            public const string GETSET = "GETSET";

            /// <summary>
            /// INCR key
            /// </summary>
            public const string INCR = "INCR";

            /// <summary>
            /// INCRBY key increment
            /// </summary>
            public const string INCRBY = "INCRBY";

            /// <summary>
            /// INCRBYFLOAT key increment
            /// </summary>
            public const string INCRBYFLOAT = "INCRBYFLOAT";

            /// <summary>
            /// MGET key [key ...]
            /// </summary>
            public const string MGET = "MGET";

            /// <summary>
            /// MSET key value [key value ...]
            /// </summary>
            public const string MSET = "MSET";

            /// <summary>
            /// MSETNX key value [key value ...]
            /// </summary>
            public const string MSETNX = "MSETNX";

            /// <summary>
            /// PSETEX key milliseconds value
            /// </summary>
            public const string PSETEX = "PSETEX";

            /// <summary>
            /// SET key value [EX seconds] [PX milliseconds] [NX|XX]
            /// </summary>
            public const string SET = "SET";

            /// <summary>
            /// SETBIT key offset value
            /// </summary>
            public const string SETBIT = "SETBIT";

            /// <summary>
            /// SETEX key seconds value
            /// </summary>
            public const string SETEX = "SETEX";

            /// <summary>
            /// SETNX key value
            /// </summary>
            public const string SETNX = "SETNX";

            /// <summary>
            /// SETRANGE key offset value
            /// </summary>
            public const string SETRANGE = "SETRANGE";

            /// <summary>
            /// STRLEN key
            /// </summary>
            public const string STRLEN = "STRLEN";

            
        }

        public class Hashes
        {
            /// <summary>
            /// HDEL key field [field ...]
            /// </summary>
            public const string HDEL = "HDEL";

            /// <summary>
            /// HEXISTS key field
            /// </summary>
            public const string HEXISTS = "HEXISTS";

            /// <summary>
            /// HGET key field
            /// </summary>
            public const string HGET = "HGET";

            /// <summary>
            /// HGETALL key
            /// </summary>
            public const string HGETALL = "HGETALL";

            /// <summary>
            /// HINCRBY key field increment
            /// </summary>
            public const string HINCRBY = "HINCRBY";

            /// <summary>
            /// HINCRBYFLOAT key field increment(Available since 2.6.0)
            /// </summary>
            public const string HINCRBYFLOAT = "HINCRBYFLOAT";

            /// <summary>
            /// HKEYS key
            /// </summary>
            public const string HKEYS = "HKEYS";

            /// <summary>
            /// HLEN key
            /// </summary>
            public const string HLEN = "HLEN";

            /// <summary>
            /// HMGET key field [field ...]
            /// </summary>
            public const string HMGET = "HMGET";

            /// <summary>
            /// HMSET key field value [field value ...]
            /// </summary>
            public const string HMSET = "HMSET";

            /// <summary>
            /// HSET key field value
            /// </summary>
            public const string HSET = "HSET";

            /// <summary>
            /// HSETNX key field value
            /// </summary>
            public const string HSETNX = "HSETNX";

            /// <summary>
            /// HVALS key
            /// </summary>
            public const string HVALS = "HVALS";

            /// <summary>
            /// HSCAN key cursor [MATCH pattern] [COUNT count]
            /// </summary>
            public const string HSCAN = "HSCAN";
        }

        public class Lists
        {
            /// <summary>
            /// BLPOP key [key ...] timeout
            /// </summary>
            public const string BLPOP = "BLPOP";

            /// <summary>
            /// BRPOP key [key ...] timeout
            /// </summary>
            public const string BRPOP = "BRPOP";

            /// <summary>
            /// BRPOPLPUSH source destination timeout
            /// </summary>
            public const string BRPOPLPUSH = "BRPOPLPUSH";

            /// <summary>
            /// LINDEX key index
            /// </summary>
            public const string LINDEX = "LINDEX";

            /// <summary>
            /// LINSERT key BEFORE|AFTER pivot value
            /// </summary>
            public const string LINSERT = "LINSERT";

            /// <summary>
            /// LLEN key
            /// </summary>
            public const string LLEN = "LLEN";

            /// <summary>
            /// LPOP key
            /// </summary>
            public const string LPOP = "LPOP";

            /// <summary>
            /// LPUSH key value [value ...]
            /// </summary>
            public const string LPUSH = "LPUSH";

            /// <summary>
            /// LPUSHX key value
            /// </summary>
            public const string LPUSHX = "LPUSHX";

            /// <summary>
            /// LRANGE key start stop
            /// </summary>
            public const string LRANGE = "LRANGE";

            /// <summary>
            /// LREM key count value
            /// </summary>
            public const string LREM = "LREM";

            /// <summary>
            /// LSET key index value
            /// </summary>
            public const string LSET = "LSET";

            /// <summary>
            /// LTRIM key start stop
            /// </summary>
            public const string LTRIM = "LTRIM";

            /// <summary>
            /// RPOP key
            /// </summary>
            public const string RPOP = "RPOP";

            /// <summary>
            /// RPOPLPUSH source destination
            /// </summary>
            public const string RPOPLPUSH = "RPOPLPUSH";

            /// <summary>
            /// RPUSH key value [value ...]
            /// </summary>
            public const string RPUSH = "RPUSH";

            /// <summary>
            /// RPUSHX key value
            /// </summary>
            public const string RPUSHX = "RPUSHX";
        }

        public class Sets
        {
            /// <summary>
            /// SADD key member [member ...]
            /// </summary>
            public const string SADD = "SADD";

            /// <summary>
            /// SCARD key
            /// </summary>
            public const string SCARD = "SCARD";

            /// <summary>
            /// SDIFF key [key ...]
            /// </summary>
            public const string SDIFF = "SDIFF";

            /// <summary>
            /// SDIFFSTORE destination key [key ...]
            /// </summary>
            public const string SDIFFSTORE = "SDIFFSTORE";

            /// <summary>
            /// SINTER key [key ...]
            /// </summary>
            public const string SINTER = "SINTER";

            /// <summary>
            /// SINTERSTORE destination key [key ...]
            /// </summary>
            public const string SINTERSTORE = "SINTERSTORE";

            /// <summary>
            /// SISMEMBER key member
            /// </summary>
            public const string SISMEMBER = "SISMEMBER";

            /// <summary>
            /// SMEMBERS key
            /// </summary>
            public const string SMEMBERS = "SMEMBERS";

            /// <summary>
            /// SMOVE source destination member
            /// </summary>
            public const string SMOVE = "SMOVE";

            /// <summary>
            /// SPOP key
            /// </summary>
            public const string SPOP = "SPOP";

            /// <summary>
            /// SRANDMEMBER key [count]
            /// </summary>
            public const string SRANDMEMBER = "SRANDMEMBER";

            /// <summary>
            /// SREM key member [member ...]
            /// </summary>
            public const string SREM = "SREM";

            /// <summary>
            /// SUNION key [key ...]
            /// </summary>
            public const string SUNION = "SUNION";

            /// <summary>
            /// SUNIONSTORE destination key [key ...]
            /// </summary>
            public const string SUNIONSTORE = "SUNIONSTORE";

            /// <summary>
            /// SCAN key cursor [MATCH pattern] [COUNT count]
            /// </summary>
            public const string SSCAN = "SSCAN";
        }

        public class SortedSets
        {
            /// <summary>
            /// ZADD key score member [score member ...]
            /// </summary>
            public const string ZADD = "ZADD";

            /// <summary>
            /// ZCARD key
            /// </summary>
            public const string ZCARD = "ZCARD";

            /// <summary>
            /// ZCOUNT key min max
            /// </summary>
            public const string ZCOUNT = "ZCOUNT";

            /// <summary>
            /// ZINCRBY key increment member
            /// </summary>
            public const string ZINCRBY = "ZINCRBY";

            /// <summary>
            /// ZINTERSTORE destination numkeys key [key ...] [WEIGHTS weight [weight ...]] [AGGREGATE SUM|MIN|MAX]
            /// </summary>
            public const string ZINTERSTORE = "ZINTERSTORE";

            /// <summary>
            /// ZRANGE key start stop [WITHSCORES]
            /// </summary>
            public const string ZRANGE = "ZRANGE";

            /// <summary>
            /// ZRANGEBYSCORE key min max [WITHSCORES] [LIMIT offset count]
            /// </summary>
            public const string ZRANGEBYSCORE = "ZRANGEBYSCORE";

            /// <summary>
            /// ZRANK key member
            /// </summary>
            public const string ZRANK = "ZRANK";

            /// <summary>
            /// ZREM key member [member ...]
            /// </summary>
            public const string ZREM = "ZREM";

            /// <summary>
            /// ZREMRANGEBYRANK key start stop
            /// </summary>
            public const string ZREMRANGEBYRANK = "ZREMRANGEBYRANK";

            /// <summary>
            /// ZREMRANGEBYSCORE key min max
            /// </summary>
            public const string ZREMRANGEBYSCORE = "ZREMRANGEBYSCORE";

            /// <summary>
            /// ZREVRANGE key start stop [WITHSCORES]
            /// </summary>
            public const string ZREVRANGE = "ZREVRANGE";

            /// <summary>
            /// ZREVRANGEBYSCORE key max min [WITHSCORES] [LIMIT offset count]
            /// </summary>
            public const string ZREVRANGEBYSCORE = "ZREVRANGEBYSCORE";

            /// <summary>
            /// ZREVRANK key member
            /// </summary>
            public const string ZREVRANK = "ZREVRANK";

            /// <summary>
            /// ZSCORE key member
            /// </summary>
            public const string ZSCORE = "ZSCORE";

            /// <summary>
            /// ZUNIONSTORE destination numkeys key [key ...] [WEIGHTS weight [weight ...]] [AGGREGATE SUM|MIN|MAX]
            /// </summary>
            public const string ZUNIONSTORE = "ZUNIONSTORE";

            /// <summary>
            /// ZSCAN key cursor [MATCH pattern] [COUNT count]
            /// </summary>
            public const string ZSCAN = "ZSCAN";
        }

        public class PubSub
        {
            /// <summary>
            /// PSUBSCRIBE pattern [pattern ...]
            /// </summary>
            public const string PSUBSCRIBE = "PSUBSCRIBE";

            /// <summary>
            /// PUBLISH channel message
            /// </summary>
            public const string PUBLISH = "PUBLISH";

            /// <summary>
            /// PUBSUB subcommand [argument [argument ...]]
            /// </summary>
            public const string PUBSUB = "PUBSUB";

            /// <summary>
            /// PUNSUBSCRIBE [pattern [pattern ...]]
            /// </summary>
            public const string PUNSUBSCRIBE = "PUNSUBSCRIBE";

            /// <summary>
            /// SUBSCRIBE channel [channel ...]
            /// </summary>
            public const string SUBSCRIBE = "SUBSCRIBE";

            /// <summary>
            /// UNSUBSCRIBE [channel [channel ...]]
            /// </summary>
            public const string UNSUBSCRIBE = "UNSUBSCRIBE";
        }

        public class Transactions
        {
            /// <summary>
            /// DISCARD
            /// </summary>
            public const string DISCARD = "DISCARD";

            /// <summary>
            /// EXEC
            /// </summary>
            public const string EXEC = "EXEC";

            /// <summary>
            /// MULTI
            /// </summary>
            public const string MULTI = "MULTI";

            /// <summary>
            /// UNWATCH
            /// </summary>
            public const string UNWATCH = "UNWATCH";

            /// <summary>
            /// WATCH key [key ...]
            /// </summary>
            public const string WATCH = "WATCH";
        }

        /// <summary>
        /// Available since 2.6.0.
        /// </summary>
        public class Scripting
        {
            /// <summary>
            /// EVAL script numkeys key [key ...] arg [arg ...]
            /// </summary>
            public const string EVAL = "EVAL";

            /// <summary>
            /// EVALSHA sha1 numkeys key [key ...] arg [arg ...]
            /// </summary>
            public const string EVALSHA = "EVALSHA";

            /// <summary>
            /// SCRIPT EXISTS script [script ...]
            /// </summary>
            public const string SCRIPTEXISTS = "SCRIPT EXISTS";

            /// <summary>
            /// SCRIPT FLUSH
            /// </summary>
            public const string SCRIPTFLUSH = "SCRIPT FLUSH";

            /// <summary>
            /// SCRIPT KILL
            /// </summary>
            public const string SCRIPTKILL = "SCRIPT KILL";

            /// <summary>
            /// SCRIPT LOAD script
            /// </summary>
            public const string SCRIPTLOAD = "SCRIPT LOAD";
        }

        public class Connection
        {
            /// <summary>
            /// AUTH password
            /// </summary>
            public const string AUTH = "AUTH";

            /// <summary>
            /// ECHO message
            /// </summary>
            public const string ECHO = "ECHO";

            /// <summary>
            /// PING
            /// </summary>
            public const string PING = "PING";

            /// <summary>
            /// QUIT
            /// </summary>
            public const string QUIT = "QUIT";

            /// <summary>
            /// SELECT index
            /// </summary>
            public const string SELECT = "SELECT";
        }

        public class Server
        {
            /// <summary>
            /// BGREWRITEAOF
            /// </summary>
            public const string BGREWRITEAOF = "BGREWRITEAOF";

            /// <summary>
            /// BGSAVE
            /// </summary>
            public const string BGSAVE = "BGSAVE";

            /// <summary>
            /// CLIENT GETNAME
            /// </summary>
            public const string CLIENTGETNAME = "CLIENT GETNAME";

            /// <summary>
            /// CLIENT KILL ip:port
            /// </summary>
            public const string CLIENTKILL = "CLIENT KILL";

            /// <summary>
            /// CLIENT LIST
            /// </summary>
            public const string CLIENTLIST = "CLIENT LIST";

            /// <summary>
            /// CLIENT SETNAME connection-name
            /// </summary>
            public const string CLIENTSETNAME = "CLIENT SETNAME";

            /// <summary>
            /// CONFIG GET parameter
            /// </summary>
            public const string CONFIGGET = "CONFIG GET";

            /// <summary>
            /// CONFIG RESETSTAT
            /// </summary>
            public const string CONFIGRESETSTAT = "CONFIG RESETSTAT";

            /// <summary>
            /// CONFIG REWRITE(Available since 2.8.0.)
            /// </summary>
            public const string CONFIGREWRITE = "CONFIG REWRITE";

            /// <summary>
            /// CONFIG SET parameter value
            /// </summary>
            public const string CONFIGSET = "CONFIG SET";

            /// <summary>
            /// DBSIZE
            /// </summary>
            public const string DBSIZE = "DBSIZE";

            /// <summary>
            /// DEBUG OBJECT key
            /// </summary>
            public const string DEBUGOBJECT = "DEBUG OBJECT";

            /// <summary>
            /// DEBUG SEGFAULT
            /// </summary>
            public const string DEBUGSEGFAULT = "DEBUG SEGFAULT";

            /// <summary>
            /// FLUSHALL
            /// </summary>
            public const string FLUSHALL = "FLUSHALL";

            /// <summary>
            /// FLUSHDB
            /// </summary>
            public const string FLUSHDB = "FLUSHDB";

            /// <summary>
            /// INFO [section]
            /// </summary>
            public const string INFO = "INFO";

            /// <summary>
            /// LASTSAVE
            /// </summary>
            public const string LASTSAVE = "LASTSAVE";

            /// <summary>
            /// MONITOR
            /// </summary>
            public const string MONITOR = "MONITOR";

            /// <summary>
            /// SAVE
            /// </summary>
            public const string SAVE = "SAVE";

            /// <summary>
            /// SHUTDOWN [NOSAVE] [SAVE]
            /// </summary>
            public const string SHUTDOWN = "SHUTDOWN";

            /// <summary>
            /// SLAVEOF host port
            /// </summary>
            public const string SLAVEOF = "SLAVEOF";

            /// <summary>
            /// SLOWLOG subcommand [argument]
            /// </summary>
            public const string SLOWLOG = "SLOWLOG";

            /// <summary>
            /// SYNC
            /// </summary>
            public const string SYNC = "SYNC";

            /// <summary>
            /// TIME(Available since 2.6.0.)
            /// </summary>
            public const string TIME = "TIME";
        }
    }
}