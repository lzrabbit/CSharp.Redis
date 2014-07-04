using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
namespace CSharp.Redis.Client
{
    public class RedisClient : RedisBase
    {
        public RedisClient(string host, int port)
            : base(host, port, null, null)
        {
        }

        public RedisClient(string host, int port, string password)
            : base(host, port, password, null)
        {
        }
        internal RedisClient(string host, int port, string password, RedisPool pool)
            : base(host, port, password, pool)
        {
        }


        #region Key

        public int Del(params string[] keys)
        {
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Keys.DEL, keys)).First();
        }


        public string Randomkey()
        {
            return ExecuteCommand<string>(RedisCommand.Keys.RANDOMKEY).First();
        }


        public void Rename(string key, string newkey)
        {
            ExecuteCommand<string>(RedisCommand.Keys.RENAME, key, newkey);
        }


        public bool RenameNx(string key, string newkey)
        {
            return ExecuteCommand<int>(RedisCommand.Keys.RENAMENX, key, newkey).First() == 1;
        }


        public bool Exists(string key)
        {
            return ExecuteCommand<int>(RedisCommand.Keys.EXISTS, key).First() == 1;
        }


        public List<string> Keys(string pattern)
        {
            //KEYS * 匹配数据库中所有 key 。

            //KEYS h?llo 匹配 hello ， hallo 和 hxllo 等。

            //KEYS h*llo 匹配 hllo 和 heeeeello 等。

            //KEYS h[ae]llo 匹配 hello 和 hallo ，但不匹配 hillo 。
            return ExecuteCommand<string>(RedisCommand.Keys.KEYS, pattern);
        }


        public bool Expire(string key, TimeSpan expireIn)
        {
            return ExecuteCommand<int>(RedisCommand.Keys.EXPIRE, key, (long)expireIn.TotalSeconds).First() == 1;
        }


        public bool ExpireAt(string key, DateTime expireAt)
        {
            return Expire(key, expireAt - DateTime.Now);
        }


        public bool PExpire(string key, TimeSpan expireIn)
        {
            return ExecuteCommand<int>(RedisCommand.Keys.PEXPIRE, key, (long)expireIn.TotalMilliseconds).First() == 1;
        }


        public bool PExpireAt(string key, DateTime expireAt)
        {
            //SendCommand<int>(RedisCommand.Keys.PEXPIREAT, key, expireAt.ToUnixTimeMs());
            return PExpire(key, expireAt - DateTime.Now);
        }


        public bool Persist(string key)
        {
            return ExecuteCommand<int>(RedisCommand.Keys.PERSIST, key).First() == 1;
        }


        public long Ttl(string key)
        {
            return ExecuteCommand<long>(RedisCommand.Keys.TTL, key).First();
        }


        public long PTtl(string key)
        {
            return ExecuteCommand<long>(RedisCommand.Keys.PTTL, key).First();
        }


        public bool Move(string key, int db)
        {
            return ExecuteCommand<int>(RedisCommand.Keys.MOVE, key, db).First() == 1;
        }


        public string Object(string key, EObjectCommand command)
        {
            return ExecuteCommand<string>(RedisCommand.Keys.OBJECT, command.ToString(), key).First();
        }


        public string Type(string key)
        {
            return ExecuteCommand<string>(RedisCommand.Keys.TYPE, key).First();
        }

        public List<string> Scan(int cursor, string pattern, int count)
        {
            return ExecuteCommand<string>(RedisCommand.Keys.SCAN.ToString(), cursor, string.IsNullOrEmpty(pattern) ? null : "MATCH", pattern, "COUNT", count);
        }

        #endregion

        #region String

        /// <summary>
        /// 将字符串值 value 关联到 key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, object value)
        {
            ExecuteCommand<string>(RedisCommand.Strings.SET, key, value);
        }

        /// <summary>
        /// 将字符串值 value 关联到 key，并设置过期时间(秒)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireIn"></param>
        public void Set(string key, object value, TimeSpan expireIn)
        {
            Set(key, value, ESetOption.EX, expireIn);
        }

        /// <summary>
        /// 将字符串值 value 关联到 key，并设置过期时间(秒)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireAt"></param>
        public void Set(string key, object value, DateTime expireAt)
        {
            Set(key, value, ESetOption.EX, (expireAt - DateTime.Now));
        }

        /// <summary>
        /// 将字符串值 value 关联到 key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="option"></param>
        /// <param name="expireIn"></param>
        public void Set(string key, object value, ESetOption option, TimeSpan? expireIn = null)
        {
            if (option == ESetOption.NX || option == ESetOption.XX) ExecuteCommand<string>(RedisCommand.Strings.SET, key, value, option.ToString());
            else
            {
                if (!expireIn.HasValue) throw new RedisException("redis command SET with option EX or PX must set expire seconds");
                ExecuteCommand<string>(RedisCommand.Strings.SET, key, value, option.ToString(), option == ESetOption.EX ? (long)expireIn.Value.TotalSeconds : (long)expireIn.Value.TotalMilliseconds);
            }
        }

        /// <summary>
        /// 返回 key 所关联的字符串值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return ExecuteCommand<T>(RedisCommand.Strings.GET, key).First();
        }

        /// <summary>
        /// 将给定 key 的值设为 value ，并返回 key 的旧值(old value)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public T GetSet<T>(string key, T value)
        {
            return ExecuteCommand<T>(RedisCommand.Strings.GETSET, key, value).First();
        }

        /// <summary>
        /// 同时设置一个或多个 key-value 对
        /// </summary>
        /// <param name="entrys"></param>
        public void MSet(Dictionary<string, object> entrys)
        {
            ExecuteCommand<string>(ArgsToArray(RedisCommand.Strings.MSET, entrys));
        }

        public void MSetNx(Dictionary<string, object> entrys)
        {
            ExecuteCommand<string>(ArgsToArray(RedisCommand.Strings.MSETNX, entrys));
        }

        public Dictionary<string, T> MGet<T>(string[] keys)
        {
            var values = ExecuteCommand<T>(ArgsToArray(RedisCommand.Strings.MGET, keys));
            Dictionary<string, T> dict = new Dictionary<string, T>();
            for (int i = 0; i < keys.Length; i++)
            {
                dict.Add(keys[i], values[i]);
            }
            return dict;
        }

        /// <summary>
        /// 将 value 追加到 key 原来的值的末尾
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Append(string key, string value)
        {
            return ExecuteCommand<int>(RedisCommand.Strings.APPEND, key, value).First();
        }

        public long Incr(string key)
        {
            return ExecuteCommand<long>(RedisCommand.Strings.INCR, key).First();
        }

        public long IncrBy(string key, int increment)
        {
            return ExecuteCommand<long>(RedisCommand.Strings.INCRBY, key, increment).First();
        }

        public long Decr(string key)
        {
            return ExecuteCommand<long>(RedisCommand.Strings.DECR, key).First();
        }

        public long DecrBy(string key, int decrement)
        {
            return ExecuteCommand<long>(RedisCommand.Strings.DECRBY, key, decrement).First();
        }

        public int SetRange(string key, int offset, string value)
        {
            return ExecuteCommand<int>(RedisCommand.Strings.SETRANGE, key, offset, value).First();
        }

        public string GetRange(string key, int start, int end)
        {
            return ExecuteCommand<string>(RedisCommand.Strings.GETRANGE, key, start, end).First();
        }

        public int StrLen(string key)
        {
            return ExecuteCommand<int>(RedisCommand.Strings.STRLEN, key).First();
        }

        /// <summary>
        /// 计算给定字符串中，被设置为 1 的比特位的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int BitCount(string key)
        {
            return ExecuteCommand<int>(RedisCommand.Strings.BITCOUNT, key).First();
        }

        /// <summary>
        /// 计算给定字符串中，被设置为 1 的比特位的数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int BitCount(string key, int start, int end)
        {
            return ExecuteCommand<int>(RedisCommand.Strings.BITCOUNT, key, start, end).First();
        }

        [Obsolete("请不要使用此方法")]
        public void BitOP()
        {
        }

        #endregion

        #region Hash

        public void HSet(string key, string field, object value)
        {
            //Return value
            //Integer reply, specifically:
            //1 if field is a new field in the hash and value was set.
            //0 if field already exists in the hash and the value was updated.
            ExecuteCommand<string>(RedisCommand.Hashes.HSET, key, field, value);
        }

        public void HSetNx(string key, string field, object value)
        {
            //Return value
            //Integer reply, specifically:
            //1 if field is a new field in the hash and value was set.
            //0 if field already exists in the hash and no operation was performed.
            ExecuteCommand<string>(RedisCommand.Hashes.HSETNX, key, field, value);
        }

        public T HGet<T>(string key, string field)
        {
            //Return value
            //Bulk reply: the value associated with field, or nil when field is not present in the hash or key does not exist.
            return ExecuteCommand<T>(RedisCommand.Hashes.HGET, key, field).First();
        }

        public Dictionary<string, T> HGetAll<T>(string key)
        {
            //Return value
            //Multi-bulk reply: list of fields and their values stored in the hash, or an empty list when key does not exist.
            Dictionary<string, T> dict = new Dictionary<string, T>();
            var vals = ExecuteCommand<string>(RedisCommand.Hashes.HGETALL, key);
            for (int i = 0; i < vals.Count; i += 2)
            {
                dict.Add(vals[i], JsonDeserialize<T>(vals[i + 1]));
            }
            return dict;
        }

        public void HMSet(string key, Dictionary<string, object> entrys)
        {
            //Return value
            //Status code reply
            ExecuteCommand<string>(ArgsToArray(RedisCommand.Hashes.HMSET, key, entrys));
        }

        public Dictionary<string, T> HMGet<T>(string key, string[] fields)
        {
            //Return value
            //Multi-bulk reply: list of values associated with the given fields, in the same order as they are requested.
            Dictionary<string, T> dict = new Dictionary<string, T>();
            var vals = ExecuteCommand<T>(ArgsToArray(RedisCommand.Hashes.HMGET, key, fields));
            for (int i = 0; i < fields.Length; i++)
            {
                dict.Add(fields[i], vals[i]);
            }
            return dict;
        }

        public int HDel(string key, params string[] fields)
        {
            //Return value
            //Integer reply: the number of fields that were removed from the hash, not including specified but non existing fields.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Hashes.HDEL, key, fields)).First();
        }

        public bool HExists(string key, string field)
        {
            //Return value
            //Integer reply, specifically:
            //1 if the hash contains field.
            //0 if the hash does not contain field, or key does not exist.
            return ExecuteCommand<int>(RedisCommand.Hashes.HEXISTS, key, field).First() == 1;
        }

        public long HIncrBy(string key, string field, long increment)
        {
            //Return value
            //Integer reply: the value at field after the increment operation.
            return ExecuteCommand<long>(RedisCommand.Hashes.HINCRBY, key, field, increment).First();
        }

        public double HIncrByFloat(string key, string field, double increment)
        {
            //Return value
            //Bulk reply: the value of field after the increment.
            return ExecuteCommand<double>(RedisCommand.Hashes.HINCRBYFLOAT, key, field, increment).First();
        }

        public List<string> HKeys(string key)
        {
            //Return value
            //Multi-bulk reply: list of fields in the hash, or an empty list when key does not exist.
            return ExecuteCommand<string>(RedisCommand.Hashes.HKEYS, key);
        }

        public List<T> HVals<T>(string key)
        {
            //Return value
            //Multi-bulk reply: list of values in the hash, or an empty list when key does not exist.
            return ExecuteCommand<T>(RedisCommand.Hashes.HVALS, key);
        }

        public int HLen(string key)
        {
            //Return value
            //Integer reply: number of fields in the hash, or 0 when key does not exist.
            return ExecuteCommand<int>(RedisCommand.Hashes.HLEN, key).First();
        }

        public Dictionary<T, V> HScan<T, V>(string key, int cursor, string pattern, int count)
        {
            Dictionary<T, V> dict = new Dictionary<T, V>();
            var vals = ExecuteCommand<string>(RedisCommand.Hashes.HSCAN, key, cursor, string.IsNullOrEmpty(pattern) ? null : "MATCH", pattern, "COUNT", count);

            for (int i = 1; i < vals.Count; i += 2)
            {
                dict.Add(JsonDeserialize<T>(vals[i]), JsonDeserialize<V>(vals[i + 1]));
            }

            return dict;
        }

        #endregion

        #region List
        public Tuple<string, T> BLPop<T>(int timeout, params string[] keys)
        {
            //Return value
            //Multi-bulk reply: specifically:
            //A nil multi-bulk when no element could be popped and the timeout expired.
            //A two-element multi-bulk with the first element being the name of the key where an element was popped and the second element being the value of the popped element.
            var vals = ExecuteCommand<string>(ArgsToArray(RedisCommand.Lists.BLPOP, keys, timeout));
            if (vals[0] != null) return new Tuple<string, T>(vals[0], JsonDeserialize<T>(vals[1]));
            else return new Tuple<string, T>(null, default(T));
        }

        public Tuple<string, T> BRPop<T>(int timeout, params string[] keys)
        {
            //Return value
            //Multi-bulk reply: specifically:
            //A nil multi-bulk when no element could be popped and the timeout expired.
            //A two-element multi-bulk with the first element being the name of the key where an element was popped and the second element being the value of the popped element.
            var vals = ExecuteCommand<string>(ArgsToArray(RedisCommand.Lists.BRPOP, keys, timeout));
            if (vals[0] != null) return new Tuple<string, T>(vals[0], JsonDeserialize<T>(vals[1]));
            else return new Tuple<string, T>(null, default(T));
        }

        public T BRPopLPush<T>(string sourceSet, string destSet, int timeout)
        {
            //Return value
            //Bulk reply: the element being popped and pushed.
            return ExecuteCommand<T>(RedisCommand.Lists.BRPOPLPUSH, sourceSet, destSet, timeout).First();
        }

        public T LIndex<T>(string key, int index)
        {
            //Return value
            //Bulk reply: the requested element, or nil when index is out of range.
            return ExecuteCommand<T>(RedisCommand.Lists.LINDEX, key, index).First();
        }

        public int LInsertBefore(string key, object pivot, object value)
        {
            return LInsert(key, "BEFORE", pivot, value);
        }

        public int LInsertAfter(string key, object pivot, object value)
        {
            return LInsert(key, "AFTER", pivot, value);
        }

        public int LInsert(string key, string postion, object pivot, object value)
        {
            //Return value
            //Integer reply: the length of the list after the insert operation, or -1 when the value pivot was not found.
            return ExecuteCommand<int>(RedisCommand.Lists.LINSERT, key, postion, pivot, value).First();
        }

        public int LLen(string key)
        {
            //Return value
            //Integer reply: the length of the list at key.
            return ExecuteCommand<int>(RedisCommand.Lists.LLEN, key).First();
        }

        public T LPop<T>(string key)
        {
            //Return value
            //Bulk reply: the value of the first element, or nil when key does not exist.
            return ExecuteCommand<T>(RedisCommand.Lists.LPOP, key).First();
        }

        public int LPush(string key, params object[] values)
        {
            //Return value
            //Integer reply: the length of the list after the push operations.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Lists.LPUSH, key, values)).First();
        }

        public int LPushX(string key, params object[] values)
        {
            //Return value
            //Integer reply: the length of the list after the push operations.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Lists.LPUSHX, key, values)).First();
        }

        public List<T> LRange<T>(string key, int start, int stop)
        {
            // Return value
            //Multi-bulk reply: list of elements in the specified range.
            return ExecuteCommand<T>(RedisCommand.Lists.LRANGE, key, start, stop);
        }

        public int LRem(string key, int count, object value)
        {
            //Return value
            //Integer reply: the number of removed elements.
            return ExecuteCommand<int>(RedisCommand.Lists.LREM, key, count, value).First();
        }

        public void LSet(string key, int index, object value)
        {
            //Return value
            //Status code reply
            ExecuteCommand<string>(RedisCommand.Lists.LSET, key, index, value);
        }

        public void LTrim(string key, int start, int stop)
        {
            //Return value
            //Status code reply
            ExecuteCommand<string>(RedisCommand.Lists.LTRIM, start, stop);
        }

        public T RPop<T>(string key)
        {
            //Return value
            //Bulk reply: the value of the first element, or nil when key does not exist.
            return ExecuteCommand<T>(RedisCommand.Lists.RPOP, key).First();
        }

        public T RPopLPush<T>(string sourceSet, string destSet)
        {
            return ExecuteCommand<T>(RedisCommand.Lists.RPOPLPUSH, sourceSet, destSet).First();
        }

        public int RPush(string key, params object[] values)
        {
            //Return value
            //Integer reply: the length of the list after the push operations.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Lists.RPUSH, key, values)).First();
        }

        public int RPushx(string key, params object[] values)
        {
            //Return value
            //Integer reply: the length of the list after the push operations.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Lists.RPUSHX, key, values)).First();
        }
        #endregion

        #region Set

        public int SAdd(string key, params object[] values)
        {
            //Return value
            //Integer reply: the number of elements that were added to the set, not including all the elements already present into the set.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Sets.SADD, key, values)).First();
        }

        public int SCard(string key)
        {
            //Return value
            //Integer reply: the cardinality (number of elements) of the set, or 0 if key does not exist.
            return ExecuteCommand<int>(RedisCommand.Sets.SCARD, key).First();
        }

        public List<T> SDiff<T>(params string[] setIds)
        {
            //Return value
            //Multi-bulk reply: list with members of the resulting set.
            return ExecuteCommand<T>(ArgsToArray(RedisCommand.Sets.SDIFF, setIds));
        }

        public int SDiffStore(params string[] setIds)
        {
            //Return value
            //Integer reply: the number of elements in the resulting set.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Sets.SDIFFSTORE, setIds)).First();
        }

        public List<T> SInter<T>(params string[] setIds)
        {
            //Return value
            //Multi-bulk reply: list with members of the resulting set.
            return ExecuteCommand<T>(ArgsToArray(RedisCommand.Sets.SINTER, setIds));
        }

        public int SInterStore(params string[] setIds)
        {
            //Return value
            //Integer reply: the number of elements in the resulting set.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Sets.SINTERSTORE, setIds)).First();
        }

        public bool SIsMember(string key, object value)
        {
            //Return value
            //Integer reply, specifically:
            //1 if the element is a member of the set.
            //0 if the element is not a member of the set, or if key does not exist.
            return ExecuteCommand<int>(RedisCommand.Sets.SISMEMBER, key, value).First() == 1;
        }

        public List<T> SMembers<T>(string key)
        {
            //Return value
            //Multi-bulk reply: all elements of the set.
            return ExecuteCommand<T>(RedisCommand.Sets.SMEMBERS, key);
        }

        public bool SMove(string source, string dest, string value)
        {
            //Return value
            //Integer reply, specifically:
            //1 if the element is moved.
            //0 if the element is not a member of source and no operation was performed.
            return ExecuteCommand<int>(RedisCommand.Sets.SMOVE, source, dest, value).First() == 1;
        }

        public T SPop<T>(string key)
        {
            //Return value
            //Bulk reply: the removed element, or nil when key does not exist.
            return ExecuteCommand<T>(RedisCommand.Sets.SPOP, key).First();
        }

        public List<T> SRandMember<T>(string key, int count = 1)
        {
            //Return value
            //Bulk reply: without the additional count argument the command returns a Bulk Reply with the randomly selected element,
            //or nil when key does not exist. Multi-bulk reply: when the additional count argument is passed the command returns an array of elements,
            //or an empty array when key does not exist.
            return ExecuteCommand<T>(RedisCommand.Sets.SRANDMEMBER, key, count);
        }

        public int SRem(string key, params object[] values)
        {
            //Return value
            //Integer reply: the number of members that were removed from the set, not including non existing members.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Sets.SREM, key, values)).First();
        }

        public List<T> SUnion<T>(params string[] setIds)
        {
            //Return value
            //Multi-bulk reply: list with members of the resulting set.
            return ExecuteCommand<T>(ArgsToArray(RedisCommand.Sets.SUNION, setIds));
        }

        public int SUnionStore(params string[] setIds)
        {
            //Return value
            //Integer reply: the number of elements in the resulting set.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.Sets.SUNIONSTORE, setIds)).First();
        }

        public List<T> SScan<T>(string key, int cursor, string pattern, int count)
        {
            var vals = ExecuteCommand<string>(RedisCommand.Sets.SSCAN, key, cursor, string.IsNullOrEmpty(pattern) ? null : "MATCH", pattern, "COUNT", count);
            List<T> list = new List<T>(vals.Count - 1);
            for (int i = 1; i < list.Count; i++)
            {
                list.Add(JsonDeserialize<T>(vals[i]));
            }
            return list;
        }

        #endregion

        #region SortedSet

        public int ZAdd(string key, double score, object value)
        {
            //Return value
            //Integer reply, specifically:
            //The number of elements added to the sorted sets, not including elements already existing for which the score was updated
            return ExecuteCommand<int>(RedisCommand.SortedSets.ZADD, key, score, value).First();
        }

        public int ZAdd(string key, Dictionary<double, object> entrys)
        {
            //Return value
            //Integer reply, specifically:
            //The number of elements added to the sorted sets, not including elements already existing for which the score was updated
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.SortedSets.ZADD, key, entrys)).First();
        }

        public int ZCard(string key)
        {
            //Return value
            //Integer reply: the cardinality (number of elements) of the sorted set, or 0 if key does not exist.
            return ExecuteCommand<int>(RedisCommand.SortedSets.ZCARD, key).First();
        }

        public int ZCount(string key, double min, double max)
        {
            //Return value
            //Integer reply: the number of elements in the specified score range.
            return ExecuteCommand<int>(RedisCommand.SortedSets.ZCOUNT, key, min, max).First();
        }

        public double ZIncrBy(string key, object value, double increment)
        {
            //Return value
            //Bulk reply: the new score of member (a double precision floating point number), represented as string.
            return ExecuteCommand<double>(RedisCommand.SortedSets.ZINCRBY, key, increment, value).First();
        }

        public int ZInterStore(params string[] setIds)
        {
            //Return value
            //Integer reply: the number of elements in the resulting sorted set at destination.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.SortedSets.ZINTERSTORE, setIds.Length, setIds)).First();
        }

        public List<T> ZRange<T>(string key, int start, int stop)
        {
            //Return value
            //Multi-bulk reply: list of elements in the specified range (optionally with their scores).
            return ExecuteCommand<T>(RedisCommand.SortedSets.ZRANGE, key, start, stop);
        }

        public Dictionary<T, double> ZRangeWithScore<T>(string key, int start, int stop)
        {
            //Return value
            //Multi-bulk reply: list of elements in the specified range (optionally with their scores).
            var datas = ExecuteCommand<string>(RedisCommand.SortedSets.ZRANGE, key, start, stop, "WITHSCORES");
            Dictionary<T, double> result = new Dictionary<T, double>();
            for (int i = 0; i < datas.Count; i += 2)
            {
                result.Add(JsonDeserialize<T>(datas[i]), JsonDeserialize<double>(datas[i + 1]));
            }
            return result;
        }

        public List<T> ZRangeByScore<T>(string key, double min, double max, int offset, int count)
        {
            //Return value
            //Multi-bulk reply: list of elements in the specified score range (optionally with their scores).
            return ExecuteCommand<T>(RedisCommand.SortedSets.ZRANGEBYSCORE, key, min, max, "LIMIT", offset, count);
        }

        public Dictionary<T, double> ZRangeByScoreWithScore<T>(string key, double min, double max, int offset, int count)
        {
            //Return value
            //Multi-bulk reply: list of elements in the specified score range (optionally with their scores).
            var datas = ExecuteCommand<string>(RedisCommand.SortedSets.ZRANGEBYSCORE, key, min, max, "WITHSCORES", "LIMIT", offset, count);
            Dictionary<T, double> result = new Dictionary<T, double>();
            for (int i = 0; i < datas.Count; i += 2)
            {
                result.Add(JsonDeserialize<T>(datas[i]), JsonDeserialize<double>(datas[i + 1]));
            }
            return result;
        }

        public int ZRank(string key, object value)
        {
            //Return value
            //•If member exists in the sorted set, Integer reply: the rank of member.
            //•If member does not exist in the sorted set or key does not exist, Bulk reply: nil.
            return ExecuteCommand<int?>(RedisCommand.SortedSets.ZRANK, key, value).First() ?? -1;
        }

        public int ZRem(string key, params object[] values)
        {
            //Return value
            //Integer reply, specifically:
            //•The number of members removed from the sorted set, not including non existing members.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.SortedSets.ZREM, values)).First();
        }

        public int ZRemRangeByRank(string key, int start, int stop)
        {
            //Return value
            //Integer reply: the number of elements removed.
            return ExecuteCommand<int>(RedisCommand.SortedSets.ZREMRANGEBYRANK, key, start, stop).First();
        }

        public int ZRemRangeByScore(string key, double min, double max)
        {
            //Return value
            //Integer reply: the number of elements removed.
            return ExecuteCommand<int>(RedisCommand.SortedSets.ZREMRANGEBYSCORE, key, min, max).First();
        }

        public List<T> ZRevRange<T>(string key, int start, int stop)
        {
            //Return value
            //Multi-bulk reply: list of elements in the specified range (optionally with their scores).
            return ExecuteCommand<T>(RedisCommand.SortedSets.ZREVRANGE, key, start, stop);
        }

        public Dictionary<TScore, TValue> ZRevRange<TScore, TValue>(string key, int start, int stop)
        {
            //Return value
            //Multi-bulk reply: list of elements in the specified range (optionally with their scores).
            var datas = ExecuteCommand<string>(RedisCommand.SortedSets.ZREVRANGE, key, start, stop, "WITHSCORES");
            Dictionary<TScore, TValue> result = new Dictionary<TScore, TValue>();

            for (int i = 0; i < datas.Count / 2; i++)
            {
                result.Add(JsonDeserialize<TScore>(datas[i + 1]), JsonDeserialize<TValue>(datas[i]));
            }

            return result;
        }

        public List<T> ZRevRangeByScore<T>(string key, double min, double max, int offset, int count)
        {
            //Return value
            //Multi-bulk reply: list of elements in the specified score range (optionally with their scores).
            return ExecuteCommand<T>(RedisCommand.SortedSets.ZREVRANGEBYSCORE, key, min, max, "LIMIT", offset, count);
        }

        public Dictionary<TScore, TValue> ZRevRangeByScore<TScore, TValue>(string key, double min, double max, int offset, int count)
        {
            //Return value
            //Multi-bulk reply: list of elements in the specified score range (optionally with their scores).
            var datas = ExecuteCommand<string>(RedisCommand.SortedSets.ZREVRANGEBYSCORE, key, min, max, "WITHSCORES", "LIMIT", offset, count);
            Dictionary<TScore, TValue> result = new Dictionary<TScore, TValue>();
            for (int i = 0; i < datas.Count / 2; i++)
            {
                result.Add(JsonDeserialize<TScore>(datas[i + 1]), JsonDeserialize<TValue>(datas[i]));
            }
            return result;
        }

        public int ZRevRank(string key, object value)
        {
            //Return value
            //•If member exists in the sorted set, Integer reply: the rank of member.
            //•If member does not exist in the sorted set or key does not exist, Bulk reply: nil.
            return ExecuteCommand<int?>(RedisCommand.SortedSets.ZREVRANK, key, value).First() ?? -1;
        }

        public T ZScore<T>(string key, object value)
        {
            return ExecuteCommand<T>(RedisCommand.SortedSets.ZSCORE, key, value).First();
        }

        public int ZUnionStore(params string[] setIds)
        {
            //Return value
            //Integer reply: the number of elements in the resulting sorted set at destination.
            return ExecuteCommand<int>(ArgsToArray(RedisCommand.SortedSets.ZUNIONSTORE, setIds.Length, setIds)).First();
        }

        public List<T> ZScan<T>(string key, int cursor, string pattern, int count)
        {
            var vals = ExecuteCommand<string>(RedisCommand.SortedSets.ZSCAN, key, cursor, string.IsNullOrEmpty(pattern) ? null : "MATCH", pattern, "COUNT", count);
            List<T> list = new List<T>(vals.Count - 1);
            for (int i = 1; i < list.Count; i++)
            {
                list.Add(JsonDeserialize<T>(vals[i]));
            }
            return list;
        }

        #endregion

        #region PubSub
        public void PSubscribe(string channel)
        {
        }

        public void Publish(string channel)
        {
        }

        public void PubSub(string channel)
        {
        }

        public void PUnSubscribe(string channel)
        {
        }

        public void Subscribe(params string[] channels)
        {
            //SendCommand(ArgsToArray(RedisCommand.PubSub.SUBSCRIBE, channels));
        }

        public void UnSubscribe(string channel)
        {
        }
        #endregion

        #region Transaction
        public void Discard(string channel)
        {
            ExecuteCommand<string>(RedisCommand.Transactions.DISCARD);
        }

        public void Exec(string channel)
        {
            ExecuteCommand<string>(RedisCommand.Transactions.EXEC);
        }

        public void Multi(string channel)
        {
            ExecuteCommand<string>(RedisCommand.Transactions.MULTI);
        }

        public void UnWatch(string channel)
        {
            ExecuteCommand<string>(RedisCommand.Transactions.UNWATCH);
        }

        public void Watch(string channel)
        {
            ExecuteCommand<string>(RedisCommand.Transactions.WATCH);
        }
        #endregion

        #region Script
        #endregion

        #region Connection
        public void Auth(string password)
        {
            ExecuteCommand<string>(RedisCommand.Connection.AUTH, password);
        }

        public void Echo(string message)
        {
            ExecuteCommand<string>(RedisCommand.Connection.ECHO, message);
        }

        public bool Ping()
        {
            return ExecuteCommand<string>(RedisCommand.Connection.PING).First() == "PONG";
        }

        //public void Quit()
        //{
        //    ExecuteCommand<string>(RedisCommand.Connection.QUIT);

        //}

        public void Select(int index)
        {
            ExecuteCommand<string>(RedisCommand.Connection.SELECT, index);
        }
        #endregion

        #region Server

        public void BgReWriteAof()
        {
            ExecuteCommand<string>(RedisCommand.Server.BGREWRITEAOF);
        }

        public void BgSave()
        {
            ExecuteCommand<string>(RedisCommand.Server.BGSAVE);
        }

        public string ClientGetName()
        {
            return ExecuteCommand<string>(RedisCommand.Server.CLIENTGETNAME).First();
        }

        public void ClientKill()
        {
            ExecuteCommand<string>(RedisCommand.Server.CLIENTKILL);
        }

        public List<string> ClientList()
        {
            return ExecuteCommand<string>(RedisCommand.Server.CLIENTLIST);
        }

        public void ClientSetName(string clientName)
        {
            ExecuteCommand<string>(RedisCommand.Server.CLIENTSETNAME, clientName);
        }

        public List<string> ConfigGet(string parameter)
        {
            return ExecuteCommand<string>(RedisCommand.Server.CONFIGGET, parameter);
        }

        public void ConfigResetStat()
        {
            ExecuteCommand<string>(RedisCommand.Server.CONFIGRESETSTAT);
        }

        public void ConfigRewrite()
        {
            ExecuteCommand<string>(RedisCommand.Server.CONFIGREWRITE);
        }

        public void ConfigSet(string parameter, string value)
        {
            ExecuteCommand<string>(RedisCommand.Server.CONFIGSET, parameter, value);
        }

        public int DbSize()
        {
            return ExecuteCommand<int>(RedisCommand.Server.DBSIZE).First();
        }

        /// <summary>
        /// DEBUG OBJECT 是一个调试命令，它不应被客户端所使用。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string DebugObject(string key)
        {
            //返回值：
            //当 key 存在时，返回有关信息。
            //当 key 不存在时，返回一个错误。
            return ExecuteCommand<string>(RedisCommand.Server.DEBUGOBJECT, key).First();
        }

        /// <summary>
        /// 执行一个不合法的内存访问从而让 Redis 崩溃，仅在开发时用于 BUG 模拟。
        /// </summary>
        public void DebugSegfault()
        {
            //返回值：
            //无
            ExecuteCommand<string>(RedisCommand.Server.DEBUGSEGFAULT);
        }

        /// <summary>
        /// 清空整个 Redis 服务器的数据(删除所有数据库的所有 key )。
        /// </summary>
        public void FlushAll()
        {
            //返回值：
            //总是返回 OK 。
            ExecuteCommand<string>(RedisCommand.Server.FLUSHALL);
        }

        /// <summary>
        /// 清空当前数据库中的所有 key。
        /// </summary>
        public void FlushDb()
        {
            //返回值：
            //总是返回 OK 。
            ExecuteCommand<string>(RedisCommand.Server.FLUSHDB);
        }

        public string Info(string section = null)
        {
            return ExecuteCommand<string>(RedisCommand.Server.INFO, section).First();
        }

        /// <summary>
        /// 返回最近一次 Redis 成功将数据保存到磁盘上的时间，以 UNIX 时间戳格式表示。
        /// </summary>
        public DateTime LastSave()
        {
            return ExecuteCommand<long>(RedisCommand.Server.LASTSAVE).First().ToDateTime();
        }


        /// <summary>
        /// SAVE 命令执行一个同步保存操作，将当前 Redis 实例的所有数据快照(snapshot)以 RDB 文件的形式保存到硬盘。
        /// </summary>
        public void Save()
        {
            ExecuteCommand<string>(RedisCommand.Server.SAVE);
        }

        /// <summary>
        ///如果持久化被打开的话， SHUTDOWN 命令会保证服务器正常关闭而不丢失任何数据。
        /// </summary>
        public void ShutDown()
        {
            ExecuteCommand<string>(RedisCommand.Server.SHUTDOWN);
        }

        /// <summary>
        /// 将当前服务器转变为指定服务器的从属服务器(slave server)。
        /// </summary>
        public void Slaveof(string host, int port)
        {
            ExecuteCommand<string>(RedisCommand.Server.SLAVEOF, host, port);

        }

        public void SlowLog()
        {
            //TODO 需要特殊处理
        }

        public void Sync()
        {
            //TODO 需要特殊处理
        }

        /// <summary>
        /// 返回当前服务器时间。
        /// </summary>
        public DateTime Time()
        {
            return ExecuteCommand<long>(RedisCommand.Server.TIME).First().ToDateTime();
        }

        #endregion

        #region Pipeline

        public Pipeline CreatePipeline()
        {
            return new Pipeline(this);
        }

        #endregion

    }
}